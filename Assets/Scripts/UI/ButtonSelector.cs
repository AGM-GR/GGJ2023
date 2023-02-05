using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    public bool startSelected;
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        if (startSelected)
        {
            Select();
        }
    }

    public void Select()
    {
        thisButton.Select();
    }
}
