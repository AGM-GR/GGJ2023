using UnityEngine;
using UnityEngine.InputSystem;

public class RoleController : MonoBehaviour
{
    private Role _currentRole;
    private int _currentRoleIndex;
    private Role[] _roles = new Role[3];
    private PlayerActions_ _playerActions;

    public SphereCollider SphereCollider;
    private bool _canExecuteAction;

    private void Awake()
    {
        _playerActions = new PlayerActions_();
        _playerActions.Enable();

        _playerActions.Player.ExecuteRoleAction.performed += ctx => ExecuteRoleAction(ctx);
        _playerActions.Player.SwitchRole.performed += ctx => SwitchRole(ctx);

        SphereCollider.isTrigger = true;

        _roles[0] = new PR();
        _roles[1] = new Badass();
        _roles[2] = new Dj();

        _currentRole = _roles[0];
    }


    public void SwitchRole(InputAction.CallbackContext ctx)
    {
        Debug.Log("Switch role");

        _currentRoleIndex++;
        if(_currentRoleIndex >= _roles.Length) _currentRoleIndex = 0;

        _currentRole = _roles[_currentRoleIndex];
        SphereCollider.radius = _currentRole.Radius;
    }

    public void ExecuteRoleAction(InputAction.CallbackContext ctx)
    {
        Debug.Log("Execute action");

        if (_canExecuteAction)
        {
            _currentRole.ExecuteAction();
        }
    }

    private void OnDestroy()
    {
        _playerActions.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("trigger");
        if (other.CompareTag(_currentRole.TargetTag))
        {
            _canExecuteAction = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canExecuteAction = false;
    }
}