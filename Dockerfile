FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app


COPY ChessGame.slnx ./
COPY src/Chess.Core/Chess.Core.csproj ./src/Chess.Core/
COPY src/Chess.UI/Chess.UI.csproj ./src/Chess.UI/
COPY src/Chess.Controller/Chess.Controller.csproj ./src/Chess.Controller/
COPY tests/Chess.Core.Tests/Chess.Core.Tests.csproj ./tests/Chess.Core.Tests/


RUN dotnet restore ChessGame.slnx


COPY src/ ./src/
COPY tests/ ./tests/


RUN dotnet test ChessGame.slnx --configuration Release

# Publish the executable console app
RUN dotnet publish src/Chess.Controller/Chess.Controller.csproj -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app


COPY --from=build-env /app/out .


ENTRYPOINT ["dotnet", "Chess.Controller.dll"]