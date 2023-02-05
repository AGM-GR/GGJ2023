using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class ActionsController : MonoBehaviour
{
    public SphereCollider SphereCollider;
    public ItemPicker _itemPicker;
    public TextMeshProUGUI _debugText;
    private Interactable _targetInteractable;
    private Character _character;
    private Animator _animator => _character.CharacterAnimator;

    public GameObject StunnerTest;


    private void Awake()
    {
        _character = GetComponent<Character>();
        Initialize();
    }

    private void Initialize()
    {
        SphereCollider.isTrigger = true;
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

            StunnerTest.SetActive(true);

            if (_itemPicker.CurrentItemNeedsTarget && _targetInteractable != null)
            {
                _targetInteractable.Interact(_itemPicker);
            }

            _itemPicker.UseItem();
        }
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
