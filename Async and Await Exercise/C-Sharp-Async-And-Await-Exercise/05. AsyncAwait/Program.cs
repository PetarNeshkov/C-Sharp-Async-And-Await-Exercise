using _05._AsyncAwait;

//await WaitForTask();
//await TaskWithContinuation();
//await TaskExceptionAndStatus();
//await MultipleTasksAtTheSameTime();
//await AtLeastOneTaskToFinish();
await AsyncLambda();

async Task AsyncLambda()
{
    var list = new List<int> { 1, 2, 3, 4, 5 };

    list.ForEach(async number => { await Task.Run(() => Console.WriteLine(number)); });
}

await DownloadContentAndSaveItToFile();

async Task DownloadContentAndSaveItToFile()
{
    using var httpClient = new HttpClient();

    var googleTask = httpClient.GetStringAsync("https://google.com");
    var codeLessonsTask = httpClient.GetStringAsync("http://codelessons.online/");
    var youtubeTask = httpClient.GetStringAsync("https://youtube.com");

    var getTasks = new List<Task<string>>
    {
        googleTask, codeLessonsTask, youtubeTask
    };

    var (google, codeLessons, youtube) = await Task.WhenAll(getTasks);

    var writeFileTasks = new List<Task>
    {
        File.WriteAllTextAsync("google.txt", google),
        File.WriteAllTextAsync("codelessons.txt", codeLessons),
        File.WriteAllTextAsync("youtube.txt", youtube)
    };

    await Task.WhenAll(writeFileTasks);

    var content = $"{google}{codeLessons}{youtube}";

    await File.AppendAllTextAsync("downloads.txt", content);
}


await CompletedTaskAndResult();

async Task CompletedTaskAndResult()
{
    while (true)
    {
        var input = Console.ReadLine();

        if (input == "end")
        {
            break;
        }

        var task = input switch
        {
            "delay" => Task
                .Delay(2000)
                .ContinueWith(_ => Console.WriteLine("Delayed")),

            "print" => Task
                .Run(() => Console.WriteLine("Printed!")),

            "throw" => Task
                .FromException(new InvalidOperationException("Error"))
                .ContinueWith(prev => Console.WriteLine(prev.Exception.Message)),

            "42" => Task
                .FromResult(42)
                .ContinueWith(prev => Console.WriteLine(prev.Result)),

            _ => Task
                .CompletedTask
                .ContinueWith(_ => Console.WriteLine("Invalid Input"))
        };

        await task;
    }
}

async Task AtLeastOneTaskToFinish()
{
    Console.WriteLine("You have 5 seconds to solve this: 111 * 111");

    var inputTask = Task.Run(() =>
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (input == "12321")
            {
                Console.WriteLine("Correct");
                break;
            }

            Console.WriteLine("Wrong answer");
        }
    });

    var timerTask = Task.Run(async () =>
    {
        for (int i = 5 - 1; i >= 0; i--)
        {
            Console.WriteLine(i);

            await Task.Delay(1000);
        }
    });

    await Task.WhenAll(inputTask, timerTask);
}

async Task MultipleTasksAtTheSameTime()
{
    var firstTask = Task.Run(async () =>
    {
        await Task.Delay(3000);
        Console.WriteLine("First");
    });

    var secondTask = Task.Run(async () =>
    {
        await Task.Delay(1000);
        Console.WriteLine("Second");
    });

    var thirdTask = Task.Run(async () =>
    {
        await Task.Delay(2000);
        Console.WriteLine("Third");
    });

    await Task.WhenAll(firstTask, secondTask, thirdTask);
}

async Task TaskExceptionAndStatus()
{
    try
    {
        await Task.Run(() => throw new InvalidOperationException("Some exception"));
    }
    catch (InvalidOperationException exception)
    {
        Console.WriteLine(exception.Message);
    }

    Console.WriteLine("Done");
}

async Task TaskWithContinuation()
{
    var result = await Task.Run(() => "Result");

    Console.WriteLine(result);

    await Task.Delay(2000);

    Console.WriteLine("After delay");
}

return;

async Task WaitForTask()
{
    var firstTask = Task.Run(() => { Console.WriteLine("First task"); });

    var secondTask = Task.Run(() => "Second task");

    Console.WriteLine("Sync write!");

    await firstTask;

    var result = await secondTask;

    Console.WriteLine(result);
}