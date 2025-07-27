from typing import Iterable

import pytest

from board import Board
from solver import Solver
from target import Target


def test_impossible_puzzles_are_reported_as_such() -> None:
    """Test that impossible puzzles are reported correctly."""
    board = Board.from_numbers([3, 7, 6, 2, 1, 7])
    target = Target(824)
    sut = Solver()

    result = sut.solve(board, target)

    assert result.solution_found is False
    assert len(result.instructions) == 0

def test_an_already_solved_is_reported_as_such_with_solution() -> None:
    """Test that an already solved puzzle is reported correctly."""
    board = Board.from_numbers([1, 2, 3, 4, 5, 100])
    target = Target(100)
    sut = Solver()

    result = sut.solve(board, target)

    assert result.solution_found is True
    assert len(result.instructions) == 2

@pytest.mark.parametrize("numbers,target,expected_solution_steps", [
    ([ 1, 2, 3, 4, 5, 6 ], 12, 3),
    ([ 1, 4, 4, 5, 6, 50 ], 350, 4),
    ([ 1, 3, 3, 8, 9, 50 ], 410, 5),
    ([ 2, 3, 3, 5, 6, 75 ], 277, 6),
    ([ 1, 10, 25, 50, 75, 100 ], 813, 7)
])
def test_a_puzzle_with_a_solution_is_reported_as_such_with_solution(numbers: Iterable[int], target: Target, expected_solution_steps: int) -> None:
    """Test that a puzzle with a solution is reported correctly."""
    board = Board.from_numbers(numbers)
    target = Target(target)
    sut = Solver()

    result = sut.solve(board, target)

    assert result.solution_found is True
    assert len(result.instructions) == expected_solution_steps
