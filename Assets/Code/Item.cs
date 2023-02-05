using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public ItemSlot Slot;

    public ItemsSpawner Spawner;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Pick(CarColor color)
    {
        // anim, vfx, etc
        gameObject.SetActive(false);
        Slot = FindObjectsOfType<ItemSlot>().Where(s => s.Rave == color).First();
        Slot.ShowItem(Data);

        Spawner.ItemDisabled();
        _animator.SetTrigger("OpenBox");
    }

}
