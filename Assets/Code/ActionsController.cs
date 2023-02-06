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

    public AudioClip drinkSfx;
    public List<AudioClip> drinkMusics;
    AudioSource aSource;

    private CinemachineBasicMultiChannelPerlin noise;


    private void Awake()
    {
        _character = GetComponent<Character>();
        _characterMovement = GetComponent<CharacterMovement>();
        characterDJMinigameInteraction = GetComponent<CharacterDJMinigameInteraction>();
        aSource = GetComponent<AudioSource>();
        noise = FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
    private void ExecuteAction()
    {
        if (_itemPicker.HasItem && !characterDJMinigameInteraction.InDJMinigame)
        {
            _animator.SetTrigger(_itemPicker.CurrentItemData.AnimationTrigger);

            switch (_itemPicker.CurrentItemData.Type)
            {
                case ItemType.BaseballBat:
                    StunnerTest.SetActive(true);
                    if (stunnerCoroutine != null)
                        StopCoroutine(stunnerCoroutine);
                    stunnerCoroutine = StartCoroutine(DisableStunnerTest(1.17f));
                    break;
                case ItemType.Scissors:
                    break;
                case ItemType.EnergyDrink:
                    MusicController.Instance.PlayEnergyDrink(_character.CharacterColor);
                    _characterMovement.AddSpeedUp();
                    ShakeCamera(200);
                    break;
            }

            if (_itemPicker.CurrentItemNeedsTarget && _targetInteractable != null)
            {
                _targetInteractable.Interact(_itemPicker);
            }

            _itemPicker.UseItem();
        }
    }


    private async void ShakeCamera(int msDelay)
    {
        await Task.Delay(msDelay);
        noise.m_AmplitudeGain = 1;
        await Task.Delay(100);
        noise.m_AmplitudeGain = 0;
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
            if (((Car)interactable).CarColor == _character.CharacterColor)
                interactable.Highlight();
        }
    }


    private void OnTriggerExit(Collider other)
    {

        Interactable interactable;
        if (other.TryGetComponent<Interactable>(out interactable)) {
            _targetInteractable = null;
            if (((Car)interactable).CarColor == _character.CharacterColor)
                interactable.Unhighlight();
        }
    }
}
