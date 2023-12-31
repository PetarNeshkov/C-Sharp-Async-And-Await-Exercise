﻿using _10._EventBasedApi;

var result = await RunAsync(() => true);
Console.WriteLine(result);

await DelayAsync(2000, () => Console.WriteLine("Delayed"));

await RunCancellation();

var eventBasedResult = await EventBasedApiWrapper();


Console.WriteLine(eventBasedResult);

return;

Task<string> EventBasedApiWrapper()
{
    var tcs = new TaskCompletionSource<string>();

    var obj = new EventBasedApi();

    obj.Done += arg =>
    {
        // This will notify the caller 
        // of the EventBasedApiWrapper that 
        // the task just completed.
        tcs.SetResult(arg);
    };

    // Start the event-based work.
    obj.Work();

    return tcs.Task;
}

async Task RunCancellation()
{
    var cancellation = new CancellationTokenSource();
    var counter = 0;

    var task = Task.Run(async () =>
    {
        while (true)
        {
            if (counter == 5)
            {
                cancellation.Cancel();
                break;
            }

            await Task.Delay(1000);

            Console.WriteLine(DateTime.Now);

            counter++;
        }
    });

    var cancellationEnd = Task.Run(() => Console.WriteLine("End"));

    await Task.WhenAll(
        task,
        RunUtilCancellation(cancellation.Token, () => cancellationEnd));
}

Task RunUtilCancellation(CancellationToken cancellationToken, Func<Task> onCancel)
{
    var tcs = new TaskCompletionSource<bool>();

    cancellationToken.Register(
        async () =>
        {
            await onCancel();
            tcs.SetResult(true);
        });

    return tcs.Task;
}

Task DelayAsync(int millisecondsDelay, Action action)
{
    if (millisecondsDelay < 0)
    {
        throw new ArgumentOutOfRangeException(nameof(millisecondsDelay));
    }

    if (action == null)
    {
        throw new ArgumentNullException(nameof(action));
    }

    var tcs = new TaskCompletionSource<object>();

    var timer = new Timer(
        _ => tcs.SetResult(null), null, millisecondsDelay, Timeout.Infinite);

    return tcs.Task.ContinueWith(_ =>
    {
        timer.Dispose();
        action();
    });
}

Task<T> RunAsync<T>(Func<T> function)
{
    if (function == null)
    {
        throw new ArgumentNullException(nameof(function));
    }

    var tcs = new TaskCompletionSource<T>();

    ThreadPool.QueueUserWorkItem(_ =>
    {
        try
        {
            var result = function();
            tcs.SetResult(result);
        }
        catch (Exception exception)
        {
            tcs.SetException(exception);
        }
    });

    return tcs.Task;
}