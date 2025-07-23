namespace PuzzleSolver.BreadthFirstSearch;

public class BreadthFirstSearch<TState, TStateTraversalDescription>
    where TStateTraversalDescription : class
{
    private readonly Predicate<TState> _successIndicator;
    private readonly Func<StateTraversal<TState, TStateTraversalDescription>, IEnumerable<StateTraversal<TState, TStateTraversalDescription>>> _nextStateGenerator;
    private readonly Queue<StateTraversal<TState, TStateTraversalDescription>> _candidates;
    private readonly HashSet<TState> _visited;

    public BreadthFirstSearch(
        Predicate<TState> successIndicator,
        Func<StateTraversal<TState, TStateTraversalDescription>, IEnumerable<StateTraversal<TState, TStateTraversalDescription>>> nextStateGenerator,
        IEqualityComparer<TState> stateComparer)
    {
        _successIndicator = successIndicator;
        _nextStateGenerator = nextStateGenerator;
        _candidates = new Queue<StateTraversal<TState, TStateTraversalDescription>>();
        _visited = new HashSet<TState>(stateComparer);
    }

    public IEnumerable<StateTraversal<TState, TStateTraversalDescription>> Execute(TState initialState)
    {
        _candidates.Enqueue(new StateTraversal<TState, TStateTraversalDescription>(
            null, 
            null, 
            initialState));

        while (_candidates.TryDequeue(out var candidate))
        {
            _visited.Add(candidate.Child);

            if (_successIndicator(candidate.Child))
            {
                yield return candidate;
            }

            foreach (var additionalCandidate in _nextStateGenerator(candidate))
            {
                if (!_visited.Contains(additionalCandidate.Child))
                {
                    _candidates.Enqueue(additionalCandidate);
                }
            }
        }
    }
}