namespace PuzzleSolver.NumbersGame;

internal interface IFormatter<in T>
{
    string Format(T value);
}