using System.Threading.Tasks.Dataflow;

public class Task131
{
    public static void Main(string[] args)
    {
        var options = new ExecutionDataflowBlockOptions
        {
            TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
        };
        var multiplyBlock = new TransformBlock<int, int>(item => item * 2);
        var displayBlock = new ActionBlock<int>(
         result => ListBox.Items.Add(result), options);
        multiplyBlock.LinkTo(displayBlock);
    }
}