using System.Linq;
using UnityEngine;

/// <summary>
/// Item picker and executer
/// </summary>
public class ItemPicker : MonoBehaviour
{
    public Character _user;
    public ItemData CurrentItemData; // just one slot
    private ItemSlot _slot; // just one slot

    public bool HasItem => CurrentItemData != null;
    public bool CurrentItemNeedsTarget => CurrentItemData.NeedsTargetInteractable;

    private void Start()
    {
        _slot = FindObjectsOfType<ItemSlot>().Where(s => s.Rave == _user.CharacterColor).First();
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item;
        if (other.TryGetComponent<Item>(out item))
        {
            CurrentItemData = item.Data;
            // update ui
            item.Pick(_user.CharacterColor);
        }
    }

    public void UseItem()
    {
        CurrentItemData = null;
        _slot.HideItem();
        // update ui
    }
}