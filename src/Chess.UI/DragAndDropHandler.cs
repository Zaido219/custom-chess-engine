using System.Numerics;

namespace Chess.UI;

public class DragAndDropHandler : IDragAndDropHandler
{
    public int selectedSquareIndex { get; private set; }
    public bool isDragging { get; private set; } // public getter; private setter restricts mutation to this class
    public Vector2 currentDragPosition { get; set; }
    private Vector2 _dragOffset; // fully hidden implementatio details

    public void BeginDrag(int squareIndex, Vector2 mousePosition, Vector2 tileOrigin)
    {
        // we are dragging
        isDragging = true;
        selectedSquareIndex = squareIndex;
        _dragOffset = mousePosition - tileOrigin;
        currentDragPosition = tileOrigin;
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