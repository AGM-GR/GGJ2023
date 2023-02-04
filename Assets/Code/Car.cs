using TMPro;
using UnityEngine;

public class Car : Interactable
{
    [SerializeField] CarColor color;
    [SerializeField] TextMeshProUGUI raversAmountText;
    [SerializeField] DJMinigame djMinigame;

    CharacterDJMinigameInteraction interaction;
    CharacterMovement movement;

    public CarColor CarColor { get { return color; } }

    float carInfluence;
    int currentRavers;

    private void Start() {
        djMinigame.SetCar(this);
    }

    public override void Interact(ItemPicker picker)
    {
        if (picker.GetComponent<Character>().CharacterColor == color) {
            picker.GetComponent<CharacterDJMinigameInteraction>().DJMinigame = djMinigame;
            picker.GetComponent<CharacterMovement>().IsMovementAllowed = false;
            djMinigame.Activate(picker.GetComponent<Character>());
        } else {
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

    public void SetInfluence(float carInfluence) {
        this.carInfluence = carInfluence;
    }
}