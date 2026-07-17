# ==========================================
# STAGE 1: Build & Test Environment
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# Copy the solution file and project configuration files first
COPY ChessGame.slnx ./
COPY src/Chess.Core/Chess.Core.csproj ./src/Chess.Core/
COPY src/Chess.UI/Chess.UI.csproj ./src/Chess.UI/
COPY src/Chess.Controller/Chess.Controller.csproj ./src/Chess.Controller/
COPY tests/Chess.Core.Tests/Chess.Core.Tests.csproj ./tests/Chess.Core.Tests/

# Restore dependencies based on the files copied above
RUN dotnet restore ChessGame.slnx

# Copy the actual source and test code files
COPY src/ ./src/
COPY tests/ ./tests/

# Execute the unit tests. If any test fails, the build halts.
RUN dotnet test ChessGame.slnx --configuration Release

# Publish the executable console app
RUN dotnet publish src/Chess.Controller/Chess.Controller.csproj -c Release -o /app/out

# ==========================================
# STAGE 2: Lightweight Runtime Environment
# ==========================================
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app

# Copy only the compiled binaries from Stage 1
COPY --from=build-env /app/out .

# Define the runnable entry point
ENTRYPOINT ["dotnet", "Chess.Controller.dll"]