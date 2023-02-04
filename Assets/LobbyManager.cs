using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public int ConnectedPlayersAmount;
    public Button PlayGameButton;

    private void Awake()
    {
        PlayGameButton.onClick.AddListener(() => this.gameObject.SetActive(false));
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
}
