using PuzzleSolver.NumbersGame;

namespace PuzzleSolver.Benchmarks;

public record Configuration(ConfigurationDifficulty Difficulty, Board Board, Target Target)
{
    public string Name => Enum.GetName(Difficulty)!;

    public override string ToString()
    {
        return Name;
    }
}

public enum ConfigurationDifficulty
{
    AlreadySolved,
    Easy,
    Medium,
    Hard,
    Impossible
}