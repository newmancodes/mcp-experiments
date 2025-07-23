namespace PuzzleSolver.NumbersGame;

internal static class BoardRules
{
    internal const int StartingSize = 6;

    internal static int ReuseLimit(Number number)
    {
        return number.Category switch
        {
            NumberCategory.Large => 1,
            NumberCategory.Small => 2,
            _ => int.MaxValue
        };
    }
}