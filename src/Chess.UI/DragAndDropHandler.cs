using System.Numerics;

namespace Chess.UI;

public class DragAndDropHandler : IDragAndDropHandler
{
    public int selectedSquareIndex { get; }
    public bool isDragging { get; }
    public Vector2 currentDragPosition { get; set; }

    public void BeginDrag(int squareIndex, Vector2 mousePosition, Vector2 tileOrigin)
    {

    }
    public void UpdateDrag(Vector2 mousePosition)
    {

    }
    public void EndDrag(out int squareIndex)
    {

    }
    public void CancelDrag()
    {

    }
}