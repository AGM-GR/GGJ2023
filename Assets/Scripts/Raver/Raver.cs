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

    public AudioSource aSource;
    public List<AudioClip> clips;


    public NavMeshAgent NavMeshAgent => _navMeshAgent;

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

    public override void InfluencedByPlayer(CarColor carColor, Car influencingCar)
    {
        if (influencingCar != _currentInfluencingCar) {
            aSource.PlayOneShot(clips.GetRandomElement());
        }

        base.InfluencedByPlayer(carColor, influencingCar);
        SetDestination(influencingCar.PointsExit);
        ChangeSpeedMultiplier(influencingCar.GetSpeedMultiplierByInfluence());
        InfluenceCircle(carColor);

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
        animator.SetFloat("Beat", MusicController.beatMultiplier);
    }

    private void SetAnimationSpeed() {
        //animator.SetFloat("Speed", 0.011f);
        animator.SetFloat("Speed", _navMeshAgent.speed * beatSpeedMultiplier);
    }
}
