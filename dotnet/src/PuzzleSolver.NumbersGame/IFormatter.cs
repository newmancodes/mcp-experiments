namespace PuzzleSolver.NumbersGame;

public interface IFormatter<in T>
{
    string Format(T value);
}