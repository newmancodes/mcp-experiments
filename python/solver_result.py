from typing import Self

from board import Board
from solution import Solution
from solve_instruction import (
    AdditionalSolveInstruction,
    FinalSolveInstruction,
    InitialSolveInstruction,
    SolveInstruction,
)
from target import Target


class SolverResult:
    @property
    def board(self) -> Board:
        return self._board

    @property
    def target(self) -> Target:
        return self._target

    @property
    def solution_found(self) -> bool:
        return self._solution_found

    @property
    def instructions(self) -> list[SolveInstruction]:
        return self._instructions

    def __init__(self, board: Board, target: Target, solutions: list[Solution]) -> None:
        """
        Initializes the SolverResult with the board, target, and solutions.

        :param board: The board containing numbers.
        :param target: The target number to reach.
        :param solutions: A list of solutions found to reach the target.
        """

        self._board = board
        self._target = target
        solution = solutions[0] if solutions else None
        self._solution_found = solution is not None

        self._instructions: list[SolveInstruction] = []

        if solution is not None:
            self._instructions.append(InitialSolveInstruction(solution.start))
            final_state = solution.start

            for step in solution.steps:
                self._instructions.append(
                    AdditionalSolveInstruction(
                        step.result,
                        step.operation,
                        step.source))
                final_state = step.result

            self._instructions.append(FinalSolveInstruction(final_state))

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
