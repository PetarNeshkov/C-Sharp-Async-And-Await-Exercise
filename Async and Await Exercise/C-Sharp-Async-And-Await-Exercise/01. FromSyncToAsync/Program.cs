SimpleExample();
ComplexExample();

void SimpleExample()
{
    var task = Task.Run(() => Console.WriteLine("First!"));

    Console.WriteLine("Second");

    task.Wait();
}

void ComplexExample()
{
    var task = Task
        .Run(() => Task
            .Delay(2000)
            .ContinueWith(t => "In a task"));

    Task.Delay(4000).Wait();
    Console.WriteLine("Outside of a task!");

    var completion = Task
        .WhenAll(task)
        .ContinueWith(async t => { Console.WriteLine((await t)[0]); });

    completion.Wait();
}