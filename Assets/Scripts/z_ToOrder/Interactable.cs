using UnityEngine;

public abstract class Interactable : MonoBehaviour, IHighlighteable
{
    public OutlineHighlighter _highlighter;

    public virtual void Highlight()
    {
        _highlighter.Highlight();
    }

    public virtual void Unhighlight()
    {
        _highlighter.Unhighlight();
    }

    public abstract void Interact(ItemPicker picker);
}
