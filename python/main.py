from mcp.server.fastmcp import FastMCP
from mcp.types import ToolAnnotations

from board import Board
from markdown_solver_result_formatter import MarkdownSolverResultFormatter
from solver import Solver
from target import Target

mcp = FastMCP("puzzle-solver-python")

@mcp.tool(description="Echoes the message back to the client.")
def echo(message: str) -> str:
    return f"hello {message}"

@mcp.tool(name="number-game-solver",
          description="Solves a popular numbers game. The game is made up of six digits and the goal is to determine how to manipulate the digits using simple arithmetic operations in order to arrive at the target. The game was made famous on the UK TV Show, Countdown.",
          annotations=ToolAnnotations(destructiveHint=False, idempotentHint=True, openWorldHint=False, readOnlyHint=True))
def solve(numbers: list[int], target: int) -> str:
    try:
        solver = Solver()
        result = solver.solve(Board.from_numbers(numbers), Target(target))

        formatter = MarkdownSolverResultFormatter()
        return formatter.format(result)
    except:
        return "The game could not be solved. Check that it is a valid puzzle."


if __name__ == "__main__":
    mcp.run(transport="streamable-http")
