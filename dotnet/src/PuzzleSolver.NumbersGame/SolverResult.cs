namespace PuzzleSolver.NumbersGame;

public class SolverResult
{
    private readonly Board _board;
    private readonly Target _target;

    public Board Board => _board;
    
    public Target Target => _target;

    public bool SolutionFound { get; init; }

    public IReadOnlyCollection<SolveInstruction> Instructions { get; init; }

    internal SolverResult(Board board, Target target)
        : this(board, target, [])
    {
    }

    internal SolverResult(Board board, Target target, Solution solution)
        : this(board, target, [ solution ])
    {
    }

    private SolverResult(Board board, Target target, IEnumerable<Solution> solutions)
    {
        _board = board;
        _target = target;
        var solution = solutions.FirstOrDefault();
        SolutionFound = solution is not null;
        
        var instructions = new List<SolveInstruction>();

        if (SolutionFound)
        {
            instructions.Add(new InitialSolveInstruction(solution!.Start));
            var finalState = solution.Start;

            foreach (var step in solution.Steps)
            {
                instructions.Add(new AdditionalSolveInstruction(step.From, step.Operation, step.Result));
                finalState = step.Result;
            }

            instructions.Add(new FinalSolveInstruction(finalState));
        }

        Instructions = instructions.AsReadOnly();
    }
}

public abstract record SolveInstruction(Board State);

public sealed record InitialSolveInstruction(Board State) : SolveInstruction(State);

public sealed record AdditionalSolveInstruction(Board State, MathematicalOperation Operation, Board Result) : SolveInstruction(State);

public sealed record FinalSolveInstruction(Board State) : SolveInstruction(State);
