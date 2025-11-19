using PuzzleSolver.Search;

namespace PuzzleSolver.BreadthFirstSearch;

public class BreadthFirstSearch<TState, TStateTraversalDescription> : ISearch<TState, TStateTraversalDescription>
    where TStateTraversalDescription : class
{
    private readonly Predicate<TState> _successIndicator;
    private readonly Func<StateTraversal<TState, TStateTraversalDescription>, IEnumerable<StateTraversal<TState, TStateTraversalDescription>>> _nextStateGenerator;
    private readonly Queue<StateTraversal<TState, TStateTraversalDescription>> _frontier;
    private readonly HashSet<TState> _explored;
    private readonly IEqualityComparer<TState> _stateComparer;

    public BreadthFirstSearch(
        Predicate<TState> successIndicator,
        Func<StateTraversal<TState, TStateTraversalDescription>, IEnumerable<StateTraversal<TState, TStateTraversalDescription>>> nextStateGenerator,
        IEqualityComparer<TState> stateComparer)
    {
        _successIndicator = successIndicator;
        _nextStateGenerator = nextStateGenerator;
        _frontier = new Queue<StateTraversal<TState, TStateTraversalDescription>>();
        _explored = new HashSet<TState>(stateComparer);
        _stateComparer = stateComparer;
    }

    public IEnumerable<StateTraversal<TState, TStateTraversalDescription>> Execute(TState initialState)
    {
        _frontier.Enqueue(new StateTraversal<TState, TStateTraversalDescription>(
            null, 
            null, 
            initialState));
        var maxLength = 0;

        while (_frontier.TryDequeue(out var candidate))
        {
            _explored.Add(candidate.Child);

            if (_successIndicator(candidate.Child))
            {
                yield return candidate;
            }

            foreach (var additionalCandidate in _nextStateGenerator(candidate))
            {
                if (!_frontier.Select(f => f.Child).Any(s => _stateComparer.Equals(s, additionalCandidate.Child)) 
                    && !_explored.Contains(additionalCandidate.Child))
                {
                    _frontier.Enqueue(additionalCandidate);
                    if (_frontier.Count > maxLength)
                    {
                        maxLength = _frontier.Count;
                    }
                }
            }
        }
    }
}