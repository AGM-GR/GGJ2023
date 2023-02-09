using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaversExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raver"))
        {
            int raverAmount = 1;
            RaverBase raverToDisable = other.GetComponent<RaverBase>();
            if (raverToDisable.RaversGroup != null) {
                raverToDisable = raverToDisable.RaversGroup;
                raverAmount = ((RaversGroup)raverToDisable).AmountOfRavers;
            }

            raverToDisable.DisableRaver();

            GetComponentInParent<Car>()?.IncreaseRavers(raverAmount);
        }
    }
}
