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
        _inputManager.onPlayerLeft += (p) => OnPlayerLeft(p);

        //characterBanners.ForEach(a => a.SetTrigger("PlayerEntry"));
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Player joined!");
        // Assign rave to a player
        characterBanners[player.playerIndex].SetTrigger("PlayerEntry");
        player.GetComponent<PlayerUser>().Rave = (CarColor)player.playerIndex;

        ConnectedPlayersAmount++;
        RefreshPlayButton();
    }

    private void RefreshPlayButton()
    {
        PlayGameButton.interactable = ConnectedPlayersAmount >= 2;
    }

    private void OnPlayerLeft(PlayerInput player)
    {
        Debug.Log("Player left!");

        // Assign rave to a player
        characterBanners[player.playerIndex].SetTrigger("PlayerDisconnect");

        ConnectedPlayersAmount--;
        RefreshPlayButton();

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
        characterMovements.ForEach(o => {
            o.GetComponent<CharacterInfluenceAction>().CanInfluence = true;
            o.IsMovementAllowed = true;
        });
    }
}
