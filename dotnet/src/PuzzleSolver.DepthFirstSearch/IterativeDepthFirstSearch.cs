using PuzzleSolver.Search;

namespace PuzzleSolver.DepthFirstSearch;

public class IterativeDepthFirstSearch<TState, TStateTraversalDescription> : ISearch<TState, TStateTraversalDescription>
    where TStateTraversalDescription : class
{
    private readonly Predicate<TState> _successIndicator;

    private readonly Func<StateTraversal<TState, TStateTraversalDescription>, IEnumerable<StateTraversal<TState, TStateTraversalDescription>>> _nextStateGenerator;

    private Stack<StateTraversal<TState, TStateTraversalDescription>>? _frontier;
    private HashSet<TState>? _explored;
    private readonly Func<StateTraversal<TState, TStateTraversalDescription>, int> _calculateDepth;
    private readonly int _maximumDepth;
    private readonly IEqualityComparer<TState> _stateComparer;

    public IterativeDepthFirstSearch(
        Predicate<TState> successIndicator,
        Func<StateTraversal<TState, TStateTraversalDescription>, IEnumerable<StateTraversal<TState, TStateTraversalDescription>>> nextStateGenerator,
        Func<StateTraversal<TState, TStateTraversalDescription>, int> calculateDepth,
        int maximumDepth,
        IEqualityComparer<TState> stateComparer)
    {
        _successIndicator = successIndicator;
        _nextStateGenerator = nextStateGenerator;
        _calculateDepth = calculateDepth;
        _maximumDepth = maximumDepth;
        _stateComparer = stateComparer;
    }

    public IEnumerable<StateTraversal<TState, TStateTraversalDescription>> Execute(TState initialState)
    {
        var depthLimit = 1;
        
        while (depthLimit < _maximumDepth)
        {
            _frontier = new Stack<StateTraversal<TState, TStateTraversalDescription>>();
            _explored = new HashSet<TState>(_stateComparer);

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
                    if (_calculateDepth(candidate) < depthLimit
                        && !_frontier.Select(f => f.Child).Any(s => _stateComparer.Equals(s, additionalCandidate.Child))
                        && !_explored.Contains(additionalCandidate.Child))
                    {
                        _frontier.Push(additionalCandidate);
                    }
                }
            }
            
            depthLimit++;
        }
    }
}