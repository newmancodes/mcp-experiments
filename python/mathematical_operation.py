from dataclasses import dataclass

from number import Number
from operators import Operators


@dataclass
class MathematicalOperation:
    left_operand: Number
    operator: Operators
    right_operand: Number
    result: Number
