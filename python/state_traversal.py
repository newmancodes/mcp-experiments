from dataclasses import dataclass
from typing import Generic, Self, TypeVar

TState = TypeVar("TState")
TStateTraversalDescription = TypeVar("TStateTraversalDescription")

@dataclass
class StateTraversal(Generic[TState, TStateTraversalDescription]):
        parent: Self | None
        description: TStateTraversalDescription | None
        child: TState
