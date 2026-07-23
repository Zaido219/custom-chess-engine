using System.Numerics;

namespace Chess.UI;

public class DragAndDropHandler : IDragAndDropHandler
{
    public int selectedSquareIndex { get; private set; }
    public bool isDragging { get; private set; } // public getter; private setter restricts mutation to this class
    public Vector2 currentDragPosition { get; set; }
    private Vector2 _dragOffset; // fully hidden implementatio details
    // purpose of this method is simply to update internal states
    public void BeginDrag(int squareIndex, Vector2 mousePosition, Vector2 tileOrigin)
    {
        // we are dragging
        isDragging = true; // update state; others are listening to this
        selectedSquareIndex = squareIndex; // keeps track of which square is clicked
        _dragOffset = mousePosition - tileOrigin; // Calculates the exact spot inside the tile where the cursor was clicked.
        currentDragPosition = tileOrigin; // Initializes the starting draw coordinates for the piece, its also updates
        // frame by frame  as the mouse moves
    }
    public void UpdateDrag(Vector2 mousePosition)
    {
        if(!isDragging) return;
        currentDragPosition = mousePosition - _dragOffset;
    }
    public void EndDrag(out int squareIndex)
    {
        squareIndex = selectedSquareIndex; //Hands off the origin square index back to the caller using the out parameter.
        isDragging = false;
        selectedSquareIndex = -1; // Resets internal state to a safe default.
        //Setting it to -1 (an invalid board array index) ensures that no piece is accidentally 
        // referenced or modified when a drag is not active.
    }
    public void CancelDrag()
    {
        // we are not dragging anymore
        isDragging = false;
        // !repeated code
        selectedSquareIndex = -1;
    }
}