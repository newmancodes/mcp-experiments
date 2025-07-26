using PuzzleSolver.BreadthFirstSearch;

namespace PuzzleSolver.NumbersGame;

public sealed class Solver
{
    public SolverResult Solve(Board board, Target target)
    {
        var search = new BreadthFirstSearch<Board, MathematicalOperation>(
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