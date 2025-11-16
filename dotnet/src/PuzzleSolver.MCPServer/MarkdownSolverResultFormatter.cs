using System.Text;
using PuzzleSolver.Common;
using PuzzleSolver.NumbersGame;

namespace PuzzleSolver.MCPServer;

internal class MarkdownSolverResultFormatter : IFormatter<SolverResult>
{
    public string Format(SolverResult value)
    {
        var sb = new StringBuilder();
        sb.Append("The number game which was ");

        if (!value.SolutionFound)
        {
            sb.Append("not ");
        }

        sb.Append("solved had the target of ");
        sb.Append(value.Target.Value);
        sb.Append(" and the numbers that could be used to calculate that target were ");
        sb.Append(value.Board);
        sb.Append(". Only basic arithmetic operations were allowed, these being addition, subtraction, multiplication and division. ");
        sb.Append("Results of any operation must be positive whole numbers. When these rules were applied, it was ");

        if (!value.SolutionFound)
        {
            sb.Append("not ");
        }

        sb.AppendLine("possible to solve the game.");
        sb.AppendLine();

        if (value.SolutionFound)
        {
            if (value.Instructions.Count == 2)
            {
                sb.AppendLine("The game was already in a solved state as the target ");
                sb.Append(value.Target.Value);
                sb.AppendLine(" was already present in the set of available numbers. No operations were required to arrive at the solution.");
            }
            else
            {
                sb.AppendLine("The steps taken to solve the problem are shown below.");
                sb.AppendLine();
                sb.AppendLine(
                    "| Available Numbers | Used Numbers | Operation Applied | Operation Result | Updated Available Numbers |");
                sb.AppendLine("|-|-|-|-|-|");
                foreach (var step in value.Instructions.OfType<AdditionalSolveInstruction>())
                {
                    sb.Append("| ");
                    sb.Append(step.State);
                    sb.Append(" | ");
                    sb.Append(step.Operation.LeftOperand.Value);
                    sb.Append(", ");
                    sb.Append(step.Operation.RightOperand.Value);
                    sb.Append(" | ");
                    sb.Append(step.Operation.Operator switch
                    {
                        Operator.Addition => '+',
                        Operator.Subtraction => '-',
                        Operator.Multiplication => '×',
                        Operator.Division => '÷',
                        _ => '?'
                    });
                    sb.Append(" | ");
                    sb.Append(step.Operation.Result.Value);
                    sb.Append(" | ");
                    sb.Append(step.Result);
                    sb.AppendLine(" |");
                }

                sb.AppendLine();
                sb.AppendLine("This was the first solution that was found, other solutions may exists and while others may take the same or more number of steps, no solution exists that takes fewer steps.");
            }
        }

        sb.AppendLine();
        return sb.ToString();
    }
}