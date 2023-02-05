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

    private void Awake() {
        djMinigame.SetCar(this);
    }

    public override void Interact(ItemPicker picker)
    {
        if (picker.GetComponent<Character>().CharacterColor == color)
        {
            if (!djMinigame.MinigameActive)
            {
                picker.GetComponent<CharacterDJMinigameInteraction>().DJMinigame = djMinigame;
                picker.GetComponent<CharacterMovement>().IsMovementAllowed = false;
                djMinigame.Activate(picker.GetComponent<Character>());
                picker.GetComponent<Animator>().SetTrigger("Scratch");
            }
        }
        else
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

    public void SetInfluence(float carInfluence) {
        this.carInfluence = carInfluence;
    }
}
