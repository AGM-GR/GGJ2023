using UnityEngine;

public class OutlineHighlighter : MonoBehaviour, IHighlighteable
{
    public Material Mat;

    public void Highlight()
    {
        Mat.SetInt("_OutlineWidth", 8);
    }

    public void Unhighlight()
    {
        Mat.SetInt("_OutlineWidth", 0);
    }
}