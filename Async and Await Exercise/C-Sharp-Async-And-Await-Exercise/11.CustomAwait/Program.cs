using CustomAwait;

var content = await new Uri("https://youtube.com");
Console.WriteLine(content);

await TimeSpan.FromSeconds(3);

Console.WriteLine("Waiting");

await 1000;

Console.WriteLine("Wait more!");

await "Hello";

Console.WriteLine("Hi");

await new List<Task>
{
    Task.Delay(1000),
    Task.Delay(2000)
};

Console.WriteLine("Result");