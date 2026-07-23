using Chess.Core.BoardState;
namespace Chess.UI;

public interface IBoardRenderer
{
    void Init();
    void Render(Board board);
}