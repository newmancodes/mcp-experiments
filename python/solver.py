from board import Board
from solver_result import SolverResult
from target import Target

class Solver:
    def solve(self, board: Board, target: Target) -> SolverResult:
        """
        Solves the board to reach the target number.
        
        :param board: The board containing numbers.
        :param target: The target number to reach.
        :return: True if the target can be reached, False otherwise.
        """
        return SolverResult.unsolvable(board, target)
        