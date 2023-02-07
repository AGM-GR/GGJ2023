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

    [SerializeField] Animator animator;
    [SerializeField] GameObject interactHint;


    CharacterDJMinigameInteraction interaction;
    float carInfluence;
    int currentRavers;

    RaversExit pointsExit;

    [HideInInspector] public bool losingRavers;
    float timeSinceLastLoss;

    public AudioSource aSource;

    public Vector3 PointsExit { get { return pointsExit.transform.position; } }
    public CarColor CarColor { get { return color; } }
    public int CurrentRavers => currentRavers;
    public int CharacterIndex = -1;

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


    // en realidad es la mesa de DJ
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
        if (LobbyManager.Instance.GameStarted)
        {
            var character = picker.GetComponent<Character>();
            if (character.CharacterColor == color)
            {
                StartMinigameIfPossible(character);
            }
            //else
            //{
            //    if (!picker.HasItem) return;
            //    if (picker.CurrentItemData.Type == ItemType.Scissors)
            //    {
            //        Debug.Log("Revienta carro!");
            //        Sabotage();
            //        picker.UseItem();
            //    }
            //}
        }
    }


    private void StartMinigameIfPossible(Character character)
    {
        if (!djMinigame.MinigameActive)
        {
            character.GetComponent<CharacterDJMinigameInteraction>().DJMinigame = djMinigame;
            character.GetComponent<CharacterMovement>().IsMovementAllowed = false;
            djMinigame.Activate(character);
            character.CharacterAnimator.SetTrigger("Scratch");
            interactHint.SetActive(false);
        }
    }

    //private void Sabotage()
    //{
    //    aSource.PlayOneShot(sabotageSfx);
    //    djMinigame.LowestTier();
    //    losingRavers = true;
    //}

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

    public void SetAwesomessLevel(int awesomeness) {
        switch (awesomeness) {
            case 0:
                animator.SetTrigger("Mola1");
                break;
            case 1:
                animator.SetTrigger("Mola2");
                break;
            case 2:
                animator.SetTrigger("Mola3");
                break;
            case 3:
                animator.SetTrigger("Mola4");
                break;
        }
    }
}
