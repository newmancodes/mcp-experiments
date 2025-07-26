import random

class Target:
    """
    Represents the target for the puzzle.
    """

    @property
    def target(self) -> int:
        return self._target

    def __init__(self, target: int) -> None:
        """
        Initializes the Target with a specific target number.
        
        :param target: The target number for the puzzle.
        """
        if target < 1 or target > 999:
            raise ValueError("Target must be between 1 and 999.")

        self._target = target

    @staticmethod
    def random() -> 'Target':
        """
        Generates a random target number between 1 and 999.
        
        :return: A Target instance with a random target number.
        """
        return Target(random.randint(1, 999))
