namespace PuzzleSolver.Search;

public interface ISearch<TState, TStateTraversalDescription>
{
    IEnumerable<StateTraversal<TState, TStateTraversalDescription>> Execute(TState initialState);
}