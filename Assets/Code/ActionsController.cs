using UnityEngine;
using TMPro;


public class ActionsController : MonoBehaviour
{
    public SphereCollider SphereCollider;
    private bool _canExecuteAction;

    public ItemPicker _itemPicker;

    public TextMeshProUGUI _debugText;

    private Interactable _targetInteractable;
    private Animator _animator;


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        SphereCollider.isTrigger = true;
    }

    public void OnExecuteAction()
    {
        if (_canExecuteAction)
        {
            ExecuteAction();
        }
    }


    // Use item
    private void ExecuteAction()
    {
        if(_itemPicker.HasItem)
        {
            _animator.SetTrigger(_itemPicker.CurrentItemData.AnimationTrigger);

            if (_itemPicker.CurrentItemNeedsTarget && _targetInteractable != null)
            {
                _targetInteractable.Interact(_itemPicker);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable;
        if (other.TryGetComponent<Interactable>(out interactable))
        {
            _canExecuteAction = true;
            _targetInteractable = interactable;
            interactable.Highlight();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        _canExecuteAction = false;

        Interactable interactable;
        if (other.TryGetComponent<Interactable>(out interactable))
        {
            _targetInteractable = null;
            interactable.Unhighlight();
        }
    }
}
