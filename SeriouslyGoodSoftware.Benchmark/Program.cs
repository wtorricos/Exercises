using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SeriouslyGoodSoftware.Benchmark;

[MemoryDiagnoser] // adds allocation/GC columns
public class WaterContainerBenchmarks
{
    [GlobalSetup]
    public void Setup()
    {
    }

    [Benchmark]
    public void C1Novice()
    {
        RunBenchmark<C1Novice.Container>();
    }

    [Benchmark]
    public void C1Personal()
    {
        RunBenchmark<C1Personal.Container>();
    }

    [Benchmark]
    public void C2Starter()
    {
        RunBenchmark<C2Starter.Container>();
    }

    [Benchmark]
    public void C31SharedGroup()
    {
        RunBenchmark<C31SharedGroup.Container>();
    }

    [Benchmark]
    public void C32CircularList()
    {
        RunBenchmark<C32CircularList.Container>();
    }

    [Benchmark]
    public void C33UnionFind()
    {
        RunBenchmark<C33UnionFind.Container>();
    }

    [Benchmark]
    public void C4Memory2()
    {
        RunBenchmark<C4Memory2.Container>();
    }

    void RunBenchmark<T>() where T : IContainer, new()
    {
        T last = new();
        for(int i = 0; i < 10; i++)
        {
            T next = new();
            next.AddWater(last.Amount + 2);
            last.ConnectTo(next);
        }
    }
}

// Run with: dotnet run -c Release --filter *WaterContainerBenchmarks*
public class Program
{
    // Using BenchmarkSwitcher lets you pass --filter, --memory, --list, etc.
    public static void Main(string[] args) =>
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
}
