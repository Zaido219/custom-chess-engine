@echo off
echo Building Chess Engine Container ...
docker build -t custom-chess-engine .

echo Cleaning up intermediate build artifacts...
docker system prune -f

echo Done!