using Chess.Core.BoardState;

namespace Chess.UI;

public class BoardRenderer
{
    public static void Render(Board board)
    {
        
    }
    private string mapUnicode(int piece)
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