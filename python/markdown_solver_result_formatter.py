from operators import Operators
from solve_instruction import AdditionalSolveInstruction
from solver_result import SolverResult


class MarkdownSolverResultFormatter:
    def format(self, result: SolverResult) -> str:
        """
        Formats a SolverResult into a Markdown string.

        :param result: The solver result to format.
        :return: A markdown-formatted string describing the attempt.
        """
        lines = ["The number game which was "]

        # Opening statement
        if not result.solution_found:
            lines.append("not ")
        lines.append(f"solved had the target of {result.target.target} ")
        lines.append("and the numbers that could be used to calculate that target ")
        lines.append(f"were {result.board}. Only basic arithmetic operations were ")
        lines.append("allowed, these being addition, subtraction, multiplication and ")
        lines.append("division. Results of any operation must be positive whole ")
        lines.append("numbers. When these rules were applied, it was ")

        if not result.solution_found:
            lines.append("not ")
        lines.append("possible to solve the game.")
        lines.append("\n\n")

        if result.solution_found:
            # Check if already solved (target was in original numbers)
            if len(result.instructions) == 2:
                lines.append("The game was already in a solved state as the target ")
                lines.append(f"{result.target.target} was already present in the set ")
                lines.append("of available numbers. No operations were required to ")
                lines.append("arrive at the solution.\n")
            else:
                # Show solution steps in table format
                lines.append(
                    "The steps taken to solve the problem are shown below.\n\n"
                )
                lines.append(
                    "| Available Numbers | Used Numbers | Operation Applied | "
                )
                lines.append("Operation Result | Updated Available Numbers |\n")
                lines.append("|-|-|-|-|-|\n")

                for instruction in result.instructions:
                    if isinstance(instruction, AdditionalSolveInstruction):
                        operation = instruction.operation
                        operator_symbol = self._get_operator_symbol(operation.operator)

                        lines.append(f"| {instruction.previous_state} | ")
                        lines.append(f"{operation.left_operand.value}, ")
                        lines.append(f"{operation.right_operand.value} | ")
                        lines.append(f"{operator_symbol} | ")
                        lines.append(f"{operation.result.value} | ")
                        lines.append(f"{instruction.state} |\n")

                lines.append("\n")
                lines.append("This was the first solution that was found, other ")
                lines.append("solutions may exist and while others may take the ")
                lines.append("same or more number of steps, no solution exists that ")
                lines.append("takes fewer steps.")

        lines.append("\n")
        return "".join(lines)

    @classmethod
    def _get_operator_symbol(cls, operator: Operators) -> str:
        """
        Maps operator enum to its symbol representation.

        :param operator: The operator enum value.
        :return: The symbol representation of the operator.
        """
        return operator.value
