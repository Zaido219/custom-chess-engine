using System.Diagnostics.Contracts;
using System.Drawing;
using Chess.Core.BoardState;
using Raylib_cs;
using Color = Raylib_cs.Color; // fixes namespace ambiguity
namespace Chess.UI;

public class RayLibBoardRenderer : IBoardRenderer
{
    private readonly int _squareSize = 80;
    public void Render(Board board)
    {
        // initialized window; if hindi pa
        if (!Raylib.IsWindowReady())
        {
            Raylib.InitWindow(640, 640, "Chess engine");
            Raylib.SetTargetFPS(60);
        }
        // main render loop
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            // main drawBoard function
            DrawBoard(board);
            
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();

    }
    private void DrawBoard(Board board)
    {
        for(int rank=7; rank >= 0; rank--)
        {
            for(int file=0; file < 8; file++)
            {
                int index = (rank * 8) + file;
                int piece = board[index];
                string symbol = mapUnicode(piece);
                // convert board rank/file to screen pixel
                int screenX = file * _squareSize;
                int screenY = (7 - rank) * _squareSize;
                // draw tile background color
                bool isLightSquare = (rank + file) % 2 != 0;
                Color tileColor = isLightSquare ? new Color(240, 217, 181, 255) : new Color(181, 136, 99, 255);
                Raylib.DrawRectangle(screenX, screenY, _squareSize, _squareSize, tileColor);
                // render piece symbol if the piece isnt empty
                if(piece != 0)
                {
                    //Draw unicode character directly to screen pixels!
                    Raylib.DrawText(symbol, screenX + 20, screenY + 15, 50, Color.Black);
                }
            }
        }
    }
    // !this is a duplicated code
    private static string mapUnicode(int piece)
    {
        return piece switch
        {
            // White Pieces
        (Piece.White | Piece.King)   => "\u2654",
        (Piece.White | Piece.Queen)  => "\u2655",
        (Piece.White | Piece.Rook)   => "\u2656",
        (Piece.White | Piece.Bishop) => "\u2657",
        (Piece.White | Piece.Knight) => "\u2658",
        (Piece.White | Piece.Pawn)   => "\u2659",

        // Black Pieces
        (Piece.Black | Piece.King)   => "\u265A",
        (Piece.Black | Piece.Queen)  => "\u265B",
        (Piece.Black | Piece.Rook)   => "\u265C",
        (Piece.Black | Piece.Bishop) => "\u265D",
        (Piece.Black | Piece.Knight) => "\u265E",
        (Piece.Black | Piece.Pawn)   => "\u265F",

        // Empty square or fallback
        _ => "."
        };
    }
}