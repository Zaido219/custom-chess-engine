using Chess.Core.BoardState;
namespace Chess.UI;

public interface IBoardRenderer
{
    void Init();
    // highlightedSquares is an optional propery that refers to valid move squares
    void Render(Board board, IDragAndDropHandler dragHandler, IEnumerable<int>? highlightedSquares=null);
    void Dispose();
}