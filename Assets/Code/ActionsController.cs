using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using System.Collections;

public class ActionsController : MonoBehaviour
{
    public ItemPicker _itemPicker;
    public TextMeshProUGUI _debugText;
    private Interactable _targetInteractable;
    private Character _character;
    private CharacterMovement _characterMovement;
    private Animator _animator => _character.CharacterAnimator;

    public GameObject StunnerTest;

    private Coroutine stunnerCoroutine;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void OnStartMinigame() {
        _targetInteractable?.Interact(_itemPicker);
    }

    public void OnExecuteAction()
    {
        ExecuteAction();
    }


    // Actually: Use item
    private void ExecuteAction()
    {
        if (_itemPicker.HasItem)
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
                    _characterMovement.AddSpeedUp();
                    break;
            }

            if (_itemPicker.CurrentItemNeedsTarget && _targetInteractable != null)
            {
                _targetInteractable.Interact(_itemPicker);
            }

            _itemPicker.UseItem();
        }
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
            interactable.Highlight();
        }
    }


    private void OnTriggerExit(Collider other)
    {

        Interactable interactable;
        if (other.TryGetComponent<Interactable>(out interactable))
        {
            _targetInteractable = null;
            interactable.Unhighlight();
        }
    }
}
