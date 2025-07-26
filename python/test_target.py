import pytest
from target import Target

def test_target_may_not_be_zero() -> None:
    """Test that the Target cannot be zero."""
    with pytest.raises(ValueError, match="Target must be between 1 and 999."):
        Target(0)

def test_target_may_not_be_negative() -> None:
    """Test that the Target cannot be negative."""
    with pytest.raises(ValueError, match="Target must be between 1 and 999."):
        Target(-1)
        
def test_target_may_not_be_greater_than_999() -> None:
    """Test that the Target cannot be greater than 999."""
    with pytest.raises(ValueError, match="Target must be between 1 and 999."):
        Target(1000)
