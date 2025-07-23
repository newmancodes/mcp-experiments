namespace PuzzleSolver.NumbersGame;

public class IllegalBoardException : Exception
{
    public IllegalBoardException(string message) : base(message)
    {
    }
}