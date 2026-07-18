using Chess.UI;
using Chess.Core.BoardState;
// console doesnt support  unicode out of the box
System.Console.OutputEncoding = System.Text.Encoding.UTF8;
string startingPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
Board board = new Board();
FenUtility fenUtility = new FenUtility();
fenUtility.loadFen(startingPosition, board);

BoardRenderer.Render(board);

// test only

Console.Write("This is a stupid way to remind but, you have turned off smart-control, turn it back-on before ending coding session");