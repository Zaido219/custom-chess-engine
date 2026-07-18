using System.Runtime.CompilerServices;

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
    public void Move(string move)
    {
        // moves will look something like this : e2e4
        // split the string by
        string sourceSquare = move.Substring(0,2);
        string destinationSquare = move.Substring(2,2);
        // match first char to the map
    }
}