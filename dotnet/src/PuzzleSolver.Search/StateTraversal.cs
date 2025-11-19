namespace PuzzleSolver.Search;

public record StateTraversal<TState, TStateTraversalDescription>(
    StateTraversal<TState, TStateTraversalDescription>? Parent,
    TStateTraversalDescription? TraversalDescription,
    TState Child);