using PuzzleSolver.BreadthFirstSearch;
using PuzzleSolver.DepthFirstSearch;
using PuzzleSolver.Search;

namespace PuzzleSolver.NumbersGame;

public sealed class Solver
{
    public SolverResult Solve(Board board, Target target, SearchStrategy searchStrategy)
    {
        ISearch<Board, MathematicalOperation> search = searchStrategy switch
        {
            SearchStrategy.BreadthFirst => new BreadthFirstSearch<Board, MathematicalOperation>(
                b => b.Options.Any(n => n.Value == target.Value),
                t => t.GeneratePossibleActions(),
                new BoardEqualityComparer()),
            SearchStrategy.DepthFirst => new DepthFirstSearch<Board, MathematicalOperation>(
                b => b.Options.Any(n => n.Value == target.Value),
                t => t.GeneratePossibleActions(),
                new BoardEqualityComparer()),
            SearchStrategy.IterativeDeepeningDepthFirst => new IterativeDeepeningDepthFirstSearch<Board, MathematicalOperation>(
                b => b.Options.Any(n => n.Value == target.Value),
                t => t.GeneratePossibleActions(),
                t => board.Count - t.Child.Count,
                board.Count,
                new BoardEqualityComparer()),
            _ => throw new ArgumentOutOfRangeException(nameof(searchStrategy), searchStrategy, "Unsupported search strategy.")
        };

        foreach (var successfulStateTraversal in search.Execute(board))
        {
            var solution = new Solution(successfulStateTraversal);
            return new SolverResult(board, target, solution);
        }

        return new SolverResult(board, target);
    }
}

public enum SearchStrategy
{
    BreadthFirst,
    DepthFirst,
    IterativeDeepeningDepthFirst
}