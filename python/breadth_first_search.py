from collections import deque
from typing import Callable, Deque, Generic, Iterable, Set, TypeVar

from predicate import Predicate
from state_traversal import StateTraversal

TState = TypeVar("TState")
TStateTraversalDescription = TypeVar("TStateTraversalDescription")


class BreadthFirstSearch(Generic[TState, TStateTraversalDescription]):
    def __init__(self, success_indicator: Predicate[TState], next_state_generator: Callable[[StateTraversal[TState, TStateTraversalDescription]], Iterable[StateTraversal[TState, TStateTraversalDescription]]]) -> None:
        self._success_indicator = success_indicator
        self._next_state_generator = next_state_generator
        self._candidates: Deque[StateTraversal[TState, TStateTraversalDescription]] = deque()
        self._visited: Set[TState] = set()

    def execute(self, initial_state: TState) -> Iterable[StateTraversal[TState, TStateTraversalDescription]]:
        """Performs the search starting from the initial state."""
        
        self._candidates.append(StateTraversal(parent=None, description=None, child=initial_state))

        while self._candidates:
            candidate = self._candidates.popleft()
            self._visited.add(candidate.child)

            if self._success_indicator(candidate.child):
                yield candidate
            
            for additional_candidate in self._next_state_generator(candidate):
                if additional_candidate.child not in self._visited:
                    self._candidates.append(additional_candidate)
