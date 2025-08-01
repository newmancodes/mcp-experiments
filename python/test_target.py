import pytest

from exceptions import TargetValidationError
from target import Target


def test_target_may_not_be_zero() -> None:
    """Test that the Target cannot be zero."""
    with pytest.raises(
        TargetValidationError, match="Target must be between 1 and 999."
    ):
        Target(0)


def test_target_may_not_be_negative() -> None:
    """Test that the Target cannot be negative."""
    with pytest.raises(
        TargetValidationError, match="Target must be between 1 and 999."
    ):
        Target(-1)


def test_target_may_not_be_greater_than_999() -> None:
    """Test that the Target cannot be greater than 999."""
    with pytest.raises(
        TargetValidationError, match="Target must be between 1 and 999."
    ):
        Target(1000)


def test_target_can_be_randomly_generated() -> None:
    """Test that the Target can be randomly generated within the valid range."""
    target = Target.random()
    assert 1 <= target.target <= 999, "Target should be between 1 and 999."
