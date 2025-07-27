from mcp.server.fastmcp import FastMCP

mcp = FastMCP("puzzle-solver-python")

@mcp.tool(description="Echoes the message back to the client.")
def echo(message: str) -> str:
    return f"hello {message}"

if __name__ == "__main__":
    mcp.run(transport="streamable-http")
