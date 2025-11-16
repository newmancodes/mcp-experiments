using Shouldly;

namespace PuzzleSolver.Haystack.Tests;

[Trait("Category", "Unit")]
public class MagnetTests
{
    [Fact]
    public void An_empty_haystack_does_not_contain_the_needle()
    {
        // Arrange
        var haystack = string.Empty;
        var needle = "some_needle";
        var sut = new Magnet();

        // Act
        var result = sut.Find(needle, haystack);

        // Assert
        result.Needle.ShouldBe(needle);
        result.Haystack.ShouldBe(haystack);
        result.Instances.ShouldBe(0);
        result.Locations.ShouldBeEmpty();
    }

    [Fact]
    public void A_haystack_which_does_not_contain_the_needle_returns_zero_instances()
    {
        // Arrange
        var haystack = "some_haystack_that_does_not_contain_the_needle";
        var needle = "some_needle";
        var sut = new Magnet();
        
        // Act
        var result = sut.Find(needle, haystack);
        
        // Assert
        result.Needle.ShouldBe(needle);
        result.Haystack.ShouldBe(haystack);
        result.Instances.ShouldBe(0);
        result.Locations.ShouldBeEmpty();
    }
    
    [Fact]
    public void A_haystack_with_multiple_occurrences_of_the_needle_returns_all_locations()
    {
        // Arrange
        var haystack = "needle_in_a_needle_haystack_with_multiple_needles";
        var needle = "needle";
        var sut = new Magnet();
        
        // Act
        var result = sut.Find(needle, haystack);
        
        // Assert
        result.Needle.ShouldBe(needle);
        result.Haystack.ShouldBe(haystack);
        result.Instances.ShouldBe(3);
        result.Locations.ShouldBe([0, 12, 42]);
    }
    
    [Fact]
    public void A_haystack_with_overlapping_occurrences_of_the_needle_returns_all_locations()
    {
        // Arrange
        var haystack = "wowowow";
        var needle = "wow";
        var sut = new Magnet();
        
        // Act
        var result = sut.Find(needle, haystack);
        
        // Assert
        result.Needle.ShouldBe(needle);
        result.Haystack.ShouldBe(haystack);
        result.Instances.ShouldBe(2);
        result.Locations.ShouldBe([0, 4]);
    }
}