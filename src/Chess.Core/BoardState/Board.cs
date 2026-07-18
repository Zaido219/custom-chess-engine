namespace Chess.Core.BoardState;
public class Board
{
    //represents board as a array of 64 square
    // will change this to bitboards later on
    private readonly int[] _squares;
    // method that will allow chess.ui to query whats piece is on this index
    // without knowing how the board is internally represented
    // this is the indexer
    public int this[int squareIndex]
    {
        get => _squares[squareIndex];
        // indexer so that fen utility can populate the board
        // internal means this method is only accesible to chess.core
        internal set => _squares[squareIndex] = value;
    }

    public Board()
    {
        _squares = new int[64];

        _squares[0] = Piece.White | Piece.Bishop;
        _squares[63] = Piece.Black | Piece.Queen;
    }
}