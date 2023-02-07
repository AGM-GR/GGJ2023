using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageTrigger : Interactable
{
    public AudioSource aSource;
    public AudioClip sabotageSfx;
    [SerializeField] DJMinigame djMinigame;
    public Car car;
    [SerializeField] GameObject interactHint;


    public override void Interact(ItemPicker picker)
    {
        if (!picker.HasItem) return;
        if (picker.CurrentItemData.Type == ItemType.Scissors)
        {
            Debug.Log("Revienta carro!");
            Sabotage();
            //picker.UseItem();
            Unhighlight();
        }
    }

    private void Sabotage()
    {
        aSource.PlayOneShot(sabotageSfx);
        djMinigame.LowestTier();
        car.losingRavers = true;
    }

    public override void Highlight()
    {
        base.Highlight();
        interactHint.SetActive(true);
    }

    public override void Unhighlight()
    {
        base.Unhighlight();
        interactHint.SetActive(false);
    }
}
