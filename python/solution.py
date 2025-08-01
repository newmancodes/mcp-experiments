from collections import deque
from collections.abc import Iterable

from board import Board
from mathematical_operation import MathematicalOperation
from solution_step import SolutionStep
from state_traversal import StateTraversal


class Solution:
    @property
    def start(self) -> Board:
        return self._start

    @property
    def steps(self) -> Iterable[SolutionStep]:
        return self._steps

    def __init__(self, traversal: StateTraversal[Board, MathematicalOperation]) -> None:
        steps: deque[SolutionStep] = deque()

        while traversal.parent and traversal.description:
            step = SolutionStep(
                source=traversal.parent.child,
                operation=traversal.description,
                result=traversal.child,
            )
            steps.appendleft(step)
            traversal = traversal.parent

        self._start = traversal.child
        self._steps = steps
