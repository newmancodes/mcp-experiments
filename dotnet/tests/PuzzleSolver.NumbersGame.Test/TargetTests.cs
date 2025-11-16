using System.ComponentModel;
using Shouldly;

namespace PuzzleSolver.NumbersGame.Test;

[Trait("Category", "Unit")]
public class TargetTests
{
    [Fact]
    public void Target_May_Not_Be_Zero()
    {
        // Arrange
        var target = 0;

        // Act
        Action act = () => { _ = new Target(target); };

        // Assert
        var ex = act.ShouldThrow<ArgumentOutOfRangeException>();
        ex.ParamName.ShouldBe("target");
        ex.Message.ShouldStartWith("Target must be between 1 and 999.");
    }

    [Fact]
    public void Target_Must_Not_Be_Negative()
    {
        // Arrange
        var target = -1;

        // Act
        Action act = () => { _ = new Target(target); };

        // Assert
        var ex = act.ShouldThrow<ArgumentOutOfRangeException>();
        ex.ParamName.ShouldBe("target");
        ex.Message.ShouldStartWith("Target must be between 1 and 999.");
    }

    [Fact]
    public void Target_Must_Not_Be_Greater_Than_999()
    {
        // Arrange
        var target = 1_000;

        // Act
        Action act = () => { _ = new Target(target); };

        // Assert
        var ex = act.ShouldThrow<ArgumentOutOfRangeException>();
        ex.ParamName.ShouldBe("target");
        ex.Message.ShouldStartWith("Target must be between 1 and 999.");
    }

    [Fact]
    public void Target_Can_Be_Randomly_Generated()
    {
        // Arrange
        
        // Act
        var target = Target.Random();
        
        // Assert
        target.Value.ShouldBeInRange(1, 999);
    }
}