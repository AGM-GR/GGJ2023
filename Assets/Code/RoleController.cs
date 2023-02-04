using UnityEngine;
using TMPro;

public class RoleController : MonoBehaviour
{
    private Role _currentRole;
    private int _currentRoleIndex = 0;
    private Role[] _roles = new Role[3];

    public SphereCollider SphereCollider;
    private bool _canExecuteAction;

    public TextMeshProUGUI _debugText;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        SphereCollider.isTrigger = true;

        _roles[0] = new PR();
        _roles[1] = new Badass();
        _roles[2] = new Dj();

        UpdateCurrentRole();
    }

    public void OnSwitchRole()
    {
        _currentRoleIndex++;
        if (_currentRoleIndex >= _roles.Length) _currentRoleIndex = 0;

        UpdateCurrentRole();
    }

    private void UpdateCurrentRole()
    {
        _currentRole = _roles[_currentRoleIndex];
        SphereCollider.radius = _currentRole.Radius;
        _debugText.text = _currentRole.Name;
    }

    public void OnExecuteRoleAction()
    {
        if (_canExecuteAction)
        {
            _currentRole.ExecuteAction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_currentRole.TargetTag))
        {
            _canExecuteAction = true;

            Highlighter highlighter;
            if(other.TryGetComponent<Highlighter>(out highlighter))
            {
                highlighter.Highlight();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_currentRole.TargetTag))
        {
            _canExecuteAction = true;
            TryHighlight(other);
        }
        else
        {
            _canExecuteAction = false;
            TryUnhighlight(other);
        }
    }

    private void TryHighlight(Collider other)
    {
        Highlighter highlighter;
        if (other.TryGetComponent<Highlighter>(out highlighter))
        {
            highlighter.Highlight();
        }
    }

    private void TryUnhighlight(Collider other)
    {
        Highlighter highlighter;
        if (other.TryGetComponent<Highlighter>(out highlighter))
        {
            highlighter.Unhighlight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canExecuteAction = false;

        Highlighter highlighter;
        if (other.TryGetComponent<Highlighter>(out highlighter))
        {
            highlighter.Unhighlight();
        }
    }
}
