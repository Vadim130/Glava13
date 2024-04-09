public class Task131
{
    public static async Task Main(string[] args)
    {
        Task task = Task.Run(() =>
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        });
        
        Task<int> task_ret = Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return 13;
        });
        Console.WriteLine(await  task_ret);
    }
}