using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string Name;
    public ItemType Type;
    // preview?
    public GameObject Prefab;
    public Sprite PreviewSprite;

    public bool NeedsTargetInteractable;
    public string AnimationTrigger;
}