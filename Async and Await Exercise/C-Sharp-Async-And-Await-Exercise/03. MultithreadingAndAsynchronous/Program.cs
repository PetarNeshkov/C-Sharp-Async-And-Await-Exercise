List<int> data = Enumerable.Range(0, 100).ToList();

for (int i = 0; i < 4; i++)
{
    var thread = new Thread(Work);

    thread.Start();

    if (i % 2 == 0)
    {
        thread.Join();
    }

    Console.WriteLine(data.Count);
}

void Work()
{
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine(i);

        Thread.Sleep(500);

        // Creates race condition
        if (data.Count > 90)
        {
            data.RemoveAt(data.Count - 1);
        }
    }
}