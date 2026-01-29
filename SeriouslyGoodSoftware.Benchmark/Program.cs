using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace SeriouslyGoodSoftware.Benchmark;

[MemoryDiagnoser] // adds allocation/GC columns
public class WaterContainerBenchmarks
{
    [GlobalSetup]
    public void Setup()
    {
    }

    [Benchmark]
    public void Novice()
    {
        C1Novice.Container a = new();
        C1Novice.Container b = new();
        C1Novice.Container c = new();
        C1Novice.Container d = new();
        a.AddWater(12);
        d.AddWater(8);
        a.ConnectTo(b);
        b.ConnectTo(c);
        // a.ConnectTo(c); // case not covered
        c.ConnectTo(d);
    }

    [Benchmark]
    public void Personal()
    {
        C1Personal.Container a = new();
        C1Personal.Container b = new();
        C1Personal.Container c = new();
        C1Personal.Container d = new();
        a.AddWater(12);
        d.AddWater(8);
        a.ConnectTo(b);
        b.ConnectTo(c);
        a.ConnectTo(c);
        c.ConnectTo(d);
    }

    [Benchmark]
    public void Starter()
    {
        C2Starter.Container a = new();
        C2Starter.Container b = new();
        C2Starter.Container c = new();
        C2Starter.Container d = new();
        a.AddWater(12);
        d.AddWater(8);
        a.ConnectTo(b);
        b.ConnectTo(c);
        a.ConnectTo(c);
        c.ConnectTo(d);
    }

    [Benchmark]
    public void SharedGroup()
    {
        C31SharedGroup.Container a = new();
        C31SharedGroup.Container b = new();
        C31SharedGroup.Container c = new();
        C31SharedGroup.Container d = new();
        a.AddWater(12);
        d.AddWater(8);
        a.ConnectTo(b);
        b.ConnectTo(c);
        // a.ConnectTo(c); // case not covered
        c.ConnectTo(d);
    }
}

// Run with: dotnet run -c Release --filter *WaterContainerBenchmarks*
public class Program
{
    // Using BenchmarkSwitcher lets you pass --filter, --memory, --list, etc.
    public static void Main(string[] args) =>
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
}
