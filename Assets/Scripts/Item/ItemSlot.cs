using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [System.Serializable]
    public struct ControlSchemeSpriteIdBinding
    {
        public string ControlScheme;
        [Space]
        public Sprite Sprite;
    }

    public GameObject ImgGO;
    public Image Img;
    public Image ButtonIconImg;
    public CarColor Rave;

    public ControlSchemeSpriteIdBinding[] ControlSchemeBindings;

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

    public void SetControlScheme(string controlScheme)
    {
        Sprite spr = ControlSchemeBindings.Where(c => c.ControlScheme == controlScheme).FirstOrDefault().Sprite;
        if(spr != null) ButtonIconImg.sprite = spr;
    }
}
