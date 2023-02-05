using System.Linq;
using TMPro;
using UnityEngine;

public class Car : Interactable
{
    [SerializeField] CarColor color;
    [SerializeField] TextMeshProUGUI raversAmountText;
    [SerializeField] TextMeshProUGUI raversSpeedMultiplierText;
    [SerializeField] DJMinigame djMinigame;
    [SerializeField] float raverLossPeriod;
    [SerializeField] GameObject interactHint;

    CharacterDJMinigameInteraction interaction;
    float carInfluence;
    int currentRavers;

    RaversExit pointsExit;

    bool losingRavers;
    float timeSinceLastLoss;

    public AudioSource aSource;
    public AudioClip sabotageSfx;

    public Vector3 PointsExit { get { return pointsExit.transform.position; } }
    public CarColor CarColor { get { return color; } }
    public int CurrentRavers => currentRavers;
    public int CharacterIndex;

    public System.Action<float> onInfluenceChanged;

    private void Awake()
    {
        raversAmountText.text = "0";
        djMinigame.SetCar(this);
        pointsExit = GetComponentInChildren<RaversExit>();
    }

    private void Update()
    {
        if (losingRavers && currentRavers > 0)
        {
            timeSinceLastLoss += Time.deltaTime;
            if (timeSinceLastLoss >= raverLossPeriod)
            {
                timeSinceLastLoss -= raverLossPeriod;
                currentRavers--;
                raversAmountText.text = currentRavers.ToString();
            }
        }
    }

    public override void Highlight() {
        base.Highlight();
        interactHint.SetActive(true);
    }

    public override void Unhighlight() {
        base.Unhighlight();
        interactHint.SetActive(false);
    }

    public void ShowInteractHint() {
        interactHint.SetActive(true);
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
                interactHint.SetActive(false);
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
        aSource.PlayOneShot(sabotageSfx);             
        djMinigame.LowestTier();
        losingRavers = true;
    }

    public void LowTierPassed()
    {
        timeSinceLastLoss = 0;
        losingRavers = false;
    }

    public void SetInfluence(float carInfluence)
    {
        this.carInfluence = carInfluence;
        raversSpeedMultiplierText.text = "X" + carInfluence.ToString();
        onInfluenceChanged?.Invoke(carInfluence);
    }

    public float GetSpeedMultiplierByInfluence()
    {
        return carInfluence;
    }

    public void IncreaseRavers(int amount)
    {
        currentRavers += amount;
        raversAmountText.text = currentRavers.ToString();
    }
}
