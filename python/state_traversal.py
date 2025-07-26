from dataclasses import dataclass
from typing import Self, TypeVar, Generic

TState = TypeVar('TState')
TStateTraversalDescription = TypeVar('TStateTraversalDescription')

@dataclass
class StateTraversal(Generic[TState, TStateTraversalDescription]):
    def __init__(self, parent: Self | None, description: TStateTraversalDescription, child: TState) -> None:
        """
        Initializes the StateTraversal with a state and its description.
        
        :param state: The current state of the traversal.
        :param description: A description of the traversal.
        """
        self.parent = parent
        self.description = description
        self.child = child
