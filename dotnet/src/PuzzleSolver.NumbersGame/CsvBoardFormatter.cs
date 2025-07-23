namespace PuzzleSolver.NumbersGame;

internal class CsvBoardFormatter : IFormatter<Board>
{
    public string Format(Board value)
    {
        return value.Options.Aggregate(
            string.Empty,
            (working, n) =>
            {
                if (working == string.Empty)
                {
                    return $"{n.Value}";
                }
                
                return string.Concat(working, ", ", n.Value);
            });
    }
}