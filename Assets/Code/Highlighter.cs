using UnityEngine;

public class Highlighter : MonoBehaviour
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