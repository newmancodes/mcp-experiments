import random
from dataclasses import dataclass
from typing import Self

@dataclass(frozen=True)
class Target:
    """
    Represents the target for the puzzle.
    """

    target: int

    def __post_init__(self) -> None:
        """
        Validates the Target after initialisation
        """
        if not 1 <= self.target <= 999:
            raise ValueError("Target must be between 1 and 999.")

    @classmethod
    def random(cls) -> Self:
        """
        Generates a random target number between 1 and 999.
        
        :return: A Target instance with a random target number.
        """
        return cls(random.randint(1, 999))
