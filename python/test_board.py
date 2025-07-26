import pytest
from board import Board

@pytest.mark.parametrize("numbers, expected", [
    ([ 1, 2, 3, 4, 5, 6 ], "1, 2, 3, 4, 5, 6"),
    ([ 1, 1, 2, 2, 3, 3 ], "1, 1, 2, 2, 3, 3"),
    ([ 7, 8, 9, 10, 25, 50 ], "7, 8, 9, 10, 25, 50"),
    ([ 3, 6, 25, 50, 75, 100 ], "3, 6, 25, 50, 75, 100")
])
def test_valid_boards_can_be_created(numbers: list[int], expected: str) -> None:
    """Test that valid boards can be created."""
    board = Board.from_numbers(numbers)
    assert str(board) == expected, f"Expected {expected}, got {str(board)}"

@pytest.mark.parametrize("numbers", [
    ([ 1, 2, 3, 4, 5 ]),
    ([ 1, 2, 3, 4, 5, 6, 7 ])
])
def test_boards_must_have_six_numbers(numbers: list[int]) -> None:
    """Test that boards must have exactly six numbers."""
    with pytest.raises(ValueError, match="Boards require six numbers."):
        Board.from_numbers(numbers)

@pytest.mark.parametrize("number", [
    (11),
    (34),
    (101)
])
def test_boards_can_only_use_valid_numbers(number: int) -> None:
    """Test that boards can only use valid numbers."""
    with pytest.raises(ValueError, match=f"The number {number} is not a valid board number."):
        Board.from_numbers([ 1, 2, 3, 4, 5, number ])

@pytest.mark.parametrize("small_number", [
    (1),
    (2),
    (3),
    (4),
    (5),
    (6),
    (7),
    (8),
    (9),
    (10)
])
def test_boards_may_not_reuse_small_numbers_more_than_twice(small_number: int) -> None:
    """Test that boards may not reuse small numbers more than twice."""
    with pytest.raises(ValueError, match=f"The number {small_number} has been used too many times."):
        Board.from_numbers([small_number, small_number, small_number, 25, 50, 75])

@pytest.mark.parametrize("large_number", [
    (25),
    (50),
    (75),
    (100)
])
def test_boards_may_not_reuse_large_numbers_more_than_once(large_number: int) -> None:
    """Test that boards may not reuse large numbers more than once."""
    with pytest.raises(ValueError, match=f"The number {large_number} has been used too many times."):
        Board.from_numbers([ 1, 2, 3, 4, large_number, large_number ])
