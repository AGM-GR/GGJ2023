using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRPPTest : MonoBehaviour
{
    [SerializeField] RaveColor color;
    [SerializeField] Transform raveTarget;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Raver raver)) {
            raver.InfluencedByPlayer(color, raveTarget.position);
        }
    }
}
