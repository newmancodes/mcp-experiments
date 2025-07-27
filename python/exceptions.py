"""
Custom exception types for the puzzle solver domain.

This module defines a hierarchy of exceptions that provide more specific
error handling for different failure modes in the puzzle solver.
"""


class PuzzleSolverError(Exception):
    """
    Base exception for all puzzle solver related errors.

    This is the root of the exception hierarchy and should be used
    for catching any puzzle solver related error.
    """


class BoardValidationError(PuzzleSolverError):
    """
    Base exception for board construction and validation errors.

    Raised when there are issues with board creation, including
    invalid numbers, wrong counts, or rule violations.
    """


class InvalidBoardSizeError(BoardValidationError):
    """
    Raised when a board is created with an incorrect number of numbers.

    The Countdown Numbers Game requires exactly 6 numbers on the board.
    """


class InvalidNumberError(BoardValidationError):
    """
    Raised when an invalid number is used in board construction.

    Valid numbers are:
    - Small numbers: 1-10
    - Large numbers: 25, 50, 75, 100
    """


class NumberReuseError(BoardValidationError):
    """
    Raised when a number is reused beyond the allowed limit.

    Game rules:
    - Small numbers (1-10): Can be used up to 2 times
    - Large numbers (25, 50, 75, 100): Can be used up to 1 time
    """


class TargetValidationError(PuzzleSolverError):
    """
    Raised when an invalid target value is specified.

    Valid targets must be between 1 and 999 inclusive.
    """
