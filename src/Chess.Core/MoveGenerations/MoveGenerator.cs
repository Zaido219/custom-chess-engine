using System.ComponentModel;
using System.IO.Pipes;
using Chess.Core.MoveGenerations;

namespace Chess.Core.BoardState;
//calculate the distance from every single one of the 64 squares to all 8 edges exactly once when the game starts up.
// this makes the engine not compute if a board will be off the board each time
// we are trading space for time in here
public static class MoveGenerator
{
    //tores the exact number of squares to the edge of the board from any given square in all 8 directions
    private static readonly int[] directionalOffsets = { 8, -8, -1, 1, 7, -7, 9, -9 };
    private static readonly int[,] numSquaresToEdge = new int[64, 8];
    private static List<Move> moves;

    public static void preComputedMoveData()
    {
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                int numNorth = 7 - rank;
                int numSouth = rank;
                int numWest = file;
                int numEast = 7 - file;

                int squareIndex = rank * 8 + file;

                numSquaresToEdge[squareIndex, 0] = numNorth;
                numSquaresToEdge[squareIndex, 1] = numSouth;
                numSquaresToEdge[squareIndex, 2] = numWest;
                numSquaresToEdge[squareIndex, 3] = numEast;
            }
        }
    }
    public static List<Move> GenerateMoves(Board board)
    {
        moves = new List<Move>();
        for (int startSquare = 0; startSquare < 64; startSquare++)
        {
            int piece = board[startSquare];
            // get the piece that of right color for whoever's turn it is to move
            if (Piece.isColor(piece, board.colorToMove))
            {
                if (Piece.isSlidingPiece(piece))
                {
                    GenerateSlidingMoves(startSquare, piece, board);
                }
            }
        }
        return moves;
    }
    public static void GenerateSlidingMoves(int startSquare, int piece, Board board)
    {
        // --- THE DIRECTIONAL INDEX TRICK ---
        // This uses ternary operators to slice our 8-element 'directionalOffsets' array.
        // Offsets 0-3 = Orthogonal (Rook) | Offsets 4-7 = Diagonal (Bishop)
        //
        // 1. If Bishop: starts at 4, ends before 8 -> loops indices [4, 5, 6, 7] (Diagonals only)
        // 2. If Rook:   starts at 0, ends before 4 -> loops indices [0, 1, 2, 3] (Orthogonals only)
        // 3. If Queen:  starts at 0, ends before 8 -> loops indices [0..7]       (All 8 directions)
        int startDirIndex = Piece.pieceType(piece) == Piece.Bishop ? 4 : 0;
        int endDirIndex = Piece.pieceType(piece) == Piece.Rook ? 4 : 8;
        // this assignment is for code readability
        int friendlyColor = board.colorToMove;
        // xor to get the opponent color
        int opponentColor = board.colorToMove ^ 24; // 24 here is the color mask
        // loop over the eight different directions
        for (int directionIndex = startDirIndex; directionIndex < endDirIndex; directionIndex++)
        {
            // loop for the number of squares in that direction
            for (int n = 0; n < numSquaresToEdge[startSquare, directionIndex]; n++)
            {
                int targetSquare = startSquare + directionalOffsets[directionIndex] * (n + 1);
                int pieceOnTargetSquare = board[targetSquare];
                // blocked by a freiendly piece, so cant move on this direction
                if (Piece.isColor(pieceOnTargetSquare, friendlyColor))
                {
                    break;
                }
                // create the move going from startSquare to endSquare and add it to the 
                // list of possible moves
                moves.Add(new Move(startSquare, targetSquare));
                // if there is an enemy piece on the target square
                // we capture and we cant go anywhere from that;
                if (Piece.isColor(pieceOnTargetSquare, opponentColor))
                {
                    break;
                }
            }
        }
    }
}