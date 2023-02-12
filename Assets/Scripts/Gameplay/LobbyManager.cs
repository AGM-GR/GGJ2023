using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { get; private set; }
    public int MinPlayerAmount = 2;

    private PlayerInputManager _inputManager;
    public int ConnectedPlayersAmount;
    public Button PlayGameButton;
    public MusicController musicController;
    public List<Animator> characterBanners;
    public Animator joinTextAnimator;

    [Header("Lobby Input Actions")]
    [SerializeField] InputAction startGame = null;

    public bool GameStarted { get; set; }

    private void Awake()
    {
        Instance = this;

        PlayGameButton.interactable = false;
        PlayGameButton.onClick.AddListener(StartGame);

        _inputManager = FindObjectOfType<PlayerInputManager>();
        _inputManager.onPlayerJoined += (p) => OnPlayerJoined(p);
    }

    private void Update()
    {
        if (startGame.WasPressedThisFrame())
        {
            PlayGameButton.OnSubmit(null);
        }
    }

    private void OnEnable()
    {
        startGame.Enable();
    }

    private void OnDisable()
    {
        startGame.Disable();
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("control scheme: " + player.currentControlScheme);
        PlayBannerAnimation(player);
        InitializeCharacter(player);
        ConnectedPlayersAmount++;
        RefreshPlayButton();
    }

    private void PlayBannerAnimation(PlayerInput player)
    {
        characterBanners[player.playerIndex].SetTrigger("PlayerEntry");
        characterBanners[player.playerIndex].Play("Loop", 1, GetPreviousAnimatorsNormalizedTime(player.playerIndex));
    }

    private static void InitializeCharacter(PlayerInput player)
    {
        var character = player.GetComponent<Character>();
        character.Initialize(player.playerIndex);
    }

    private void RefreshPlayButton()
    {
        bool canPlay = ConnectedPlayersAmount >= MinPlayerAmount;
        PlayGameButton.interactable = canPlay;
        if (canPlay)
        {
            PlayGameButton.GetComponent<Animator>().Play("Normal", 0, GetPreviousAnimatorsNormalizedTime(ConnectedPlayersAmount));
        }
    }


    public void StartGame()
    {
        AllowPlayersMovement();
        _inputManager.enabled = false;
        this.gameObject.SetActive(false);
        FindObjectOfType<ItemsSpawner>().enabled = true;
        FindObjectOfType<GameplayTimer>().enabled = true;
        musicController.StartMusicAndGames();
        GameStarted = true;
    }

    private static void AllowPlayersMovement()
    {
        var characterMovements = FindObjectsOfType<CharacterMovement>().ToList();
        characterMovements.ForEach(o =>
        {
            o.GetComponent<CharacterInfluenceAction>().CanInfluence = true;
            o.IsMovementAllowed = true;
        });
    }

    private float GetPreviousAnimatorsNormalizedTime(int index) {
        if (index == 0) {
            return joinTextAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        } else {
            return characterBanners[index - 1].GetCurrentAnimatorStateInfo(1).normalizedTime;
        }
    }
}
