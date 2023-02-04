using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rave : MonoBehaviour
{
    [SerializeField] RaveColor color;
    [SerializeField] TextMeshProUGUI raversAmountText;

    public int SpeedMultiplier { get; set; }

    public RaveColor RaveColor { get { return color; } }

    int currentRavers;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Raver raver)) {
            currentRavers++; // Poner valor del raver
            UpdateAmountText();
            Destroy(other.gameObject);
        }
    }

    private void UpdateAmountText() {
        raversAmountText.text = currentRavers.ToString();
    }    
}
