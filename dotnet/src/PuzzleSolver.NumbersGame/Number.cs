namespace PuzzleSolver.NumbersGame;

public class Number
{
    public int Value { get; init; }
    
    public NumberCategory Category { get; init; }

    private Number(int value) : this(value, NumberCategory.Custom)
    {
    } 
    
    private Number(int value, NumberCategory category)
    { 
        Value = value;
        Category = category;
    }

    public override string ToString()
    {
        return $"{Value} ({Category})";
    }

    internal static Number Generate(int number)
    {
        if (_commonNumbers.TryGetValue(number, out var commonNumber))
        {
            return commonNumber;
        }
        
        return new Number(number);
    }

    private static readonly IReadOnlyDictionary<int, Number> _commonNumbers = new Dictionary<int, Number>(14)
    {
        { 1, new Number(1, NumberCategory.Small) },
        { 2, new Number(2, NumberCategory.Small) },
        { 3, new Number(3, NumberCategory.Small) },
        { 4, new Number(4, NumberCategory.Small) },
        { 5, new Number(5, NumberCategory.Small) },
        { 6, new Number(6, NumberCategory.Small) },
        { 7, new Number(7, NumberCategory.Small) },
        { 8, new Number(8, NumberCategory.Small) },
        { 9, new Number(9, NumberCategory.Small) },
        { 10, new Number(10, NumberCategory.Small) },
        { 25, new Number(25, NumberCategory.Large) },
        { 50, new Number(50, NumberCategory.Large) },
        { 75, new Number(75, NumberCategory.Large) },
        { 100, new Number(100, NumberCategory.Large) }
    }.AsReadOnly();
}

public enum NumberCategory
{
    Small,
    Large,
    Custom
}