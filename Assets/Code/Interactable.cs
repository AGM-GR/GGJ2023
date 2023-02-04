using UnityEngine;

public abstract class Interactable : MonoBehaviour, IHighlighteable
{
    public OutlineHighlighter _highlighter;

    public void Highlight()
    {
        _highlighter.Highlight();
    }

    public void Unhighlight()
    {
        _highlighter.Unhighlight();
    }

    public abstract void Interact(ItemPicker picker);
}
