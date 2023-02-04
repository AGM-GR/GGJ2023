using UnityEngine;

public class RoleController : MonoBehaviour
{
    private Role _currentRole;
    private int _currentRoleIndex;
    private Role[] _roles = new Role[3];

    public SphereCollider SphereCollider;
    private bool _canExecuteAction;

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

        _currentRole = _roles[0];
    }

    public void OnSwitchRole()
    {
        _currentRoleIndex++;
        if(_currentRoleIndex >= _roles.Length) _currentRoleIndex = 0;

        _currentRole = _roles[_currentRoleIndex];
        SphereCollider.radius = _currentRole.Radius;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canExecuteAction = false;
    }
}