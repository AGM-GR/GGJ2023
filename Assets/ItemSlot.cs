using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image Img;
    public RaveColor Rave;

    private void Start()
    {
        Img.enabled = false;
    }

    public void ShowItem(ItemData item)
    {
        Img.enabled = true;        
        Img.sprite = item.PreviewSprite;
    }

    public void HideItem()
    {
        Img.enabled = false;
    }
}
