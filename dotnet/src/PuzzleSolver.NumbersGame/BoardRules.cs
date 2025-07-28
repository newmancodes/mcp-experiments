namespace PuzzleSolver.NumbersGame;

public static class BoardRules
{
    internal const int StartingSize = 6;

    public static int ReuseLimit(Number number)
    {
        return BoardRules.ReuseLimit(number.Category);
    }

    public static int ReuseLimit(NumberCategory category)
    {
        return category switch
        {
            NumberCategory.Large => 1,
            NumberCategory.Small => 2,
            _ => int.MaxValue
        };
    }
}