using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// to check
public class InputSelectionManager : MonoBehaviour
{
    PlayerInputManager inputManager;

    [Header("Two players on keyboard")]
    [SerializeField] GameObject keyboardOnlyP1;
    [SerializeField] GameObject keyboardOnlyP2;

    [Header("One player on keyboard, other player on controller")]
    [SerializeField] GameObject keyboardP1;
    [SerializeField] GameObject controllerP2;

    [Header("Both players on same controller")]
    [SerializeField] GameObject controllerOnlyP1;
    [SerializeField] GameObject controllerOnlyP2;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        ChooseInputMode(0);
    }

    public void ChooseInputMode(int mode)
    {
        switch (mode)
        {
            case 0:
                inputManager.playerPrefab = keyboardOnlyP1;
                inputManager.JoinPlayer(0, -1, "Keyboard", Keyboard.current);
                inputManager.playerPrefab = keyboardOnlyP2;
                inputManager.JoinPlayer(1, -1, "Keyboard", Keyboard.current);
                break;
            case 1:
                inputManager.playerPrefab = keyboardP1;
                inputManager.JoinPlayer(0, -1, "Keyboard", Keyboard.current);
                inputManager.playerPrefab = controllerP2;
                inputManager.JoinPlayer(1, -1, "Controller", Gamepad.current);
                break;
            case 2:
                inputManager.playerPrefab = controllerOnlyP1;
                inputManager.JoinPlayer(0, -1, "JoinedController", Gamepad.current);
                inputManager.playerPrefab = controllerOnlyP2;
                inputManager.JoinPlayer(1, -1, "JoinedController", Gamepad.current);
                break;
        }

    }
}
