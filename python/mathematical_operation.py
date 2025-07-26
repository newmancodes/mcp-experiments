from dataclasses import dataclass
from number import Number
from operators import Operator

@dataclass
class MathematicalOperation:
    def __init__(self, leftOperand: Number, operator: Operator, rightOperand: Number, result: Number) -> None:
        self.leftOperand = leftOperand
        self.operator = operator
        self.rightOperand = rightOperand
        self.result = result
