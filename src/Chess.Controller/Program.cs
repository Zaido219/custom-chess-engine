using Chess.UI;
using Chess.Core.BoardState;
using System.Diagnostics.CodeAnalysis;
// console doesnt support  unicode out of the box
System.Console.OutputEncoding = System.Text.Encoding.UTF8;

Board board = new Board();
BoardRenderer.Render(board);

Console.Write("This is a stupid way to remind but, you have turned off smart-control, turn it back-on before ending coding session");