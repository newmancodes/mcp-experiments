from dataclasses import dataclass
from typing import Generic, Self, TypeVar

TState = TypeVar("TState")
TStateTraversalDescription = TypeVar("TStateTraversalDescription")


@dataclass
class StateTraversal(Generic[TState, TStateTraversalDescription]):
    def __init__(
        self,
        parent: Self | None,
        description: TStateTraversalDescription,
        child: TState,
    ) -> None:
        """
        Initializes the StateTraversal with a state and its description.

        :param parent: The traversal that lead to this state in the search space or None if this is the root.
        :param description: A description of the traversal.
        :param child: The child state resulting from the traversal.
        """
        self.parent = parent
        self.description = description
        self.child = child
