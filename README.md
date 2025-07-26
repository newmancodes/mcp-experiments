# MCP Experiments

[![CI](https://github.com/newmancodes/mcp-experiments/actions/workflows/main.yml/badge.svg)](https://github.com/newmancodes/mcp-experiments/actions/workflows/main.yml)

Some simple experiments with Model Context Protocol (MCP). I'm primarily interested in seeing what is involved with created my own MCP servers. Preference is for Remote MCP servers (using HTTP) with the necessary authorization required to protect the resource server.

## Experiment

The experiment is to create some simple tools that can solve some simple games. I have chosen to implement some tools related to the Numbers round from the popular UK game show [Countdown](https://en.wikipedia.org/wiki/Countdown_(game_show)).

### Game Rules

The rules to the game are fairly simple. From a board of six numbers a randomly generated target must be reached. The numbers available are the first ten natural numbers (1, 2, 3, 4, 5, 6, 7, 8, 9, and 10) along with four larger natural numbers (25, 50, 75, 100). The randomly generated target must be between 1 and 999 (inclusive).

Using the six numbers and by applying simple arithmatic operations addition (+), subtraction (-), multiplication (×), and division (÷). The results of operations must be natural numbers, for example subtraction 5 from 2 would result in -3 and is therefor illegal. Similarly dividing 6 by 4 would be 1.5 and is not allowed. Division by zero is similarly banned.

## Language Choices

I have chosen to implement the MCP Server in each of C#, Python, and TypeScript. This is to test the relative maturity of each SDK. Though Anthropic makes Python and TypeScript SDKs available as first class citizens, the C# SDK is build in collaboration with Microsoft.

## Available Tools

Currently I have implemented a tool which can report if a given Numbers round can be solved. If a solution is found, the solution is detailed. The description of the solve attempt is output in Markdown and then supplied to the LLM via the Client you're using (if you open this solution in Visual Studio Code, there is an [mcp.json](./.vscode/mcp.json) which allows Visual Studio Code to connect to the server(s).

## Using the tool

Make sure the MCP server you want to use is running e.g. open the [.NET Solution](./dotnet/PuzzleSolver.sln) and make sure it is running.

The Client retrieves details of the tools available and communicates this to the LLM (I've tested with Claude Sonnet 4 via GitHub Copilot). If you enter a prompt within your client such as:

```
Can you see if the Countdown numbers game can be solved if the board contains the numbers 1, 2, 3, 4, 5, and 6 with the target being 60?
```

You'll then receive output similar to:

```
Yes! The Countdown numbers game can be solved with the numbers [1, 2, 3, 4, 5, 6] and target 60. Here's the solution:

Solution for target 60:

Step 1: 2 × 5 = 10 (remaining: 1, 3, 4, 6, 10)
Step 2: 6 × 10 = 60 ✓
This is a very elegant 2-step solution! The solver found that you can reach 60 by:

First multiplying 2 and 5 to get 10
Then multiplying 6 by that result (10) to get exactly 60
The solver confirms this is the minimum number of steps possible for this puzzle, though other solutions may exist that take the same or more steps.
```

# Model Context Protocol Inspector

The MCP Inspector is a useful tool for debugging a custom MCP Server without relying on the underlying client's capabiliteies. It's a good first stop in checking if the MCP server is working as expected. You can exeute the Debugging tool via: `npx @modelcontextprotocol/inspector`. I've been using it with v22.15.0 of Node and it appears to work nicely.
