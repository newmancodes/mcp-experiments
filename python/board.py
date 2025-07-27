from dataclasses import dataclass
from typing import Iterable, Self

from board_rules import _BoardRules
from exceptions import InvalidBoardSizeError, InvalidNumberError, NumberReuseError
from mathematical_operation import MathematicalOperation
from number import Number, NumberCategory
from operators import Operators
from target import Target


class Board:
    def __init__(self, numbers: list[Number]) -> None:
        """
        Initializes the Board with a list of numbers.

        :param numbers: A list of integers representing the board.
        """
        numbers.sort(key=lambda num: num.value)
        self._numbers = tuple(numbers)
        self._string_representation = ", ".join(str(number.value) for number in self._numbers)

    def __eq__(self, other: object) -> bool:
        """
        Checks equality with another Board.

        :param other: Another Board instance.
        :return: True if both boards should be considered equal, False otherwise.
        """
        if not isinstance(other, Board):
            return False

        return str(self) == str(other)
    
    def __hash__(self) -> int:
        return hash(str(self))

    def __str__(self) -> str:
        return self._string_representation
    
    def is_solved(self, target: Target) -> bool:
        """
        Checks if the board is solved for the given target.

        :param target: The target number to check against.
        :return: True if the board is solved, False otherwise.
        """
        return len(tuple(filter(lambda num: num.value == target.target, self._numbers))) > 0
    
    def _generate_possible_operations(self) -> Iterable["Board.PossibleAction"]:
        """
        Generates possible operations from the current board state.

        :return: An iterable of PossibleAction instances.
        """
        if len(self._numbers) == 1:
            return
        
        for i in range(len(self._numbers) - 1):
            for j in range(i + 1, len(self._numbers)):
                left_operand, right_operand = self._numbers[i], self._numbers[j]
                left_operand_value, right_operand_value = left_operand.value, right_operand.value

                # Add
                addition_result = Number(left_operand_value + right_operand_value)
                addition = MathematicalOperation(left_operand, Operators.ADDITION, right_operand, addition_result)
                yield Board.PossibleAction(addition, self._execute(addition))

                # Multiply
                multiplication_result = Number(left_operand_value * right_operand_value)
                multiplication = MathematicalOperation(left_operand, Operators.MULTIPLICATION, right_operand, multiplication_result)
                yield Board.PossibleAction(multiplication, self._execute(multiplication))
                                           
                # Subtract
                if left_operand_value != right_operand_value:
                    if left_operand_value > right_operand_value:
                        subtraction_result = Number(left_operand_value - right_operand_value)
                        subtraction = MathematicalOperation(left_operand, Operators.SUBTRACTION, right_operand, subtraction_result)
                        yield Board.PossibleAction(subtraction, self._execute(subtraction))
                    else:
                        subtraction_result = Number(right_operand_value - left_operand_value)
                        subtraction = MathematicalOperation(right_operand, Operators.SUBTRACTION, left_operand, subtraction_result)
                        yield Board.PossibleAction(subtraction, self._execute(subtraction))

                # Divide
                if left_operand_value == right_operand_value:
                    division_result = Number(1)
                    division = MathematicalOperation(left_operand, Operators.DIVISION, right_operand, division_result)
                    yield Board.PossibleAction(division, self._execute(division))
                elif left_operand_value > right_operand_value:
                    if left_operand_value % right_operand_value == 0:
                        division_result = Number(left_operand_value // right_operand_value)
                        division = MathematicalOperation(left_operand, Operators.DIVISION, right_operand, division_result)
                        yield Board.PossibleAction(division, self._execute(division))
                else:
                    if right_operand_value % left_operand_value == 0:
                        division_result = Number(right_operand_value // left_operand_value)
                        division = MathematicalOperation(right_operand, Operators.DIVISION, left_operand, division_result)
                        yield Board.PossibleAction(division, self._execute(division))

    def _execute(self, operation: MathematicalOperation) -> "Board":
        """
        Executes the given operation on the board and returns a new board state.

        :param operation: The mathematical operation to execute.
        :return: A new Board instance with the result of the operation.
        """
        new_numbers = list(self._numbers)
        new_numbers.remove(operation.left_operand)
        new_numbers.remove(operation.right_operand)
        new_numbers.append(operation.result)

        return Board(new_numbers)

    @classmethod
    def from_numbers(cls, numbers: list[int]) -> "Board":
        """
        Factory method to create a Board from a list of numbers.

        :param numbers: A list of integers representing the board.
        :return: An instance of Board.
        """
        if len(numbers) != _BoardRules.starting_size():
            raise InvalidBoardSizeError("Boards require six numbers.")

        builder = Board._BoardBuilder()
        for number in numbers:
            builder = builder.with_number(number)

        return builder.build()

    class _BoardBuilder:
        """
        Internal class for building a board.
        This is not part of the public API.
        """

        def __init__(self) -> None:
            self._numbers: list[Number] = []

        def with_number(self, number: int) -> Self:
            """
            Adds a number to the board being built.

            :param number: The number to add.
            :return: The builder instance for chaining.
            """
            new_number = Number(number)

            if new_number.category not in [NumberCategory.SMALL, NumberCategory.LARGE]:
                raise InvalidNumberError(f"The number {number} is not a valid board number.")

            number_limit = _BoardRules.reuse_limit(new_number)
            if self._numbers.count(new_number) >= number_limit:
                raise NumberReuseError(f"The number {number} has been used too many times.")

            self._numbers.append(new_number)
            return self

        def build(self) -> "Board":
            """
            Builds the board with the numbers added.

            :return: An instance of Board.
            """
            return Board(self._numbers)
        
    @dataclass
    class PossibleAction:
        operation: MathematicalOperation
        result: "Board"
