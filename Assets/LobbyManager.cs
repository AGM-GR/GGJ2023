using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _inputManager;
    public int ConnectedPlayersAmount;
    public Button PlayGameButton;

    private int _currentPlayerIndex;

    private void Awake()
    {
        PlayGameButton.interactable = false;
        PlayGameButton.onClick.AddListener(StartGame);

        _inputManager.onPlayerJoined += (p) => OnPlayerJoined(p);

    }

    private void OnPlayerJoined(PlayerInput player)
    {
        // Assign rave to a player
        player.GetComponent<PlayerUser>().Rave = (RaveColor)(Enum.GetValues(typeof(RaveColor)).GetValue(_currentPlayerIndex));
        _currentPlayerIndex++;

        Debug.Log("Player joined!");
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
    }

    private static void AllowPlayersMovement()
    {
        var characterMovements = FindObjectsOfType<CharacterMovement>().ToList();
        characterMovements.ForEach(o => o.IsMovementAllowed = true);
    }
}
