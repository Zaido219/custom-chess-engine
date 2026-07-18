using Chess.UI;
using Chess.Core.BoardState;
// console doesnt support  unicode out of the box
System.Console.OutputEncoding = System.Text.Encoding.UTF8;
Board board = new Board();
FenUtility fenUtility = new FenUtility();
fenUtility.loadFen("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2", board);

BoardRenderer.Render(board);

// test only

Console.Write("This is a stupid way to remind but, you have turned off smart-control, turn it back-on before ending coding session");