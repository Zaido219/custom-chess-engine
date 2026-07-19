namespace Chess.Core.BoardState;
//calculate the distance from every single one of the 64 squares to all 8 edges exactly once when the game starts up.
// this makes the engine not compute if a board will be off the board each time
// we are trading space for time in here
public static class MoveGenerator
{
    //tores the exact number of squares to the edge of the board from any given square in all 8 directions
    private static readonly int[] directionalOffsets = {8, -8, -1, 1, 7, -7, 9, -9};
    private static readonly int[,] numSquaresToEdge = new int[64,8];
    
    public static void preComputedMoveData()
    {
        for(int file = 0; file < 8; file++)
        {
            for(int rank = 0; rank < 8; rank++)
            {
                int numNorth = 7 - rank;
                int numSouth = rank;
                int numWest = file;
                int numEast = 7 - file;

                int squareIndex = rank * 8 + file;

                numSquaresToEdge[squareIndex, 0] = numNorth;
                numSquaresToEdge[squareIndex, 1] = numSouth;
                numSquaresToEdge[squareIndex, 2] = numWest;
                numSquaresToEdge[squareIndex, 3] = numSouth;
            }
        }
    }
}