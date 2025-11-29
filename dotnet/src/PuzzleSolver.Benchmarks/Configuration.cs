using PuzzleSolver.NumbersGame;

namespace PuzzleSolver.Benchmarks;

public record Configuration(ConfigurationDifficulty Difficulty, Board Board, Target Target)
{
    public string Name => Enum.GetName(Difficulty)!;
}

public enum ConfigurationDifficulty
{
    Solved,
    Easy,
    Medium,
    Hard,
    Impossible
}