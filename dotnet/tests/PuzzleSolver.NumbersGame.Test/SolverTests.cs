using System.Text;
using Shouldly;

namespace PuzzleSolver.NumbersGame.Test;

[Trait("Category", "Unit")]
public class SolverTests
{
    [Theory]
    [InlineData(SearchStrategy.BreadthFirst)]
    [InlineData(SearchStrategy.DepthFirst)]
    [InlineData(SearchStrategy.IterativeDepthFirst)]
    public void An_Impossible_Puzzle_Is_Reported_As_Such(SearchStrategy searchStrategy)
    {
        // Arrange
        var board = Board.From(new[] { 3, 7, 6, 2, 1, 7 });
        var target = new Target(824);
        var sut = new Solver();

        // Act
        var result = sut.Solve(board, target, searchStrategy);

        // Assert
        result.SolutionFound.ShouldBeFalse();
        result.Instructions.Count.ShouldBe(0);
    }

    [Theory]
    [InlineData(SearchStrategy.BreadthFirst)]
    [InlineData(SearchStrategy.DepthFirst)]
    [InlineData(SearchStrategy.IterativeDepthFirst)]
    public void An_Already_Solved_Puzzle_Is_Reported_As_Such_With_Solution(SearchStrategy searchStrategy)
    {
        // Arrange
        var board = Board.From(new[] { 1, 2, 3, 4, 5, 100 });
        var target = new Target(100);
        var sut = new Solver();
        
        // Act
        var result = sut.Solve(board, target, searchStrategy);
        
        // Assert
        result.SolutionFound.ShouldBeTrue();
        result.Instructions.Count.ShouldBe(2);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 12, SearchStrategy.BreadthFirst, 3)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 12, SearchStrategy.DepthFirst, 4)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 12, SearchStrategy.IterativeDepthFirst, 3)]
    [InlineData(new[] { 1, 4, 4, 5, 6, 50 }, 350, SearchStrategy.BreadthFirst, 4)]
    [InlineData(new[] { 1, 4, 4, 5, 6, 50 }, 350, SearchStrategy.DepthFirst, 7)]
    [InlineData(new[] { 1, 4, 4, 5, 6, 50 }, 350, SearchStrategy.IterativeDepthFirst, 4)]
    [InlineData(new[] { 1, 3, 3, 8, 9, 50 }, 410, SearchStrategy.BreadthFirst, 5)]
    [InlineData(new[] { 1, 3, 3, 8, 9, 50 }, 410, SearchStrategy.DepthFirst, 7)]
    [InlineData(new[] { 1, 3, 3, 8, 9, 50 }, 410, SearchStrategy.IterativeDepthFirst, 5)]
    [InlineData(new[] { 2, 3, 3, 5, 6, 75 }, 277, SearchStrategy.BreadthFirst, 6)]
    [InlineData(new[] { 2, 3, 3, 5, 6, 75 }, 277, SearchStrategy.DepthFirst, 7)]
    [InlineData(new[] { 2, 3, 3, 5, 6, 75 }, 277, SearchStrategy.IterativeDepthFirst, 6)]
    [InlineData(new[] { 1, 10, 25, 50, 75, 100 }, 813, SearchStrategy.BreadthFirst, 7)]
    [InlineData(new[] { 1, 10, 25, 50, 75, 100 }, 813, SearchStrategy.DepthFirst, 7)]
    [InlineData(new[] { 1, 10, 25, 50, 75, 100 }, 813, SearchStrategy.IterativeDepthFirst, 7)]
    public void A_Possible_Puzzle_Is_Reported_As_Such_With_Solution(int[] numbers, int target, SearchStrategy searchStrategy, int expectedSolutionSteps)
    {
        // Arrange
        var board = Board.From(numbers);
        
        var sut = new Solver();

        // Act
        var result = sut.Solve(board, new Target(target), searchStrategy);

        // Assert
        result.SolutionFound.ShouldBeTrue();
        result.Instructions.Count.ShouldBe(expectedSolutionSteps);
    }
}