using Chess.UI;
using Chess.Core.BoardState;
// console doesnt support  unicode out of the box
System.Console.OutputEncoding = System.Text.Encoding.UTF8;
// Call this first so the raycaster knows where the board edges are!
MoveGenerator.preComputedMoveData();
string startingPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
Board board = new Board();
FenUtility fenUtility = new FenUtility();
fenUtility.loadFen(startingPosition, board);

BoardRenderer.Render(board);
// this is a test
// An iterable sequence of a full chess game (Fried Liver Counter-Attack)
// Each pair represents White's move followed immediately by Black's move.
string[] fullGameMoves = new string[]
{
    "e2e4", "e7e5",
    "g1f3", "b8c6",
    "f1c4", "g8f6",
    "f3g5", "d7d5",
    "e4d5", "f6d5",
    "g5f7", "e8f7",
    "d1f3", "f7e6",
    "b1c3", "c6e7",
    "d2d4", "c7c6",
    "c1g5", "h7h6",
    "g5e7", "f8e7",
    "e1c1", "h6g5",
    "h1e1", "e6d6",
    "d4e5", "d6c7",
    "c4d5", "c6d5",
    "c3d5", "c7b8",
    "f3b3", "c8e6",
    "c2c4", "d8e8",
    "h2h3", "e8f7",
    "f2f3", "e6d5",
    "c4d5", "f7f4",
    "c1b1", "e7f6",
    "b3c2", "a7a6",
    "e5e6", "h8h3",
    "g2h3", "f4b4",
    "c2b3", "b4b3",
    "a2b3", "a8h8",
    "d5d6", "b8c8",
    "e6e7", "c8d7",
    "e7e8q", "h8e8",
    "e1e8", "d7e8",
    "d1c1", "e8d7",
    "c1c7", "d7d6",
    "c7b7", "a6a5",
    "b7a7", "f6b2",
    "a7a5", "b2f6",
    "a5b5", "d6e6",
    "b5b6", "e6f5",
    "b5b5", "f5f4",
    "b5b4", "f4f3",
    "b5b4", "f3g3",
    "b5f5", "g3h3",
    "b1c2", "g5g4",
    "f5h5", "h3g2",
    "b3b4", "g4g3",
    "b4b5", "g3f2",
    "h5f5", "f2e2",
    "b5b6", "g2g1q",
    "f5f6", "g1d1" // Black Wins via Checkmate!
};
 Console.Write("This is a stupid way to remind but, you have turned off smart-control, turn it back-on before ending coding session");
// console input loop
foreach(string moveStr in fullGameMoves)
{
    Console.Clear();
    ConsoleInputHandler.Move(moveStr, board);
    BoardRenderer.Render(board);
    Console.WriteLine("-----------------------------------");
    await Task.Delay(2000);
}
