using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    private PlayerInputManager _inputManager;
    public int ConnectedPlayersAmount;
    public Button PlayGameButton;
    public List<Animator> characterBanners;

    private void Awake()
    {
        PlayGameButton.interactable = false;
        PlayGameButton.onClick.AddListener(StartGame);

        _inputManager = FindObjectOfType<PlayerInputManager>();
        _inputManager.onPlayerJoined += (p) => OnPlayerJoined(p);
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        characterBanners[player.playerIndex].SetTrigger("PlayerEntry");
        InitializeCharacter(player);
        ConnectedPlayersAmount++;
        RefreshPlayButton();
    }

    private static void InitializeCharacter(PlayerInput player)
    {
        var character = player.GetComponent<Character>();
        character.Initialize(player.playerIndex);
    }

    private void RefreshPlayButton()
    {
        PlayGameButton.interactable = ConnectedPlayersAmount >= 2;
    }


    public void StartGame()
    {
        AllowPlayersMovement();
        _inputManager.enabled = false;
        this.gameObject.SetActive(false);
        FindObjectOfType<ItemsSpawner>().enabled = true;
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
}
