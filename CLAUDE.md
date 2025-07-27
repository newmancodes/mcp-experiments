# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an MCP (Model Context Protocol) experiment project that implements puzzle solvers for the Countdown Numbers Game across three languages: C#, Python, and TypeScript. Each language demonstrates different maturity levels in implementing MCP server functionality.

### Game Rules
The Countdown Numbers Game uses 6 numbers from a pool (1-10 and 25,50,75,100) to reach a target between 1-999 using basic arithmetic (+, -, ×, ÷). Results must be natural numbers (no negatives, decimals, or division by zero).

## Repository Structure

- `dotnet/` - C# implementation (complete MCP server with 4 projects)
- `python/` - Python implementation (complete MCP server, 34 tests)  
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
uv run --frozen pytest                           # Run tests (34 tests)
uv run pytest --cov=. --cov-report=term-missing  # Run tests with coverage report
uv run mypy .                                     # Type checking with MyPy
uv run ruff check .                               # Linting and code quality
uv run main.py                                    # Run MCP server
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
- Full MCP server implementation using ASP.NET Core with ModelContextProtocol.AspNetCore
- Generic breadth-first search with state traversal
- Markdown result formatting for LLM consumption
- Board equality comparison and validation
- Complete test coverage for core logic

### Python Implementation (Complete)
**Core Modules:**
- **board.py**: Board aggregate with factory methods and validation
- **solver.py**: Solver implementation using breadth-first search
- **breadth_first_search.py**: Generic BFS algorithm with state traversal
- **number.py**: Value object with category validation (Small: 1-10, Large: 25,50,75,100)
- **target.py**: Validated target value object with random generation
- **mathematical_operation.py**: Operation execution and validation
- **solution.py** & **solution_step.py**: Solution representation with step breakdown
- **operators.py**: Enum for arithmetic operators (+, -, ×, ÷)
- **board_rules.py**: Validation rules for board construction
- **exceptions.py**: Custom exception types
- **predicate.py**: Generic predicate interface
- **solve_instruction.py**: Instructions for solve steps
- **solver_result.py**: Result container for solve attempts
- **state_traversal.py**: State traversal interface for BFS

**MCP Server:**
- **main.py**: FastMCP server with HTTP transport
- **markdown_solver_result_formatter.py**: Formats results as Markdown tables
- Tools: `echo` and `number-game-solver`

**Key Features:**
- Complete MCP server using FastMCP framework
- 34 comprehensive tests covering all functionality
- Modern Python practices with strict type hints
- Immutable value objects using `@dataclass(frozen=True)`
- Builder pattern for board construction
- Comprehensive validation and error handling
- Generic state traversal and BFS implementation
- HTTP transport for MCP communication

### TypeScript Implementation (Minimal)
**Current State:**
- Basic project structure with TypeScript configuration
- MCP SDK dependency configured
- Empty server.ts file (0 lines)
- Build scripts configured but no implementation

### Common Architecture Patterns
All implementations share:
- **Board**: Game state with available numbers and validation rules
- **Solver**: BFS-based solving algorithm with state exploration
- **Target**: Validated target number (1-999)
- **MathematicalOperation**: Arithmetic operations with validation
- **Solution**: Step-by-step solution representation
- **BreadthFirstSearch**: Generic search algorithm with state traversal
- **MarkdownFormatter**: Consistent output formatting for LLM consumption

## Development Methodology

This project follows **Test-First Test-Driven Development (TDD)** using the Red-Green-Refactor cycle:

1. **Red**: Write a failing test first that describes the desired behavior
2. **Green**: Write the minimal code needed to make the test pass  
3. **Refactor**: Improve the code while keeping tests passing

### Current Test Status
- **C#**: Full test suite with MSTest framework
- **Python**: 34 tests covering board validation, solver logic, and target generation
- **TypeScript**: No tests implemented yet

### Language-Specific Guidelines

**C# (.NET 9.0)**: 
- Follow SOLID principles with dependency injection
- Use record types for value objects where appropriate
- Implement proper async/await patterns
- Utilize nullable reference types
- Apply comprehensive error handling
- ASP.NET Core for MCP server hosting

**Python (3.13+)**:
- Strict adherence to PEP 8 and modern Python idioms
- Comprehensive type hints with proper typing imports
- `@property` decorators for computed properties
- `@dataclass(frozen=True)` for immutable value objects
- `@classmethod` for factory methods
- Convention-based privacy with `_` prefixes
- FastMCP framework for server implementation
- Ruff for linting, MyPy for type checking

**TypeScript**:
- Strict TypeScript configuration enabled
- Modern ES6+ features and syntax
- Proper interface design and type safety
- Functional programming patterns preferred
- Readonly properties for immutability

## MCP Server Status

### C# MCP Server (Complete)
- ASP.NET Core with ModelContextProtocol.AspNetCore v0.3.0-preview.3
- HTTP transport
- `number-game-solver` and `echo` tools implemented
- Markdown-formatted responses with solution tables
- Production-ready implementation

### Python MCP Server (Complete)
- FastMCP framework with streamable HTTP transport
- `number-game-solver` and `echo` tools implemented
- Comprehensive markdown formatting with step-by-step tables
- Error handling for invalid puzzles
- Full test coverage of core logic

### TypeScript MCP Server (Not Implemented)
- `@modelcontextprotocol/sdk` v1.13.0 dependency configured
- Server file empty, awaiting implementation
- Project structure ready for development

## Testing and Quality

- **Target Test Coverage**: >90% for all implementations
- **C#**: MSTest framework with comprehensive unit tests
- **Python**: pytest with 34 tests, mypy type checking, ruff linting
- **TypeScript**: Test framework not yet configured

## Debugging MCP Servers

Use the MCP Inspector for debugging: `npx @modelcontextprotocol/inspector`

## VS Code Integration

Configure VS Code to connect to MCP servers via `.vscode/mcp.json` (see README.md for example usage).