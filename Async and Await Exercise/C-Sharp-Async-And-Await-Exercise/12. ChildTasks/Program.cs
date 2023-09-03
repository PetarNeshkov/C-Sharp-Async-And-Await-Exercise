//Detached();
//Attached();

await DetachedAsync();

return;

async Task DetachedAsync()
{
    Console.WriteLine("First task async");

    await Task.Run(async () =>
    {
        await Task.Delay(4000);
        Console.WriteLine("Second task async");
    });

    Console.WriteLine("Done");
}

void Attached()
{
    Task
        .Factory
        .StartNew(() =>
        {
            Console.WriteLine("First task attached");

            Task.Factory.StartNew(() =>
            {
                Task.Delay(10000).Wait();

                Console.WriteLine("Second task attached");
            }, TaskCreationOptions.AttachedToParent);
        })
        .Wait();
}

void Detached()
{
    Task
        .Factory
        .StartNew(() =>
        {
            Console.WriteLine("First task detached");

            Task.Factory.StartNew(() =>
            {
                Task.Delay(10000).Wait();
                Console.WriteLine("Second task detached");
            });
        })
        .Wait();
}