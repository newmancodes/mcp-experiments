from collections.abc import Callable
from typing import TypeVar

T = TypeVar("T")
Predicate = Callable[[T], bool]
