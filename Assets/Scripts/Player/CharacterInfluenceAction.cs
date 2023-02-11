using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfluenceAction : MonoBehaviour
{
    public CharacterInfluenceTrigger influenceTrigger;

    private Character thisCharacter;
    private Car mycar;

    public bool CanInfluence { get; set; }
    public bool AutoInfluence { get; set; }

    private void OnValidate()
    {
        if (influenceTrigger == null)
            influenceTrigger = GetComponentInChildren<CharacterInfluenceTrigger>();
    }

    private void Awake()
    {
        CanInfluence = false;
        thisCharacter = GetComponent<Character>();
        foreach (Car car in FindObjectsByType<Car>(FindObjectsSortMode.None))
        {
            if (car.CarColor == thisCharacter.CharacterColor)
                mycar = car;
        }
    }

    private void Update()
    {
        if (AutoInfluence && CanInfluence)
        {
            foreach (RaverBase raver in influenceTrigger.raversInInfluenceRange)
            {
                raver.InfluencedByPlayer(thisCharacter.CharacterColor, mycar, true);
            }
        }
    }

    public void OnInfluenceRaver()
    {
        if (CanInfluence)
        {
            RaverBase closerRaver = null;
            float raverDistance = float.PositiveInfinity;
            foreach (RaverBase raver in influenceTrigger.raversInInfluenceRange)
            {
                float distanceToRaver = (raver.transform.position - transform.position).sqrMagnitude;
                if (distanceToRaver < raverDistance)
                {
                    raverDistance = distanceToRaver;
                    closerRaver = raver;
                }
            }

            if (closerRaver != null)
            {
                closerRaver.InfluencedByPlayer(thisCharacter.CharacterColor, mycar);
            }
        }
    }
}
