using UnityEngine;


// TO DO
// make item spawner


public class Item : MonoBehaviour
{
    public ItemData Data;

    public void Spawn()
    {
        // anim, vfx, etc
    }

    public void Pick()
    {
        // anim, vfx, etc
        Destroy(gameObject);
    }

}
