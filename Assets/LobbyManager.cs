using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public PlayerInputManager InputManager;
    public int ConnectedPlayersAmount;
    public Button PlayGameButton;

    private void Awake()
    {
        PlayGameButton.interactable = false;
        PlayGameButton.onClick.AddListener(StartGame);
    }

    public void OnPlayerJoined()
    {
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
        InputManager.enabled = false;
        this.gameObject.SetActive(false);
    }

    private static void AllowPlayersMovement()
    {
        var characterMovements = FindObjectsOfType<CharacterMovement>().ToList();
        characterMovements.ForEach(o => o.IsMovementAllowed = true);
    }
}
