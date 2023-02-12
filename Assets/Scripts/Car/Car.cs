using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

// DJ table
public class Car : Interactable
{
    [SerializeField] CarColor color;
    [SerializeField] TextMeshProUGUI raversAmountText;
    [SerializeField] TextMeshProUGUI raversSpeedMultiplierText;
    [SerializeField] DJMinigame djMinigame;
    [SerializeField] float raverLossPeriod;

    [SerializeField] Animator animator;
    [SerializeField] CanvasGroup fliparteHint;
    [SerializeField] GameObject sabotearHint;
    [SerializeField] GameObject sabotageFx;


    CharacterDJMinigameInteraction interaction;
    float carInfluence;
    int currentRavers;

    RaversExit pointsExit;

    [HideInInspector] public bool losingRavers;
    float timeSinceLastLoss;

    public AudioSource aSource;
    public AudioClip sabotageSfx;

    [HideInInspector] public Character highlightingCharacter;

    public GameObject minusOneFx;


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
                ShowMinusOneFx();
            }
        }
    }

    private void ShowMinusOneFx()
    {
        minusOneFx.gameObject.SetActive(false);
        minusOneFx.gameObject.SetActive(true);
    }


    // en realidad es la mesa de DJ
    public override void Highlight()
    {
        base.Highlight();

        if (IsCarOwner(highlightingCharacter))
        {
            if (!djMinigame.IsOnMaxTier)
            {
                fliparteHint.alpha = 1;
            }
        }
        else
        {
            sabotearHint.SetActive(true);
        }
    }



    public override void Unhighlight() {

        base.Unhighlight();

        if (IsCarOwner(highlightingCharacter))
        {
            fliparteHint.alpha = 0;
        }
        else
        {
            sabotearHint.SetActive(false);
        }
    }

    public void ShowFliparteHint() {
        fliparteHint.alpha = 1;
    }

    public override void Interact(ItemPicker picker)
    {
        if (LobbyManager.Instance.GameStarted)
        {
            var character = picker.GetComponent<Character>();
            if (IsCarOwner(character))
            {
                StartMinigameIfPossible(character);
            }
            else
            {
                if (!picker.HasItem) return;
                if (picker.CurrentItemData.Type == ItemType.Scissors)
                {
                    Debug.Log("Revienta carro!");
                    Sabotage();
                    Unhighlight();
                }
            }
        }
    }

    private bool IsCarOwner(Character character)
    {
        return character.CharacterColor == color;
    }

    private void Sabotage()
    {
        sabotageFx.SetActive(false);
        sabotageFx.SetActive(true);
        aSource.PlayOneShot(sabotageSfx);
        djMinigame.LowestTier();
        losingRavers = true;
    }


    private void StartMinigameIfPossible(Character character)
    {
        if (!djMinigame.MinigameActive && !djMinigame.IsOnMaxTier)
        {
            character.GetComponent<CharacterDJMinigameInteraction>().DJMinigame = djMinigame;
            character.GetComponent<CharacterMovement>().IsMovementAllowed = false;
            djMinigame.Activate(character);
            character.CharacterAnimator.SetTrigger("Scratch");
            fliparteHint.alpha = 0;
        }
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
