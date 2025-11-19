using PuzzleSolver.BreadthFirstSearch;
using PuzzleSolver.DepthFirstSearch;
using PuzzleSolver.Search;

namespace PuzzleSolver.NumbersGame;

public sealed class Solver
{
    public SolverResult Solve(Board board, Target target)
    {
        ISearch<Board, MathematicalOperation> search = new DepthFirstSearch<Board, MathematicalOperation>(
            b => b.Options.Any(n => n.Value == target.Value),
            t => t.GeneratePossibleActions(),
            new BoardEqualityComparer());

        foreach (var successfulStateTraversal in search.Execute(board))
        {
            var solution = new Solution(successfulStateTraversal);
            return new SolverResult(board, target, solution);
        }
        
        return new SolverResult(board, target);
    }
}