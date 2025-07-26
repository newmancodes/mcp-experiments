from typing import Self
from board import Board
from solution import Solution
from target import Target


class SolverResult:
    @property
    def solution_found(self) -> bool:
        return self._solution is not None

    @property
    def instructions(self) -> list[str]:
        return []

    def __init__(self, board: Board, target: Target, solutions: list[Solution]) -> None:
        """
        Initializes the SolverResult with the board, target, and solutions.

        :param board: The board containing numbers.
        :param target: The target number to reach.
        :param solutions: A list of solutions found to reach the target.
        """

        self._board = board
        self._target = target
        self._solution = solutions[0] if solutions else None

    @classmethod
    def unsolvable(cls, board: Board, target: Target) -> Self:
        """
        Factory method to create an unsolvable SolverResult.

        :param board: The board containing numbers.
        :param target: The target number to reach.
        :return: An instance of SolverResult indicating no solutions found.
        """
        return cls(board, target, [])

    @classmethod
    def with_solutions(
        cls, board: Board, target: Target, solutions: list[Solution]
    ) -> Self:
        """
        Factory method to create a SolverResult with solutions.

        :param board: The board containing numbers.
        :param target: The target number to reach.
        :param solutions: A list of solutions found to reach the target.
        :return: An instance of SolverResult with the provided solutions.
        """
        return cls(board, target, solutions)
