using System.Numerics;
using System.Runtime.CompilerServices;

namespace Chess.UI;

public interface IDragAndDropHandler
{
    int selectedSquareIndex {get;} // the piece being dragged
    bool isDragging {get;} // indicates if a drag operation is active
    Vector2 currentDragPosition {get;set;} //updating continuously to draw the piece directly under the 
                                            //cursor while dragging.
    void BeginDrag(int squareIndex, Vector2 mousePosition, Vector2 tileOrigin);
    void UpdateDrag(Vector2 mousePosition);
    void EndDrag(out int squareIndex);
    void CancelDrag();
}