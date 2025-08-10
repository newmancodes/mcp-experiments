using Shouldly;

namespace PuzzleSolver.NumbersGame.Test;

[Trait("Category", "Unit")]
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

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 12, 3)]
    [InlineData(new[] { 1, 4, 4, 5, 6, 50 }, 350, 4)]
    [InlineData(new[] { 1, 3, 3, 8, 9, 50 }, 410, 5)]
    [InlineData(new[] { 2, 3, 3, 5, 6, 75 }, 277, 6)]
    [InlineData(new[] { 1, 10, 25, 50, 75, 100 }, 813, 7)]
    public void A_Possible_Puzzle_Is_Reported_As_Such_With_Solution(int[] numbers, int target, int expectedSolutionSteps)
    {
        // Arrange
        var sut = new Solver();

        // Act
        var result = sut.Solve(numbers, new Target(target));

        // Assert
        result.SolutionFound.ShouldBeTrue();
        result.Instructions.Count.ShouldBe(expectedSolutionSteps);
    }
}