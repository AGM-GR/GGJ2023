using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Raver : RaverBase
{
    [SerializeField]
    private float _baseSpeed = 2f;
    [SerializeField]
    private float beatSpeedMultiplier = 0.2f;
    [SerializeField]
    private float rootAdditionalSpeedMultiplier = 1.5f;
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private RaverMaterials _raverMaterials;
    [SerializeField]
    private SpriteRenderer circle;
    [SerializeField]
    private Color blueColor;
    [SerializeField]
    private Color redColor;
    [SerializeField]
    private Color yellowColor;
    [SerializeField]
    private Color purpleColor;

    private Animator animator;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public bool IsInGroup => RaversGroup != null;

    private Coroutine _rootInfluenceCoroutine;

    [SerializeField] private float _rootInfluenceTimeInSeconds;
    // extra speed and invulnerability -- debería coincidir con root time?? podría funcionar

    [SerializeField] private GameObject _rootInfluenceVfx;

    private void OnEnable()
    {
        MusicController.OnMusicChanged += SetAnimatorBeat;
    }

    private void OnDisable()
    {
        MusicController.OnMusicChanged -= SetAnimatorBeat;
    }
    
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnValidate()
    {
        if (_navMeshAgent == null)
            _navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        if (_raverMaterials == null)
            _raverMaterials = GetComponentInChildren<RaverMaterials>();
    }

    public override void SetDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
    }

    public override void EnableRaver()
    {
        base.EnableRaver();

        _navMeshAgent.speed = _baseSpeed;
        gameObject.SetActive(true);
        _navMeshAgent.enabled = true;
        _raverSpawner.RaverEnabled();
        circle.enabled = false;
        SetAnimatorBeat();
        SetAnimationSpeed();
        _raverMaterials.DoDissolve(0.6f);
    }

    public override void DisableRaver()
    {
        base.DisableRaver();
        _raverSpawner.RaverDisabled();
        _navMeshAgent.enabled = false;
        gameObject.SetActive(false);
    }

    public override void InfluencedByPlayer(CarColor carColor, Car influencingCar, bool withRoot = false)
    {
        base.InfluencedByPlayer(carColor, influencingCar, withRoot);
        if (_currentState == RaverState.INFLUENCED_ROOT) return;

        SetDestination(influencingCar.PointsExit);
        SetKindOfInfluence(influencingCar, withRoot);
        InfluenceCircle(carColor);
    }

    private void SetKindOfInfluence(Car influencingCar, bool withRoot)
    {
        if (withRoot)
        {
            if (_rootInfluenceCoroutine != null) StopCoroutine(_rootInfluenceCoroutine);
            _rootInfluenceCoroutine = StartCoroutine(StartRootInfluence(influencingCar));
        }
        else
        {
            SetRegularInfluence(influencingCar);
        }
    }

    private void SetRegularInfluence(Car influencingCar)
    {
        _currentState = RaverState.INFLUENCED;
        ChangeSpeedMultiplier(influencingCar.GetSpeedMultiplierByInfluence());
    }

    private IEnumerator StartRootInfluence(Car influencingCar)
    {
        _currentState = RaverState.INFLUENCED_ROOT;
        float rootSpeedMultiplier = influencingCar.GetSpeedMultiplierByInfluence() * rootAdditionalSpeedMultiplier;
        ChangeSpeedMultiplier(rootSpeedMultiplier);
        _rootInfluenceVfx.SetActive(true);

        yield return new WaitForSeconds(_rootInfluenceTimeInSeconds);

        _rootInfluenceVfx.SetActive(false);
        SetRegularInfluence(influencingCar);
        yield return null;
    }



    public void InfluenceCircle(CarColor carColor) {
        circle.enabled = true;
        circle.color = GetColorByCarColor(carColor);
    }

    public override void ChangeSpeedMultiplier(float speedMultiplier)
    {
        _navMeshAgent.speed = _baseSpeed * speedMultiplier;
        SetAnimationSpeed();
    }

    private Color GetColorByCarColor(CarColor carColor) {
        switch (carColor) {
            case CarColor.BLUE:
                return blueColor;
            case CarColor.PURPLE:
                return purpleColor;
            case CarColor.RED:
                return redColor;
            case CarColor.YELLOW:
                return yellowColor;
            default:
                return Color.white;
        }
    }

    public void  SetAnimatorBeat() {
        animator.SetFloat("Beat", MusicController.Instance.BeatMultiplier);
    }

    private void SetAnimationSpeed() {
        //animator.SetFloat("Speed", 0.011f);
        animator.SetFloat("Speed", _navMeshAgent.speed * beatSpeedMultiplier);
    }
}
