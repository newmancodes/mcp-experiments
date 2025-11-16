namespace PuzzleSolver.Common;

public interface IFormatter<in T>
{
    string Format(T value);
}