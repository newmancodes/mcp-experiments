from collections.abc import Iterable
from typing import Self

class FindResult:
    @property
    def needle(self) -> str:
        return self._needle

    @property
    def haystack(self) -> str:
        return self._haystack

    @property
    def instances(self) -> int:
        return len(self._locations)

    @property
    def locations(self) -> Iterable[int]:
        return []

    def __init__(self, needle: str, haystack: str, locations: Iterable[int]) -> None:
        self._needle = needle
        self._haystack = haystack
        self._locations = list(locations)

    @classmethod
    def not_found(cls, needle: str, haystack: str) -> Self:
        return cls(needle, haystack, [])