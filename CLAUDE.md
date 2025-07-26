# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an MCP (Model Context Protocol) experiment project that implements puzzle solvers for the Countdown Numbers Game across three languages: C#, Python, and TypeScript. Each language provides its own MCP server implementation.

### Game Rules
The Countdown Numbers Game uses 6 numbers from a pool (1-10 and 25,50,75,100) to reach a target between 1-999 using basic arithmetic (+, -, ×, ÷). Results must be natural numbers (no negatives, decimals, or division by zero).

## Repository Structure

- `dotnet/` - C# implementation (fully functional MCP server)
- `python/` - Python implementation (mature core game logic, 94% test coverage, MCP server not yet implemented)  
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
uv run --frozen pytest                         # Run tests (27 tests, 94% coverage)
uv run pytest --cov=. --cov-report=term-missing # Run tests with coverage report
uv run main.py                                  # Run main script
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

### Python Implementation
- **number.py**: Value object for game numbers with category-based validation (Small: 1-10, Large: 25,50,75,100)
- **board.py**: Board aggregate with factory methods and builder pattern for construction validation
- **board_rules.py**: Pure functions for game rules (starting size, reuse limits)
- **target.py**: Validated target value object with random generation
- **main.py**: Entry point (placeholder)

The Python implementation follows modern Python practices with comprehensive type hints, property decorators, dataclasses, and the builder pattern implemented as nested classes. All game rules are enforced during board construction with appropriate validation.

### Key Classes
- `Board`: Represents the current game state with available numbers
- `Solver`: Main solving logic using BFS
- `NumberGameTool`: MCP tool that exposes solver via HTTP
- `BreadthFirstSearch<TState, TAction>`: Generic BFS implementation

### MCP Integration
The C# server runs on HTTP and exposes a `number-game-solver` tool that takes an array of 6 numbers and a target, returning a markdown-formatted solution or failure message.

## Development Notes

- The C# implementation is complete and functional
- The Python implementation has mature core game logic with comprehensive test coverage (94%) but no MCP server yet
- TypeScript implementation is skeletal - only basic project structure exists
- All three implementations are configured for Docker deployment
- GitHub Actions CI builds and tests all three languages
- The `.vscode/mcp.json` file configures VS Code to connect to the MCP servers

### Development Methodology

This project follows Test-First Test-Driven Development (TDD) practices using the Red-Green-Refactor cycle:

1. **Red**: Write a failing test first that describes the desired behavior
2. **Green**: Write the minimal code needed to make the test pass
3. **Refactor**: Improve the code while keeping tests passing

When working on any of the implementations (C#/Python/TypeScript), expect tests to fail initially - this is the intended starting point for TDD.

### Language-Specific Guidelines

**C# (.NET)**: Follow established .NET conventions with SOLID principles, dependency injection, and async/await patterns. Use record types for value objects and proper exception handling.

**Python**: Adhere to PEP 8 and modern Python idioms. Use type hints throughout (`typing.Self` for self-referential returns), `@property` decorators for getters, `@dataclass(frozen=True)` for immutable value objects, and `@classmethod` for factory methods. Prefer composition over inheritance and use convention-based privacy (`_` prefixes).

**TypeScript**: Follow TypeScript best practices with strict type checking, proper interface design, and modern ES6+ features. Use readonly properties for immutability and prefer functional programming patterns where appropriate.

All implementations should maintain high test coverage (target >90%) and demonstrate mastery of each language's idioms and design patterns.

## Testing MCP Servers

Use the MCP Inspector for debugging: `npx @modelcontextprotocol/inspector`

## VS Code Integration

The repository includes VS Code configuration in `.vscode/mcp.json` that connects to all three MCP server implementations when running.