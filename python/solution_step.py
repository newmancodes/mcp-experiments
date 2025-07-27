from dataclasses import dataclass

from board import Board
from mathematical_operation import MathematicalOperation


@dataclass
class SolutionStep:
    source: Board
    operation: MathematicalOperation
    result: Board
