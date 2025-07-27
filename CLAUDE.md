# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an MCP (Model Context Protocol) experiment project that implements puzzle solvers for the Countdown Numbers Game across three languages: C#, Python, and TypeScript. Each language demonstrates different maturity levels in implementing MCP server functionality.

### Game Rules
The Countdown Numbers Game uses 6 numbers from a pool (1-10 and 25,50,75,100) to reach a target between 1-999 using basic arithmetic (+, -, ×, ÷). Results must be natural numbers (no negatives, decimals, or division by zero).

## Repository Structure

- `dotnet/` - C# implementation (complete MCP server with 4 projects)
- `python/` - Python implementation (complete core logic, 96% test coverage, MCP server not implemented)  
- `typescript/` - TypeScript implementation (minimal setup, empty server file)

## Build and Test Commands

### .NET (dotnet/)
```bash
# From dotnet/ directory
dotnet restore PuzzleSolver.sln
dotnet build --configuration Release --no-restore PuzzleSolver.sln
dotnet test --verbosity normal                   # Run tests
dotnet run --project src/PuzzleSolver.MCPServer  # Run MCP server
```

### Python (python/)
```bash
# From python/ directory
uv run --frozen pytest                           # Run tests (34 tests, 96% coverage)
uv run pytest --cov=. --cov-report=term-missing  # Run tests with coverage report
uv run mypy .                                     # Type checking with MyPy
uv run ruff check .                               # Linting and code quality
uv run main.py                                    # Run main script (placeholder)
```

### TypeScript (typescript/)
```bash
# From typescript/ directory
npm ci              # Install dependencies
npm run build       # Build TypeScript
npm run build:watch # Build with watch mode
npm run dev         # Run with tsx (currently empty server)
```

## Architecture

### C# Implementation (Complete)
**Projects:**
- **PuzzleSolver.NumbersGame**: Core game logic with Board, Solver, and mathematical operations
- **PuzzleSolver.BreadthFirstSearch**: Generic BFS algorithm for state space search
- **PuzzleSolver.MCPServer**: HTTP-based MCP server with tools (NumberGameTool, EchoTool)
- **PuzzleSolver.NumbersGame.Test**: Comprehensive unit tests

**Key Features:**
- Full MCP server implementation using HTTP transport
- Generic breadth-first search with state traversal
- Markdown result formatting for LLM consumption
- Board equality comparison and validation
- Complete test coverage for core logic

### Python Implementation (Core Complete, MCP Pending)
**Modules:**
- **board.py**: Board aggregate with factory methods and builder pattern
- **solver.py**: Solver implementation using breadth-first search
- **breadth_first_search.py**: Generic BFS algorithm
- **number.py**: Value object with category validation (Small: 1-10, Large: 25,50,75,100)
- **target.py**: Validated target value object with random generation
- **mathematical_operation.py**: Operation execution and validation
- **solution.py**: Solution representation with step-by-step breakdown
- **main.py**: Entry point (placeholder for MCP server)

**Key Features:**
- 96% test coverage (359 statements, 16 missing)
- Modern Python practices with strict type hints
- Immutable value objects using `@dataclass(frozen=True)`
- Builder pattern for board construction
- Comprehensive validation and error handling
- Generic state traversal and BFS implementation

### TypeScript Implementation (Minimal)
**Current State:**
- Basic project structure with TypeScript configuration
- MCP SDK dependency configured
- Empty server.ts file (0 lines)
- Build scripts configured but no implementation

### Common Architecture Patterns
All implementations share:
- **Board**: Game state with available numbers
- **Solver**: BFS-based solving algorithm
- **Target**: Validated target number (1-999)
- **MathematicalOperation**: Arithmetic operations with validation
- **Solution**: Step-by-step solution representation
- **BreadthFirstSearch**: Generic search algorithm

## Development Methodology

This project follows **Test-First Test-Driven Development (TDD)** using the Red-Green-Refactor cycle:

1. **Red**: Write a failing test first that describes the desired behavior
2. **Green**: Write the minimal code needed to make the test pass  
3. **Refactor**: Improve the code while keeping tests passing

### Current Test Status
- **C#**: Full test suite with unit tests for core logic
- **Python**: 34 tests with 96% coverage, comprehensive test suite
- **TypeScript**: No tests implemented yet

### Language-Specific Guidelines

**C# (.NET 9.0)**: 
- Follow SOLID principles with dependency injection
- Use record types for value objects
- Implement proper async/await patterns
- Utilize nullable reference types
- Apply comprehensive error handling

**Python (3.13+)**:
- Strict adherence to PEP 8 and modern Python idioms
- Comprehensive type hints with `typing.Self` for fluent interfaces
- `@property` decorators for computed properties
- `@dataclass(frozen=True)` for immutable value objects
- `@classmethod` for factory methods
- Convention-based privacy with `_` prefixes

**TypeScript**:
- Strict TypeScript configuration enabled
- Modern ES6+ features and syntax
- Proper interface design and type safety
- Functional programming patterns preferred
- Readonly properties for immutability

## MCP Server Status

### C# MCP Server (Functional)
- HTTP-based transport
- `number-game-solver` tool implemented
- Markdown-formatted responses
- Ready for VS Code integration

### Python MCP Server (Not Implemented)
- Core game logic complete and tested
- MCP integration pending
- `mcp[cli]` dependency configured

### TypeScript MCP Server (Not Implemented)
- `@modelcontextprotocol/sdk` dependency configured
- Server file empty, awaiting implementation

## Testing and Quality

- **Target Test Coverage**: >90% for all implementations
- **C#**: MSTest framework with comprehensive unit tests
- **Python**: pytest with 96% coverage, mypy type checking, ruff linting
- **TypeScript**: Test framework not yet configured

## Debugging MCP Servers

Use the MCP Inspector for debugging: `npx @modelcontextprotocol/inspector`

## VS Code Integration

Configure VS Code to connect to MCP servers via `.vscode/mcp.json` (currently not present in repository).