using PuzzleSolver.Search;

namespace PuzzleSolver.DepthFirstSearch;

public class DepthFirstSearch<TState, TStateTraversalDescription>
    where TStateTraversalDescription : class
{
    private readonly Predicate<TState> _successIndicator;

    private readonly
        Func<StateTraversal<TState, TStateTraversalDescription>,
            IEnumerable<StateTraversal<TState, TStateTraversalDescription>>> _nextStateGenerator;

    private readonly Stack<StateTraversal<TState, TStateTraversalDescription>> _frontier;
    private readonly HashSet<TState> _explored;
    private readonly IEqualityComparer<TState> _stateComparer;

    public DepthFirstSearch(
        Predicate<TState> successIndicator,
        Func<StateTraversal<TState, TStateTraversalDescription>, IEnumerable<StateTraversal<TState, TStateTraversalDescription>>> nextStateGenerator,
        IEqualityComparer<TState> stateComparer)
    {
        _successIndicator = successIndicator;
        _nextStateGenerator = nextStateGenerator;
        _frontier = new Stack<StateTraversal<TState, TStateTraversalDescription>>();
        _explored = new HashSet<TState>(stateComparer);
        _stateComparer = stateComparer;
    }

    public IEnumerable<StateTraversal<TState, TStateTraversalDescription>> Execute(TState initialState)
    {
        _frontier.Push(new StateTraversal<TState, TStateTraversalDescription>(
            null,
            null,
            initialState));

        while (_frontier.TryPop(out var candidate))
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
                    _frontier.Push(additionalCandidate);
                }
            }
        }
    }
}