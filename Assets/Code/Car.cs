using TMPro;
using UnityEngine;

public class Car : Interactable
{
    [SerializeField] CarColor color;
    [SerializeField] TextMeshProUGUI raversAmountText;
    [SerializeField] DJMinigame djMinigame;

    CharacterDJMinigameInteraction interaction;
    CharacterMovement movement;
    float carInfluence;
    int currentRavers;

    RaversExit pointsExit;

    public Vector3 PointsExit { get { return pointsExit.transform.position; } }
    public CarColor CarColor { get { return color; } }

    public System.Action<float> onInfluenceChanged;

    private void Awake() {
        djMinigame.SetCar(this);
        pointsExit = GetComponentInChildren<RaversExit>();
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
                picker.GetComponent<Character>().CharacterAnimator.SetTrigger("Scratch");
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
                    Debug.Log("Revienta carro!");
                    Sabotage();
                    picker.UseItem();
                    break;
                default:
                    break;
            }
        }
    }

    private void Sabotage()
    {
        djMinigame.LowestTier(); // Lo pone a 0
    }

    public void SetInfluence(float carInfluence) {
        this.carInfluence = carInfluence;
        onInfluenceChanged?.Invoke(carInfluence);
    }

    public float GetSpeedMultiplierByInfluence()
    {
        return carInfluence;
    }

    public void IncreaseRavers(int amount) {
        currentRavers += amount;
        raversAmountText.text = currentRavers.ToString();
    }
}
