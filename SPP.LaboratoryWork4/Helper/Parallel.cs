namespace Helper;

public class ParallelExtensions
{
    public static void WaitAll(Action<int>[] actions)
    {
        try
        {
            Parallel.For(0, actions.Length, i => actions[i].Invoke(i));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}