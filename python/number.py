from dataclasses import dataclass
from enum import Enum
from typing import Self

class NumberCategory(Enum):
    SMALL = "small"
    LARGE = "large"
    CUSTOM = "custom"

class Number:
    def __init__(self, value: int) -> None:
        """
        Initializes a Number with a value and determines its category.
        
        :param value: The integer value of the number.
        """
        self._value = value

        if 1 <= value <= 10:
            self._category = NumberCategory.SMALL
        elif value in [25, 50, 75, 100]:
            self._category = NumberCategory.LARGE
        else:
            self._category = NumberCategory.CUSTOM

    @property
    def value(self) -> int:
        return self._value
    
    @property
    def category(self) -> NumberCategory:
        return self._category
    
    def __eq__(self, other: object) -> bool:
        """
        Checks equality with another Number.
        
        :param other: Another Number instance.
        :return: True if both numbers should be considered equal, False otherwise.
        """

        if not isinstance(other, Number):
            return False

        return self.value == other.value
    
    def __hash__(self) -> int:
        """
        Returns a hash of the number.
        
        :return: The hash value of the number.
        """
        return hash(self.value)
    
    def __str__(self) -> str:
        """
        Returns a string representation of the number.
        
        :return: The string representation of the number.
        """
        return f"{self.value} ({self.category.value.title()})"
