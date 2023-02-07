using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public GameObject ImgGO;
    public Image Img;
    public CarColor Rave;

    private void Start()
    {
        ImgGO.SetActive(false);
    }

    public void ShowItem(ItemData item)
    {
        ImgGO.SetActive(true);
        Img.sprite = item.PreviewSprite;
    }

    public void HideItem()
    {
        ImgGO.SetActive(false);
    }
}
