from collections.abc import Iterable

from board import Board
from breadth_first_search import BreadthFirstSearch
from mathematical_operation import MathematicalOperation
from solution import Solution
from solver_result import SolverResult
from state_traversal import StateTraversal
from target import Target


class Solver:
    def solve(self, board: Board, target: Target) -> SolverResult:
        """
        Solves the board to reach the target number.

        :param board: The board containing numbers.
        :param target: The target number to reach.
        :return: The result of the solving attempt.
        """
        search = BreadthFirstSearch[Board, MathematicalOperation](
            lambda b: b.is_solved(target),
            lambda t: self._generate_possible_actions(t)
        )

        for successful_state_traversal in search.execute(board):
            solution = Solution(successful_state_traversal)
            return SolverResult.with_solutions(board, target, [ solution ])

        return SolverResult.unsolvable(board, target)

    def _generate_possible_actions(
            self,
            traversal: StateTraversal[Board, MathematicalOperation]
    ) -> Iterable[StateTraversal[Board, MathematicalOperation]]:
        """
        Generates possible actions from the current state traversal.

        :param traversal: The current state traversal.
        :return: An iterable of possible state traversals.
        """

        for possible_action in traversal.child._generate_possible_operations():
            yield StateTraversal(
                parent=traversal,
                description=possible_action.operation,
                child=possible_action.result
            )
