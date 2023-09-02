var cancellation = new CancellationTokenSource(2000);

var printTask = Task.Run(async () =>
{
    while (true)
    {
        if (cancellation.IsCancellationRequested)
        {
            Console.WriteLine("Enough!");

            cancellation.Token.ThrowIfCancellationRequested();

            break;
        }

        Console.WriteLine(DateTime.Now);

        await Task.Delay(1000);
    }
}, cancellation.Token);

var readTask = Task.Run(() =>
{
    while (true)
    {
        var input = Console.ReadLine();

        if (input == "end")
        {
            cancellation.Cancel();

            break;
        }
    }
});

await Task.WhenAll(printTask, readTask);