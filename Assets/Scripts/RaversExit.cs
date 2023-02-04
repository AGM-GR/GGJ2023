using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaversExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raver"))
        {
            other.GetComponent<Raver>().DisableRaver();
        }
    }
}
