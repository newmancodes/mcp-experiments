FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /src

COPY . .

RUN dotnet restore PuzzleSolver.sln

RUN dotnet build PuzzleSolver.sln -c Release

RUN dotnet publish PuzzleSolver.sln -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build-env /app .

EXPOSE 80

ENTRYPOINT ["dotnet", "PuzzleSolver.MCPServer.dll"]
