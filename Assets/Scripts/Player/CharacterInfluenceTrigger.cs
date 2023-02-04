using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfluenceTrigger : MonoBehaviour
{
    public List<RaverBase> raversInInfluenceRange { get; private set; }

    private void Awake()
    {
        raversInInfluenceRange = new List<RaverBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raver"))
        {
            RaverBase newRaver = other.GetComponent<RaverBase>();
            if (newRaver.RaversGroup != null)
                newRaver = newRaver.RaversGroup;

            raversInInfluenceRange.Add(newRaver);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Raver"))
        {
            RaverBase raverToRemove = other.GetComponent<RaverBase>();
            if (raverToRemove.RaversGroup != null)
                raverToRemove = raverToRemove.RaversGroup;

            raversInInfluenceRange.Remove(raverToRemove);
        }
    }
}
