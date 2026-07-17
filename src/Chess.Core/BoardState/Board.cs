namespace Chess.Core.BoardState;
public class Board
{
    //represents board as a array of 64 square
    // will change this to bitboards later on
    private readonly int[] _squares = new int[64];
    // method that will allow chess.ui to query whats piece is on this index
    // without knowing how the board is internally represented
    public int this[int squareIndex]
    {
        get => _squares[squareIndex];
    }
}