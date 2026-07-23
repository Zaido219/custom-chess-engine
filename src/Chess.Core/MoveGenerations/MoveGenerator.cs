using System.ComponentModel;
using System.IO.Pipes;
using System.Text.RegularExpressions;
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
    private static List<Move> moves = new();
    // all valid knight's moves offset
    private static readonly int[] knightOffsets = {17, 15, 10, 6, -6, -10, -15, -17};

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
                numSquaresToEdge[squareIndex, 4] = Math.Min(numNorth, numWest);
                numSquaresToEdge[squareIndex, 5] = Math.Min(numSouth, numWest);
                numSquaresToEdge[squareIndex, 6] = Math.Min(numNorth, numEast);
                numSquaresToEdge[squareIndex, 7] = Math.Min(numSouth, numEast);
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
                else if (Piece.pieceType(piece) == Piece.Pawn)
                {
                    GeneratePawnMoves(startSquare, piece, board);
                }
                else if(Piece.pieceType(piece) == Piece.King)
                {
                    GenerateKnightMoves(startSquare, piece, board);
                }
            }
        }
        return moves;
    }
    // knight moves
    public static void GenerateKnightMoves(int startSquare, int piece, Board board)
    {
        int startFile = startSquare % 8;
        int startRank = startSquare / 8;
        // !repeated code
        int opponentColor = board.colorToMove ^ (Piece.White | Piece.Black);
        //!repeated code
        int friendlyColor = board.colorToMove;
        foreach (int offset in knightOffsets)
        {
            int targetSquare = startSquare + offset;

            if (targetSquare >= 0 && targetSquare < 64)
            {
                int targetFile = targetSquare % 8;
                int targetRank = targetSquare / 8;

                // Calculate absolute distance in ranks and files
                int fileChange = Math.Abs(targetFile - startFile);
                int rankChange = Math.Abs(targetRank - startRank);

                // A valid "L" jump ALWAYS changes 2 files and 1 rank, OR 1 file and 2 ranks
                if ((fileChange == 1 && rankChange == 2) || (fileChange == 2 && rankChange == 1))
                {
                    int targetPiece = board[targetSquare];
                   if (!Piece.isColor(targetPiece,friendlyColor))
                {
                    moves.Add(new Move(startSquare, targetSquare));
                }
                }
            }
        }
    }
    // pawn moves
    public static void GeneratePawnMoves(int startSquare, int piece, Board board)
    {
        // white paws move up the board +8
        // black pawns move down the board -8
        int pawnMoveOffset = (board.colorToMove == Piece.White) ? 8 : -8;
        int startRank = (board.colorToMove == Piece.White) ? 1 : 6;
        int promotionRank = (board.colorToMove == Piece.White) ? 7 : 0;

        int currentRank = startSquare / 8;
        int currentFile = startSquare % 8;
        int targetSquare = startSquare + pawnMoveOffset;
        // single push move
        if (targetSquare >= 0 && targetSquare < 64 && board[targetSquare] == Piece.None)
        {
            if (targetSquare / 8 == promotionRank)
            {
                // Add 4 promotion variations
            }
            else
            {
                moves.Add(new Move(startSquare, targetSquare));
            }

            //adding double-step INSIDE the single-push block
            // A pawn CANNOT double step if the square directly in front of it is blocked
            int doubleStepTarget = startSquare + (pawnMoveOffset * 2);
            if (currentRank == startRank && board[doubleStepTarget] == Piece.None)
            {
                moves.Add(new Move(startSquare, doubleStepTarget));
            }
        }
        // diagonal captures
        // Calculate diagonal offsets based on color
        int leftCaptureOffset = (board.colorToMove == Piece.White) ? 7 : -9;
        int rightCaptureOffset = (board.colorToMove == Piece.White) ? 9 : -7;
        int opponentColor = board.colorToMove ^ (Piece.White | Piece.Black);

        // Left capture (Only safe if NOT on File A -> currentFile > 0)
        if (currentFile > 0)
        {
            int leftTarget = startSquare + leftCaptureOffset;
            if (leftTarget >= 0 && leftTarget < 64)
            {
                int targetPiece = board[leftTarget];
                if ((targetPiece != Piece.None && Piece.isColor(targetPiece, opponentColor)) || leftTarget == board.EnPassantSquare)
                {
                    if (leftTarget / 8 == promotionRank)
                    {
                        // TODO: Add promotion capture variations
                    }
                    else
                    {
                        moves.Add(new Move(startSquare, leftTarget));
                    }

                }
            }
        }

        // Right capture (Only safe if NOT on File H -> currentFile < 7)
        if (currentFile < 7)
        {
            int rightTarget = startSquare + rightCaptureOffset;
            if (rightTarget >= 0 && rightTarget < 64)
            {
                int targetPiece = board[rightTarget];
                if ((targetPiece != Piece.None && Piece.isColor(targetPiece, opponentColor)) || rightTarget == board.EnPassantSquare)
                {
                    if (rightTarget / 8 == promotionRank)
                    {
                        // TODO: Add promotion capture variations
                    }
                    else
                    {
                        moves.Add(new Move(startSquare, rightTarget));
                    }
                }
            }

        }
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