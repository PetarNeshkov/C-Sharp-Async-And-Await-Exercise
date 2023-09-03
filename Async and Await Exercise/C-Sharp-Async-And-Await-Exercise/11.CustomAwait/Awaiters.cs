using System.Runtime.CompilerServices;

namespace CustomAwait;

public static class Awaiters
{
    public static TaskAwaiter<string> GetAwaiter(this Uri uri)
        => new HttpClient().GetStringAsync(uri).GetAwaiter();

    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        => Task.Delay(timeSpan).GetAwaiter();

    public static TaskAwaiter GetAwaiter(this int number)
        => TimeSpan.FromMilliseconds(number).GetAwaiter();

    public static TaskAwaiter GetAwaiter(this string text)
    {
        var task = Task.Run(async () =>
        {
            await Task.Delay(5000);

            var message = text;
            Console.WriteLine(message);
        });

        return task.GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks)
        => Task.WhenAll(tasks).GetAwaiter();
}