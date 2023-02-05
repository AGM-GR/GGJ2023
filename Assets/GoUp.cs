using UnityEngine;

public class GoUp : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.up * speed, Space.World);
    }
}
