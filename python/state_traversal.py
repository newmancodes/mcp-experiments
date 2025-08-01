from dataclasses import dataclass
from typing import Self, TypeVar

TState = TypeVar("TState")
TStateTraversalDescription = TypeVar("TStateTraversalDescription")

@dataclass
class StateTraversal[TState, TStateTraversalDescription]:
        parent: Self | None
        description: TStateTraversalDescription | None
        child: TState
