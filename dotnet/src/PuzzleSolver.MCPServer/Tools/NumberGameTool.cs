using System.ComponentModel;
using ModelContextProtocol.Server;
using PuzzleSolver.NumbersGame;

namespace PuzzleSolver.MCPServer.Tools;

[McpServerToolType]
public class NumberGameTool
{
    [McpServerTool, Description("Solves a popular numbers game. The game is made up of six digits and the goal is to determine how to manipulate the digits using simple arithmetic operations in order to arrive at the target. The game was made famous on the UK TV Show, Countdown.")]
    public static string Solve(int[] numbers, int target)
    {
        try
        {
            var solver = new Solver();
            var solution = solver.Solve(numbers, new Target(target));

            return solution.SolutionFound ? "The game was solved." : "The game could not be solved.";
        }
        catch
        {
            return "The game could not be solved.";
        }
    }
}