from board import Board
from target import Target
from solver import Solver

def test_impossible_puzzles_are_reported_as_such():
    """Test that impossible puzzles are reported correctly."""
    board = Board([3, 7, 6, 2, 1, 7])
    target = Target(824)
    sut = Solver()
    
    result = sut.solve(board, target)
    
    assert result.solution_found is False
    assert len(result.instructions) == 0
