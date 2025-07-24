namespace PuzzleSolver.NumbersGame;

public class Target
{
    private const int MinTarget = 1;
    private const int MaxTarget = 999;
    
    public int Value { get; init; }

    public Target(int target)
    {
        if (target is < MinTarget or > MaxTarget)
        {
            throw new ArgumentOutOfRangeException(
                nameof(target),
                $"Target must be between {MinTarget} and {MaxTarget}.");
        }

        Value = target;
    }

    private static readonly Random _random = new();
    
    public static Target Random()
    {
        var randomTarget = _random.Next(MinTarget, MaxTarget);
        return new Target(randomTarget);
    }
}