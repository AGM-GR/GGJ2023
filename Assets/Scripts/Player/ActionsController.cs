using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using System;
using System.Threading.Tasks;

public class ActionsController : MonoBehaviour
{
    public ItemPicker _itemPicker;
    public TextMeshProUGUI _debugText;
    private Interactable _targetInteractable;
    
    private Character _character;
    private CharacterMovement _characterMovement;
    private CharacterDJMinigameInteraction characterDJMinigameInteraction;
    
    private Animator _animator => _character.CharacterAnimator;

    public GameObject StunnerTest;

    private Coroutine stunnerCoroutine;

    public ParticleSystem DrinkParticleSystem;

    public AudioClip drinkSfx;
    public List<AudioClip> drinkMusics;
    private AudioSource _aSource;


    private void Awake()
    {
        _character = GetComponent<Character>();
        _characterMovement = GetComponent<CharacterMovement>();
        characterDJMinigameInteraction = GetComponent<CharacterDJMinigameInteraction>();
        _aSource = GetComponent<AudioSource>();
    }

    public void OnStartMinigame()
    {
        _targetInteractable?.Interact(_itemPicker);
    }

    public void OnExecuteAction()
    {
        ExecuteAction();
    }


    // Actually: Use item
    private async void ExecuteAction()
    {
        if (_itemPicker.HasItem && !characterDJMinigameInteraction.InDJMinigame)
        {
            _animator.SetTrigger(_itemPicker.CurrentItemData.AnimationTrigger);

            if (!_itemPicker.CurrentItemNeedsTarget)
            {
                await HandleNonTargetItems();
            }
            else
            {
                if (_targetInteractable != null)
                {
                    _targetInteractable.Interact(_itemPicker);
                }
            }

            _itemPicker.UseItem();
        }
    }

    private async Task HandleNonTargetItems()
    {
        switch (_itemPicker.CurrentItemData.Type)
        {
            case ItemType.BaseballBat:
                StunnerTest.SetActive(true);
                if (stunnerCoroutine != null)
                    StopCoroutine(stunnerCoroutine);
                stunnerCoroutine = StartCoroutine(DisableStunnerTest(1.17f));
                break;
            case ItemType.EnergyDrink:

                DrinkParticleSystem.gameObject.SetActive(true);
                MusicController.Instance.PlayEnergyDrink(_character.CharacterColor);
                await ZoomCamera();
                _characterMovement.AddSpeedUp();
                break;
        }
    }

    private async Task ZoomCamera()
    {
        _character.ZoomController.ZoomIn();
        await Task.Delay(1500);
        _character.ZoomController.ZoomOut();
    }

    private IEnumerator DisableStunnerTest(float stunerTime)
    {
        yield return new WaitForSeconds(stunerTime);
        StunnerTest.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable;
        if (other.TryGetComponent<Interactable>(out interactable))
        {
            _targetInteractable = interactable;

            if (interactable is Car)
            {
                (interactable as Car).highlightingCharacter = this._character;
                interactable.Highlight();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {

        Interactable interactable;
        if (other.TryGetComponent<Interactable>(out interactable))
        {
            _targetInteractable = null;

            if (interactable is Car)
            {
                (interactable as Car).highlightingCharacter = this._character;
                interactable.Unhighlight();
            }
        }
    }
}
