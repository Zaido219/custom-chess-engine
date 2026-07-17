namespace Chess.Core.BoardState;

public static class Piece
{
    public const int None = 0;
    public const int King = 1;
    public const int Pawn = 2;
    public const int Knight = 3;
    public const int Bishop = 4;
    public const int Rook = 5;
    public const int Queen = 6;
    //colors
    public const int White = 8;
    public const int Black = 16;
    //bitmask
    private const int typeMask = 0b00111; // 7 in decimal (covers bits 1, 2, and 4)
    private const int colorMask = 0b11000; // 24 in decimal (covers bits 8 and 16)
    // extract the color value 8 for white, 16 for black, 0 for none
    public static int color(int piece) => piece & colorMask;
    // extract the type value 1-6
    public static int pieceType(int piece) => piece & typeMask;
    // helper to check if piece is of specific color
    public static bool isColor(int piece, int color) => (piece & colorMask) == color;
}

// How this works in practice:If we pass our White Bishop (12 or 01100) into these helpers:
// PieceType(12): Performs 01100 & 00111 $\rightarrow$ results in 00100 ($4$, which is Bishop).
// Color(12): Performs 01100 & 11000 $\rightarrow$ results in 01000 ($8$, which is White).IsColor(12, 
// Piece.White): Checks if Color(12) == 8 $\rightarrow$ returns true.