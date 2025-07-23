namespace PuzzleSolver.NumbersGame;

internal class BoardBuilder
{
    private readonly List<Number> _numbers = new(6);
        
    internal Board Build()
    {
        return new Board(_numbers);
    }

    internal BoardBuilder WithNumber(int number)
    {
        var newNumber = Number.Generate(number);

        if (newNumber.Category != NumberCategory.Small
            && newNumber.Category != NumberCategory.Large)
        {
            throw new IllegalBoardException($"The number '{number}' is not a valid board number.");
        }
        
        var numberLimit = BoardRules.ReuseLimit(newNumber);
        if (_numbers.Count(n => n.Value == newNumber.Value) >= numberLimit)
        {
            throw new IllegalBoardException($"The number '{number}' has been used too many times.");
        }

        _numbers.Add(newNumber);
        return this;
    }
}