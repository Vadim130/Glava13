using System.Data;
using System.Threading.Tasks.Dataflow;

public class Task133
{
    static void RotateMatrices(IEnumerable<IEnumerable<Matrix>> collections,
        float degrees)
    {
        var schedulerPair = new ConcurrentExclusiveSchedulerPair(
        TaskScheduler.Default, maxConcurrencyLevel: 8);
        TaskScheduler scheduler = schedulerPair.ConcurrentScheduler;
        ParallelOptions options = new ParallelOptions
        {
            TaskScheduler = scheduler
        };
        Parallel.ForEach(collections, options,
        matrices => Parallel.ForEach(matrices, options,
        matrix => matrix.Rotate(degrees)));
    }
    public static void Main(string[] args)
    {
        Matrix[][] matrices = new Matrix[4][];

        for (int i = 0; i < 4; i++)
        {
            matrices[i] = new Matrix[45];
            for (int j = 0; j < 45; j++)
            {
                matrices[i][j] = new Matrix(new float[,] { { 1, 0 }, { 0, 1 } });
                matrices[i][j].Rotate(i*45+j);
            }
        }
        RotateMatrices(matrices, 180);
        foreach (var row in matrices)
            foreach (var m in row)
                Console.WriteLine(m);

    }
}
public class Matrix
{
    float[,] els = new float[2, 2];
    public Matrix(float[,] e)
    {
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                els[i, j] = e[i, j];
    }
    public void Rotate(float degrees)
    {
        Matrix old = new Matrix(els);
        float angle = degrees * (float)Math.PI / 180;
        float c = (float)Math.Cos(angle), s = (float)Math.Sin(angle);
        /*
            *  ( a00' a01')   ( c  -s )  ( a00 a01)
            *  ( a10' a11') = ( s   c )* ( a10 a11)
            */
        els[0, 0] = old.els[0, 0] * c - old.els[1, 0] * s;
        els[0, 1] = old.els[0, 1] * c - old.els[1, 1] * s;
        els[1, 0] = old.els[0, 0] * s + old.els[1, 0] * c;
        els[1, 1] = old.els[0, 1] * s + old.els[1, 1] * c;
    }
    public float det()
    {
        return els[0, 0] * els[1, 1] - els[1, 0] * els[0, 1];
    }
    public bool IsInvertible
    {
        get { return det() != 0; }
    }

    public void Invert()
    {
        float d = det();
        Matrix old = new Matrix(els);
        els[0, 0] = old.els[1, 1] / d;
        els[1, 1] = old.els[0, 0] / d;
        els[0, 1] = -old.els[0, 1] / d;
        els[1, 0] = -old.els[1, 0] / d;
    }
    public override String ToString()
    {
        return "{ {" + els[0, 0] + ", " + els[0, 1] + "}, {" + els[1, 0] + ", " + els[1, 1] + "} }";
    }
}