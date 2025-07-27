from dataclasses import dataclass

from board import Board
from mathematical_operation import MathematicalOperation


@dataclass
class SolveInstruction:
    state: Board

@dataclass
class InitialSolveInstruction(SolveInstruction):
    pass

@dataclass
class AdditionalSolveInstruction(SolveInstruction):
    operation: MathematicalOperation
    previous_state: Board

@dataclass
class FinalSolveInstruction(SolveInstruction):
    pass