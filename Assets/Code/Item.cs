using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public ItemSlot Slot;

    public ItemsSpawner Spawner;

    public void Spawn()
    {
        // anim, vfx, etc
    }

    public void Pick(RaveColor color)
    {
        // anim, vfx, etc
        gameObject.SetActive(false);
        Slot = FindObjectsOfType<ItemSlot>().Where(s => s.Rave == color).First();
        Slot.ShowItem(Data);

        Spawner.ItemDisabled();
    }

}
