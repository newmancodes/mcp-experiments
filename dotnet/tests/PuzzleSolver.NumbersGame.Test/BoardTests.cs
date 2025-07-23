using Shouldly;

namespace PuzzleSolver.NumbersGame.Test;

public class BoardTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, "1, 2, 3, 4, 5, 6")]
    [InlineData(new[] { 1, 1, 2, 2, 3, 3 }, "1, 1, 2, 2, 3, 3")]
    [InlineData(new[] { 7, 8, 9, 10, 25, 50 }, "7, 8, 9, 10, 25, 50")]
    [InlineData(new[] { 3, 6, 25, 50, 75, 100 }, "3, 6, 25, 50, 75, 100")]
    public void Valid_Boards_Can_Be_Created(int[] numbers, string expectedStringRepresentation)
    {
        // Arrange

        // Act
        Board board = numbers;

        // Assert
        board.Count.ShouldBe(6);
        board.ToString().ShouldBe(expectedStringRepresentation);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 })]
    public void Boards_Must_Have_Six_Numbers(int[] numbers)
    {
        // Arrange
        
        // Act
        Action act = () => _ = (Board)numbers;

        // Assert
        var ex = act.ShouldThrow<IllegalBoardException>();
        ex.Message.ShouldBe("Boards require six numbers.");
    }

    [Theory]
    [InlineData(11)]
    [InlineData(34)]
    [InlineData(101)]
    public void Boards_Can_Only_Use_Valid_Numbers(int number)
    {
        // Arrange
        var numbers = new[] { 1, 2, 3, 4, 5, number };
        
        // Act
        Action act = () => _ = (Board)numbers;

        // Assert
        var ex = act.ShouldThrow<IllegalBoardException>();
        ex.Message.ShouldBe($"The number '{number}' is not a valid board number.");
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    public void Boards_May_Not_Reuse_Small_Numbers_More_Than_Twice(int smallNumber)
    {
        // Arrange
        var numbers = new[] { smallNumber, smallNumber, smallNumber, 25, 50, 75 };
        
        // Act
        Action act = () => _ = (Board)numbers;

        // Assert
        var ex = act.ShouldThrow<IllegalBoardException>();
        ex.Message.ShouldBe($"The number '{smallNumber}' has been used too many times.");
    }

    [Theory]
    [InlineData(25)]
    [InlineData(50)]
    [InlineData(75)]
    [InlineData(100)]
    public void Boards_May_Not_Reuse_Large_Numbers_More_Than_Once(int largeNumber)
    {
        // Arrange
        var number = new[] { largeNumber, largeNumber, 1, 2, 3, 4 };
        
        // Act
        Action act = () => _ = (Board)number;
        
        // Assert
        var ex = act.ShouldThrow<IllegalBoardException>();
        ex.Message.ShouldBe($"The number '{largeNumber}' has been used too many times.");
    }
}