using Chess.Core.BoardState;
namespace Chess.UI;

public interface IBoardRenderer
{
    void Render(Board board);
}