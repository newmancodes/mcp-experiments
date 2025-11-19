using BenchmarkDotNet.Attributes;
using PuzzleSolver.NumbersGame;

namespace PuzzleSolver.Benchmarks;

[MemoryDiagnoser]
public class NumbersGameBenchmarks
{
    private readonly Board _board = Board.From([ 1, 4, 4, 5, 6, 50 ]);
    private readonly Target _target = new(350);
    
    [Benchmark(Baseline = true)]
    public void UsingBreadthFirstSearch()
    {
        var solver = new Solver();
        var solution = solver.Solve(_board, _target, SearchStrategy.BreadthFirst);
    }
    
    [Benchmark]
    public void UsingDepthFirstSearch()
    {
        var solver = new Solver();
        var solution = solver.Solve(_board, _target, SearchStrategy.DepthFirst);
    }
    
    [Benchmark]
    public void UsingIterativeDepthFirstSearch()
    {
        var solver = new Solver();
        var solution = solver.Solve(_board, _target, SearchStrategy.IterativeDepthFirst);
    }
}