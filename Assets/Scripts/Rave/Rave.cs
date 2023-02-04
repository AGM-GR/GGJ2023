using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rave : MonoBehaviour
{
    [SerializeField] CarColor color;
    [SerializeField] TextMeshProUGUI raversAmountText;

    public CarColor RaveColor { get { return color; } }
    public float RaveInfluence { get; set; }
    public DJMinigame DJMinigame { get; set; }

    int currentRavers;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Raver raver)) {
            currentRavers++;
            UpdateAmountText();
            Destroy(other.gameObject);
        }

    }

    private void UpdateAmountText() {
        raversAmountText.text = currentRavers.ToString();
    }    

    public void Sabotage() {
        DJMinigame.LowestTier();
    }
}
