class Target:
    """
    Represents the target for the puzzle.
    """

    def __init__(self, target: int) -> None:
        """
        Initializes the Target with a specific target number.
        
        :param target: The target number for the puzzle.
        """
        if target < 1 or target > 999:
            raise ValueError("Target must be between 1 and 999.")

        self._target = target
