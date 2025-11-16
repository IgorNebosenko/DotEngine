namespace DotEngine;

public interface IMouseExtensions
{
    void OnMouseDown();
    void OnMouseDrag();
    void OnMouseEnter();
    void OnMouseExit();
    void OnMouseOver();
    void OnMouseUp();
    
    void OnMouseUpAsButton();
}