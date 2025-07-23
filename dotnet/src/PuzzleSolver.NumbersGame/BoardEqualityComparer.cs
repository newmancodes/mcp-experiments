namespace PuzzleSolver.NumbersGame;

internal class BoardEqualityComparer : IEqualityComparer<Board>
{
    public bool Equals(Board? x, Board? y)
    {
        if (x is null && y is null)
            return true;

        if (x is null || y is null)
            return false;

        return x.ToString() == y.ToString();
    }

    public int GetHashCode(Board obj)
    {
        return obj.ToString().GetHashCode();
    }
}