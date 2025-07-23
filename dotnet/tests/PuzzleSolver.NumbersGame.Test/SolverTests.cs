using Shouldly;

namespace PuzzleSolver.NumbersGame.Test;

public class SolverTests
{
    [Fact]
    public void An_Impossible_Puzzle_Is_Reported_As_Such()
    {
        // Arrange
        var board = new[] { 3, 7, 6, 2, 1, 7 };
        var target = new Target(824);
        var sut = new Solver();

        // Act
        var result = sut.Solve(board, target);

        // Assert
        result.SolutionFound.ShouldBeFalse();
        result.Instructions.Count.ShouldBe(0);
    }

    [Fact]
    public void An_Already_Solved_Puzzle_Is_Reported_As_Such_With_Solution()
    {
        // Arrange
        var board = new[] { 1, 2, 3, 4, 5, 100 };
        var target = new Target(100);
        var sut = new Solver();
        
        // Act
        var result = sut.Solve(board, target);
        
        // Assert
        result.SolutionFound.ShouldBeTrue();
        result.Instructions.Count.ShouldBe(2);
    }

    [Fact]
    public void A_Possible_Puzzle_Is_Reported_As_Such_With_Solution()
    {
        // Arrange
        var board = new[] { 1, 3, 3, 8, 9, 50 };
        var target = new Target(410);
        var sut = new Solver();

        // Act
        var result = sut.Solve(board, target);

        // Assert
        result.SolutionFound.ShouldBeTrue();
        result.Instructions.Count.ShouldBe(5);
    }
}