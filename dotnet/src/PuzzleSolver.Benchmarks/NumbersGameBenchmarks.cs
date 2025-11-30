using BenchmarkDotNet.Attributes;
using PuzzleSolver.NumbersGame;

namespace PuzzleSolver.Benchmarks;

public class NumbersGameBenchmarks
{
    public IEnumerable<Configuration> Configurations()
    {
        yield return new Configuration(ConfigurationDifficulty.AlreadySolved, Board.From([ 1, 2, 3, 4, 5, 100 ]), new Target(100));
        yield return new Configuration(ConfigurationDifficulty.Easy, Board.From([ 1, 4, 4, 5, 6, 50 ]), new Target(350));
        yield return new Configuration(ConfigurationDifficulty.Medium, Board.From([ 1, 3, 3, 8, 9, 50 ]), new Target(410));
        yield return new Configuration(ConfigurationDifficulty.Hard, Board.From([ 1, 10, 25, 50, 75, 100 ]), new Target(813));
        yield return new Configuration(ConfigurationDifficulty.Impossible, Board.From([ 3, 7, 6, 2, 1, 7 ]), new Target(824));
    }

    [ParamsSource(nameof(Configurations))]
    public Configuration Configuration { get; set; } = null!;

    [Benchmark(Baseline = true)]
    public void UsingBreadthFirstSearch()
    {
        var solver = new Solver();
        var solution = solver.Solve(Configuration.Board, Configuration.Target, SearchStrategy.BreadthFirst);
    }
    
    [Benchmark]
    public void UsingDepthFirstSearch()
    {
        var solver = new Solver();
        var solution = solver.Solve(Configuration.Board, Configuration.Target, SearchStrategy.DepthFirst);
    }
    
    [Benchmark]
    public void UsingIterativeDepthFirstSearch()
    {
        var solver = new Solver();
        var solution = solver.Solve(Configuration.Board, Configuration.Target, SearchStrategy.IterativeDeepeningDepthFirst);
    }
}