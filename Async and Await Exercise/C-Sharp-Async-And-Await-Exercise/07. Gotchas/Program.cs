//RunAsyncVoid();
//AsyncVoidLambda();

await NestedTasks();

async Task NestedTasks()
{
    await Task.Run(async () =>
    {
        Console.WriteLine("Before delay");

        await Task.Delay(1000);

        Console.WriteLine("After Delay");
    });

    Console.WriteLine("After Task");
}

void AsyncVoidLambda()
{
    try
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };

        list.ForEach(async number =>
        {
            await Task.Run(() => Console.WriteLine(number));

            throw new InvalidOperationException("In a lambda!");
        });
    }
    catch
    {
        Console.WriteLine("Cannot be caught!");
    }
}

void RunAsyncVoid()
{
    try
    {
        AsyncVoid();
    }
    catch
    {
        Console.WriteLine("Cannot be caught!");
        throw;
    }
}

async void AsyncVoid()
{
    await Task.Run(() => Console.WriteLine("Message"));

    throw new InvalidOperationException("Error");
}