# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an MCP (Model Context Protocol) experiment project that implements puzzle solvers for the Countdown Numbers Game across three languages: C#, Python, and TypeScript. Each language provides its own MCP server implementation.

### Game Rules
The Countdown Numbers Game uses 6 numbers from a pool (1-10 and 25,50,75,100) to reach a target between 1-999 using basic arithmetic (+, -, ×, ÷). Results must be natural numbers (no negatives, decimals, or division by zero).

## Repository Structure

- `dotnet/` - C# implementation (fully functional MCP server)
- `python/` - Python implementation (basic setup, MCP server not yet implemented)  
- `typescript/` - TypeScript implementation (basic setup, server.ts is empty)

## Build and Test Commands

### .NET (dotnet/)
```bash
# From dotnet/ directory
dotnet restore PuzzleSolver.sln
dotnet build --configuration Release --no-restore PuzzleSolver.sln
dotnet test --configuration Release --no-build --verbosity normal PuzzleSolver.sln

# Run MCP server
dotnet run --project src/PuzzleSolver.MCPServer
```

### Python (python/)
```bash
# From python/ directory
uv run --frozen pytest  # Run tests
uv run main.py          # Run main script
```

### TypeScript (typescript/)
```bash
# From typescript/ directory
npm ci              # Install dependencies
npm run build       # Build TypeScript
npm run build:watch # Build with watch mode
npm run dev         # Run with tsx
```

## Architecture

### C# Implementation
- **PuzzleSolver.NumbersGame**: Core game logic with Board, Solver, and mathematical operations
- **PuzzleSolver.BreadthFirstSearch**: Generic BFS algorithm for state space search
- **PuzzleSolver.MCPServer**: HTTP-based MCP server with NumberGameTool
- **PuzzleSolver.NumbersGame.Test**: Unit tests

The solver uses breadth-first search to find solutions, generating all possible mathematical operations at each state until the target is reached.

### Key Classes
- `Board`: Represents the current game state with available numbers
- `Solver`: Main solving logic using BFS
- `NumberGameTool`: MCP tool that exposes solver via HTTP
- `BreadthFirstSearch<TState, TAction>`: Generic BFS implementation

### MCP Integration
The C# server runs on HTTP and exposes a `number-game-solver` tool that takes an array of 6 numbers and a target, returning a markdown-formatted solution or failure message.

## Development Notes

- The C# implementation is complete and functional
- Python and TypeScript implementations are skeletal - only basic project structure exists
- All three implementations are configured for Docker deployment
- GitHub Actions CI builds and tests all three languages
- The `.vscode/mcp.json` file configures VS Code to connect to the MCP servers

## Testing MCP Servers

Use the MCP Inspector for debugging: `npx @modelcontextprotocol/inspector`

## VS Code Integration

The repository includes VS Code configuration in `.vscode/mcp.json` that connects to all three MCP server implementations when running.