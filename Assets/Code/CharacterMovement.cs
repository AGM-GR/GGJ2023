using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    // Exposed variables
    public float MoveSpeed = 3f;
    public float MinInputAmount = 0.5f;
    public float SpeedDampTime = 0.2f;
    public float RotateSpeed = 6f;
    public float FloorOffsetY = 0.75f;

    // Private fields
    private float _horizontalAxis;
    private float _verticalAxis;
    private float _inputAmount;

    private Vector3 _moveDirection = Vector3.zero;

    // Component dependences
    private Camera _mainCamera;
    private Rigidbody _rb;
    private Animator _animator;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }


    public void OnMove(InputValue value)
    {
        //_moveDirection = value.Get<Vector2>().normalized;
        //Debug.Log(direction);
        //direction *= Speed;
        //Rb.velocity = new Vector3(direction.x, 0, direction.y);

        _verticalAxis = value.Get<Vector2>().y;
        _horizontalAxis = value.Get<Vector2>().x;

        Move();
        //Rotate();

        // Handle walking animation
        //animator.SetFloat("Speed", inputAmount, speedDampTime, Time.deltaTime);
        _rb.velocity = _moveDirection * MoveSpeed * _inputAmount;
    }

    private void Update()
    {
        Rotate();
    }


    private void Move()
    {
        _moveDirection = Vector3.zero; // reset movement

        //_verticalAxis = Input.GetAxis("Vertical");
        //_horizontalAxis = Input.GetAxis("Horizontal");

        // Multiplicar por los ejes de la cámara para
        // que el personaje se mueva dependiendo de la vista de la cámara
        // (esto es para evitar que si está mirando hacia la cámara, vaya en la dirección contraria al input)
        Vector3 correctedVertical = _verticalAxis * _mainCamera.transform.forward;
        Vector3 correctedHorizontal = _horizontalAxis * _mainCamera.transform.right;

        // Obtener la dirección del movimento
        Vector3 combinedInput = correctedVertical + correctedHorizontal;
        _moveDirection = new Vector3(combinedInput.normalized.x, 0, combinedInput.normalized.z);

        float inputMagnitude = Mathf.Abs(_horizontalAxis) + Mathf.Abs(_verticalAxis);
        _inputAmount = Mathf.Clamp01(inputMagnitude);
        if (_inputAmount <= MinInputAmount) _inputAmount = 0;
    }


    private void Rotate()
    {
        // Obtener la rotación en función de la dirección de movimiento
        Quaternion rot = Quaternion.LookRotation(_moveDirection);
        // Usar el input amount hace que rote más lentamente si el input es más sutil
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * _inputAmount * RotateSpeed);
        transform.rotation = targetRotation;
    }
}