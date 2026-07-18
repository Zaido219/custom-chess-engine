using Chess.Core.BoardState;
using Chess.Core.MoveGenerations;

namespace Chess.UI;
public class ConsoleInputHandler()
{
    // map letters to file (note that files are the vertical columns)
    private static readonly Dictionary<char, int> FileMap = new Dictionary<char, int>
    {
        ['a'] = 0,
        ['b'] = 1,
        ['c'] = 2,
        ['d'] = 3,
        ['e'] = 4,
        ['f'] = 5,
        ['g'] = 6,
        ['h'] = 7,
    };
    public void Move(string move, Board board)
    {
        // TODO:Have some checking for that move string, malformed and invalid
        // moves will look something like this : e2e4
        // split the string by 2
        string sourceSquare = move.Substring(0,2);
        string destinationSquare = move.Substring(2,2);
        // get the coordinate
        int parsedSource = ParseSquareName(sourceSquare);
        int parsedDestination = ParseSquareName(destinationSquare);
        Move newMove = new Move(parsedSource, parsedDestination);
        //update board's state with that new move
        board.MakeMove(newMove);
    }
    private int ParseSquareName(string squareName)
    {
        char fileChar = squareName[0];
        char rankChar = squareName[1];

        int file = FileMap[fileChar];
        int rank = rankChar - '1'; // in c# chars is secretly just a number, this expression will be evaluated to thier
        // underlying numerical value

        return (rank * 8) + file;
    }
}