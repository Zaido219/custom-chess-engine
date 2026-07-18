using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Chess.Core.BoardState;
// this class must change the state of the current board
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

    // parse fen string
    public void loadFen(string fen, Board board)
    {
        // this class needs to return a piece
        //isolate the piece layouts from active color, castling rights, etc.
        string fenBoard = fen.Split(' ')[0];
        Console.WriteLine(fenBoard);
        // loop through the fen board
        int file = 0;
        int rank = 7;
        foreach (char symbol in fenBoard)
        {
            if (symbol == '/')
            {
                file = 0;
                rank--;
            }
            else if (char.IsDigit(symbol)) // check if its a number cause its a square count
            {
                // move file pointer by that number
                file += int.Parse(symbol.ToString());
            }
            else // current symbol is a letter
            {
                // calculate the 1d index
                int index = (rank * 8) + file;
                //write the fetch piece to the board
                board[index] = PieceMap[symbol];
                // increment file
                file++;
            }
        }
    }
}

// Now you can read and write to squares directly using array syntax on the board object:
//board[0] = PieceMap['R']; // This works because of your internal setter!