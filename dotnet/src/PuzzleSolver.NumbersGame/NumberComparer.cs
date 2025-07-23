namespace PuzzleSolver.NumbersGame;

internal class NumberComparer : IComparer<Number>
{
    public int Compare(Number? x, Number? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (y is null)
        {
            return 1;
        }

        if (x is null)
        {
            return -1;
        }
        
        var valueComparison = x.Value.CompareTo(y.Value);

        if (valueComparison != 0)
        {
            return valueComparison;
        }
        
        return x.Category.CompareTo(y.Category);
    }
}