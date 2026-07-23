using System.Numerics;

namespace Chess.UI;

public class DragAndDropHandler : IDragAndDropHandler
{
    public int selectedSquareIndex { get; }
    public bool isDragging { get; }
    public Vector2 currentDragPosition { get; set; }

    void BeginDrawing(int squareIndex, Vector2 mousePosition, Vector2 tileOrigin)
    {

    }
    void UpdateDrag(Vector2 mousePosition)
    {

    }
    void EndDrag(out int squareIndex)
    {

    }
    void CancelDrag()
    {

    }
}