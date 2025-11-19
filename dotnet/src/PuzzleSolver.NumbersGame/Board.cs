using Humanizer;

namespace PuzzleSolver.NumbersGame;

public class Board
{
    private readonly IReadOnlyList<Number> _numbers;
    private readonly Lazy<string> _formatter;
    
    public int Count => _numbers.Count;
    
    public IEnumerable<Number> Options => _numbers;
    
    internal Board(IEnumerable<Number> numbers)
    {
        _numbers = numbers.Order(new NumberComparer()).ToList().AsReadOnly();
        _formatter = new Lazy<string>(() =>
        {
            var formatter = new CsvBoardFormatter();
            return formatter.Format(this);
        });
    }

    public override string ToString()
    {
        return _formatter.Value;
    }

    internal IEnumerable<PossibleAction> GeneratePossibleActions()
    {
        if (_numbers.Count == 1)
        {
            // Require at least two options to generate possible actions
            yield break;
        }

        for (var i = 0; i < _numbers.Count - 1; i++)
        {
            for (var j = i + 1; j < _numbers.Count; j++)
            {
                var ithOperand = _numbers[i];
                var ithOperandValue = ithOperand.Value;
                var jthOperand = _numbers[j];
                var jthOperandValue = jthOperand.Value;
            
                // Add
                var additionResult = Number.Generate(ithOperandValue + jthOperandValue);
                var addition = new MathematicalOperation(ithOperand, Operator.Addition, jthOperand, additionResult);
                yield return new PossibleAction(addition, Execute(addition));

                // Multiply
                var multiplicationResult = Number.Generate(ithOperandValue * jthOperandValue);
                var multiplication = new MathematicalOperation(ithOperand, Operator.Multiplication, jthOperand, multiplicationResult);
                yield return new PossibleAction(multiplication, Execute(multiplication));

                // Subtract
                if (ithOperandValue != jthOperandValue)
                {
                    if (ithOperandValue > jthOperandValue)
                    {
                        var subtractionResult = Number.Generate(ithOperandValue - jthOperandValue);
                        var subtraction = new MathematicalOperation(ithOperand, Operator.Subtraction, jthOperand, subtractionResult);
                        yield return new PossibleAction(subtraction, Execute(subtraction));
                    }
                    else
                    {
                        var subtractionResult = Number.Generate(jthOperandValue - ithOperandValue);
                        var subtraction = new MathematicalOperation(jthOperand, Operator.Subtraction, ithOperand, subtractionResult);
                        yield return new PossibleAction(subtraction, Execute(subtraction));
                    }
                }

                // Divide
                if (ithOperandValue == jthOperandValue)
                {
                    var divisionResult = Number.Generate(1);
                    var division = new MathematicalOperation(ithOperand, Operator.Division, jthOperand, divisionResult);
                    yield return new PossibleAction(division, Execute(division));
                }
                else if (ithOperandValue > jthOperandValue)
                {
                    if (ithOperandValue % jthOperandValue == 0)
                    {
                        var divisionResult = Number.Generate(ithOperandValue / jthOperandValue);
                        var division = new MathematicalOperation(ithOperand, Operator.Division, jthOperand, divisionResult);
                        yield return new PossibleAction(division, Execute(division));
                    }
                }
                else
                {
                    if (jthOperandValue % ithOperandValue == 0)
                    {
                        var divisionResult = Number.Generate(jthOperandValue / ithOperandValue);
                        var division = new MathematicalOperation(jthOperand, Operator.Division, ithOperand, divisionResult);
                        yield return new PossibleAction(division, Execute(division));
                    }
                }
            }
        }
    }

    private Board Execute(MathematicalOperation operation)
    {
        var newNumbers = new List<Number>(_numbers);
        var leftOperandIndex = newNumbers.FindIndex(0, n => n == operation.LeftOperand);
        newNumbers.RemoveAt(leftOperandIndex);
        var rightOperandIndex = newNumbers.FindIndex(0, n => n == operation.RightOperand);
        newNumbers.RemoveAt(rightOperandIndex);
        newNumbers.Add(operation.Result);
        return new Board(newNumbers);
    }

    public static Board From(int[] numbers)
    {
        if (numbers.Length != BoardRules.StartingSize)
        {
            throw new IllegalBoardException($"Boards require {BoardRules.StartingSize.ToWords()} numbers.");
        }
        
        var builder = new BoardBuilder();
        foreach (var number in numbers)
        {
            builder = builder.WithNumber(number);
        }

        return builder.Build();
    }
}