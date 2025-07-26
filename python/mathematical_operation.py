from dataclasses import dataclass

from number import Number
from operators import Operator


@dataclass
class MathematicalOperation:
    left_operand: Number
    operator: Operator
    right_operand: Number
    result: Number
