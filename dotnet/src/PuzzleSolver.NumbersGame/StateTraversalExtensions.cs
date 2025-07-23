using PuzzleSolver.BreadthFirstSearch;

namespace PuzzleSolver.NumbersGame;

internal static class StateTraversalExtensions
{
    internal static IEnumerable<StateTraversal<Board, MathematicalOperation>> GeneratePossibleActions(this StateTraversal<Board, MathematicalOperation> traversal)
    {
        foreach (var possibleAction in traversal.Child.GeneratePossibleActions())
        {
            yield return new StateTraversal<Board, MathematicalOperation>(traversal, possibleAction.Operation, possibleAction.Result);
        }
    }
}