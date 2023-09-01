//WaitForTask();
//TaskContinuation();
//TaskExceptionsAndStatus();
//MultipleTasksAtTheSameTime();
//AtLeastOneTaskToFinish();
//CompletedTaskFromResult();

DownloadContentAndSaveItToFile();

void DownloadContentAndSaveItToFile()
{
    using var httpClient = new HttpClient();

    var googleTask = httpClient.GetStringAsync("https://google.com");

    googleTask
        .ContinueWith(prevTask =>
        {
            File
                .WriteAllTextAsync("google.txt", prevTask.Result)
                .Wait();
        });

    var codeLessonsTask = httpClient.GetStringAsync("http://codelessons.online/");

    codeLessonsTask
        .ContinueWith(prevTask =>
        {
            File
                .WriteAllTextAsync("codelessons.txt", prevTask.Result)
                .Wait();
        });

    var ambitionedTask = httpClient.GetStringAsync("http://ambitioned.com");

    ambitionedTask
        .ContinueWith(prevTask =>
        {
            File
                .WriteAllTextAsync("ambitioned.com.txt", prevTask.Result)
                .Wait();
        });

    var tasks = new[]
    {
        googleTask,
        codeLessonsTask,
        ambitionedTask,
    };

    Task
        .WhenAll(tasks)
        .ContinueWith(prevTask =>
        {
            var content = $"{prevTask.Result[0]}{prevTask.Result[1]}{prevTask.Result[2]}";

            File
                .AppendAllTextAsync("downloads.txt", content)
                .Wait();
        })
        .Wait();
}

void CompletedTaskFromResult()
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
                .ContinueWith(prev => Console.WriteLine("Invalid input!"))
        };

        task.Wait();
    }
}


void AtLeastOneTaskToFinish()
{
    Console.WriteLine("You have 5 seconds to solve this: 111 * 111");

    var inputTask = Task.Run(() =>
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (input == "12321")
            {
                Console.WriteLine("Correct!");
                break;
            }

            Console.WriteLine("Wrong answer!");
        }
    });

    var timerTask = Task.Run(() =>
    {
        for (var i = 5; i > 0; i--)
        {
            Console.WriteLine(i);

            Task.Delay(1000).Wait();
        }
    });

    Task.WaitAny(inputTask, timerTask);
}

void MultipleTasksAtTheSameTime()
{
    var firstTask = Task
        .Run(() => Task.Delay(3000).Wait())
        .ContinueWith(_ => Console.WriteLine("First"));

    var secondTask = Task
        .Run(() => Task.Delay(1000).Wait())
        .ContinueWith(_ => Console.WriteLine("Second"));

    var thirdTask = Task
        .Run(() => Task.Delay(2000).Wait())
        .ContinueWith(_ => Console.WriteLine("Third"));

    Task.WaitAll(firstTask, secondTask, thirdTask);
}

void TaskExceptionsAndStatus()
{
    var task = Task
        .Run(() => throw new InvalidOperationException("Some exception"))
        .ContinueWith(previousTask =>
        {
            if (previousTask.IsFaulted)
            {
                Console.WriteLine(previousTask.Exception.Message);
            }
        })
        .ContinueWith(previousTask =>
        {
            if (previousTask.IsCompletedSuccessfully)
            {
                Console.WriteLine("Done");
            }
        });

    task.Wait();
}

void TaskContinuation()
{
    var task = Task
        .Run(() => "Result")
        .ContinueWith(previousTask => { Console.WriteLine(previousTask.Result); })
        .ContinueWith(_ => Task.Delay(2000).Wait())
        .ContinueWith(_ => { Console.WriteLine("After delay!"); });

    task.Wait();
}

void WaitForTask()
{
    var firstTask = Task.Run(() => { Console.WriteLine("First Task"); });

    var secondTask = Task.Run(() => "Second Task");

    Console.WriteLine("Sync write!");

    firstTask.Wait();

    var result = secondTask.Result;

    Console.WriteLine(result);
}