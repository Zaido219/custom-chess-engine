using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Chess.Core.BoardState;

public class FenUtility
{
    // fen map
    private static readonly Dictionary<char, int> PieceMap = new Dictionary<char, int>
    {
        // White Pieces (Uppercase)
        ['K'] = Piece.White | Piece.King,
        ['P'] = Piece.White | Piece.Pawn,
        ['N'] = Piece.White | Piece.Knight,
        ['B'] = Piece.White | Piece.Bishop,
        ['R'] = Piece.White | Piece.Rook,
        ['Q'] = Piece.White | Piece.Queen,

        // Black Pieces (Lowercase)
        ['k'] = Piece.Black | Piece.King,
        ['p'] = Piece.Black | Piece.Pawn,
        ['n'] = Piece.Black | Piece.Knight,
        ['b'] = Piece.Black | Piece.Bishop,
        ['r'] = Piece.Black | Piece.Rook,
        ['q'] = Piece.Black | Piece.Queen

    };
}