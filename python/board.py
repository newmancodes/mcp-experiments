from typing import Self

from board_rules import _BoardRules
from number import Number, NumberCategory

class Board:
    def __init__(self, numbers: list[Number]) -> None:
        """
        Initializes the Board with a list of numbers.
        
        :param numbers: A list of integers representing the board.
        """
        self._numbers = numbers

    def __str__(self) -> str:
        return ', '.join(str(number.value) for number in self._numbers)
    
    @classmethod
    def from_numbers(cls, numbers: list[int]) -> Self:
        """
        Factory method to create a Board from a list of numbers.
        
        :param numbers: A list of integers representing the board.
        :return: An instance of Board.
        """
        if len(numbers) != _BoardRules.starting_size():
            raise ValueError("Boards require six numbers.")
        
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
            self._numbers = []

        def with_number(self, number: int) -> Self:
            """
            Adds a number to the board being built.
            
            :param number: The number to add.
            :return: The builder instance for chaining.
            """
            new_number = Number(number)

            if not new_number.category in [ NumberCategory.SMALL, NumberCategory.LARGE ]:
                raise ValueError(f"The number {number} is not a valid board number.")

            number_limit = _BoardRules.reuse_limit(new_number)
            if self._numbers.count(new_number) >= number_limit:
                raise ValueError(f"The number {number} has been used too many times.")

            self._numbers.append(new_number)
            return self
        
        def build(self) -> 'Board':
            """
            Builds the board with the numbers added.
            
            :return: An instance of Board.
            """
            return Board(self._numbers)
