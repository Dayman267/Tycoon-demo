using UnityEngine;

public sealed class LorryMovement : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float speed = ProjectNames.LorrySpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rb.velocity = Vector3.right * speed;
    }
}
