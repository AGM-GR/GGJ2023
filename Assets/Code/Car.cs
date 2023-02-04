using UnityEngine;

public class Car : Interactable
{
    private int _carInfluenceLevel; // ??

    public override void Interact(ItemPicker picker)
    {
        if (!picker.HasItem) return;

        switch (picker.CurrentItemData.Type)
        {
            case ItemType.None:
                break;
            case ItemType.BaseballBat:
                break;
            case ItemType.Scissors:
                // reventar carro
                Debug.Log("Revienta carro!");
                picker.UseItem();
                // _carInfluenceLevel
                break;
            default:
                break;
        }
    }
}
