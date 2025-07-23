using PuzzleSolver.BreadthFirstSearch;

namespace PuzzleSolver.NumbersGame;

public class Solution
{
    internal Board Start { get; init; }

    internal IReadOnlyCollection<SolutionStep> Steps { get; init; }

    public Solution(StateTraversal<Board, MathematicalOperation> traversal)
    {
        var steps = new Stack<SolutionStep>();

        do
        {
            if (traversal is { Parent: not null, TraversalDescription: not null })
            {
                var step = new SolutionStep(traversal.Parent.Child, traversal.TraversalDescription, traversal.Child);
                steps.Push(step);
                traversal = traversal.Parent;
            }
            else
            {
                // Reached start of solution path.
                break;
            }
        } while (true);

        Start = traversal.Child;
        Steps = steps.ToArray().AsReadOnly();
    }
}

internal record SolutionStep(Board From, MathematicalOperation Operation, Board Result);