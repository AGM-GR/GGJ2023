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
    public List<GameObject> characterBanners;


    private int _currentPlayerIndex = 0;

    private void Awake()
    {
        PlayGameButton.interactable = false;
        PlayGameButton.onClick.AddListener(StartGame);

        _inputManager = FindObjectOfType<PlayerInputManager>();
        _inputManager.onPlayerJoined += (p) => OnPlayerJoined(p);

        characterBanners.ForEach(b => b.SetActive(false));
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Player joined!");
        // Assign rave to a player
        characterBanners[_currentPlayerIndex].SetActive(true);
        player.GetComponent<PlayerUser>().Rave = (CarColor)_currentPlayerIndex;
        _currentPlayerIndex++;

        ConnectedPlayersAmount++;
        if (ConnectedPlayersAmount >= 2)
        {
            PlayGameButton.interactable = true;
        }
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
        characterMovements.ForEach(o => o.IsMovementAllowed = true);
    }
}
