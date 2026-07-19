using System.Drawing;
using System.Reflection;
using Chess.Core.MoveGenerations;

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
    // track which color is it to move in this current turn
    // remember this are auto implemneted properties, you directly manipulate thier values
    // without wrapping them on a method
    private  int colorToMove {get; set;} = 8; // chess starts with white to move
    public Board()
    {
        _squares = new int[64];

        _squares[0] = Piece.White | Piece.Bishop;
        _squares[63] = Piece.Black | Piece.Queen;
    }
    public void MakeMove(Move move)
    {
        // get the piece from move.startSquare
        int piece = this[move.StartSquare];
        // check for none piece
        if(piece == Piece.None)
        {
            throw new Exception("Pieces to be moved can't be none");
        }
        // make sure piece matches, the active color turn
        // prevents back to back move from a one color
        // get the color of the current moving piece
        int pieceColor = Piece.color(piece);
        if(pieceColor != colorToMove)
        {
            throw new Exception("Cannot move an opponent's piece out of turn");
        }
        //assing it to move.targetSquare
        _squares[move.TargetSquare] = piece;
        //set move.StartSquare = Piece.None
        _squares[move.StartSquare] = Piece.None;
        // update state, who's color should be moving next
        // todo: there must be a better way to do this
        if(colorToMove == Piece.White)
        {
            colorToMove = Piece.Black;
        }
        else if(colorToMove == Piece.Black)
        {
            colorToMove = Piece.White;
        }
    }
}