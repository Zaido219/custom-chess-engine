using Chess.UI;
using Chess.Core.BoardState;
using Chess.Core.MoveGenerations;
using Raylib_cs;
// console doesnt support  unicode out of the box
System.Console.OutputEncoding = System.Text.Encoding.UTF8;
// Call this first so the raycaster knows where the board edges are!
MoveGenerator.preComputedMoveData();
string startingPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
Board board = new Board();
FenUtility fenUtility = new FenUtility();
fenUtility.loadFen(startingPosition, board);
// UI dependecies
IBoardRenderer BoardRenderer = new RayLibBoardRenderer();
IDragAndDropHandler dragHandler = new DragAndDropHandler();
// init window and load graphic textures
BoardRenderer.Init();
BoardRenderer.Render(board);
// main game loop
while (!Raylib.WindowShouldClose())
{
    
}
BoardRenderer.Dispose();