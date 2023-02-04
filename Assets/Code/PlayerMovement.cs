using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody Rb;
    public float Speed;

    public void OnMove(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>().normalized;
        Debug.Log(direction);

        direction *= Speed;

        Rb.velocity = new Vector3(direction.x, 0, direction.y);
    }
}
