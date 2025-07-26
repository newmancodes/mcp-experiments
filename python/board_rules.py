import sys

from number import Number, NumberCategory


class _BoardRules:
    @staticmethod
    def starting_size() -> int:
        """
        Returns the starting size of the board.

        :return: The starting size of the board.
        """
        return 6

    @staticmethod
    def reuse_limit(number: Number) -> int:
        """
        Determines the reuse limit for a number based on its category.

        :param number: The Number instance to check.
        :return: The maximum number of times this number can be reused.
        """
        if number.category == NumberCategory.SMALL:
            return 2
        elif number.category == NumberCategory.LARGE:
            return 1
        else:
            return sys.maxsize
