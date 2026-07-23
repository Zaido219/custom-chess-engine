// todo:We need a much better controller class that will encapsulate all of this
using Chess.UI;
using Chess.Core.BoardState;
using Chess.Core.MoveGenerations;
using Raylib_cs;
using System.Numerics;
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
// main game loop
while (!Raylib.WindowShouldClose())
{
    Vector2 mousePos = Raylib.GetMousePosition();
     //Mouse Clicked (Begin Drag)
    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
    {
        int clickedSquare = GetSquareFromMouse(mousePos);

        // Check if a valid square was clicked AND contains a piece
        if (clickedSquare != -1 && board[clickedSquare] != 0)
        {
            Vector2 tileOrigin = GetTileOrigin(clickedSquare);
            dragHandler.BeginDrag(clickedSquare, mousePos, tileOrigin);
        }
    }

    //Mouse Held (Update Drag Position)
    if (dragHandler.isDragging)
    {
        dragHandler.UpdateDrag(mousePos);
    }

    // PHASE C: Mouse Released (End Drag / Commit Move)
    if (Raylib.IsMouseButtonReleased(MouseButton.Left) && dragHandler.isDragging)
    {
        dragHandler.EndDrag(out int sourceSquare);
        int targetSquare = GetSquareFromMouse(mousePos);

        if (targetSquare != -1)
        {
            // Execute move on the board array!
            // e.g., board.MakeMove(sourceSquare, targetSquare);
        }
    }

    // render pass
    // Pass dragHandler so BoardRenderer knows which piece to float
    BoardRenderer.Render(board, dragHandler);
}
BoardRenderer.Dispose();


// Converts pixel coordinates (e.g. Mouse X, Y) to a 0-63 board array index
int GetSquareFromMouse(Vector2 mousePosition, int tileSize = 80)
{
    int file = (int)(mousePosition.X / tileSize);
    int rank = 7 - (int)(mousePosition.Y / tileSize); // Flip Y because screen Y=0 is the top

    // Clamp to valid 8x8 boundaries
    if (file < 0 || file > 7 || rank < 0 || rank > 7) return -1;

    return rank * 8 + file;
}

// Converts a 0-63 board array index to top-left screen pixel coordinates
Vector2 GetTileOrigin(int squareIndex, int tileSize = 80)
{
    int file = squareIndex % 8;
    int rank = squareIndex / 8;

    float x = file * tileSize;
    float y = (7 - rank) * tileSize;

    return new Vector2(x, y);
}