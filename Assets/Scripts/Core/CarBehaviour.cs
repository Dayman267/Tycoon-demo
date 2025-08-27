using System;
using System.Collections;
using UnityEngine;

public sealed class CarBehaviour : MonoBehaviour
{
    private Rigidbody _rb;
    private float _speed = ProjectNames.CarSpeed;
    private float _detectionDistance = ProjectNames.CarDetectionDistance;
    private float _waitTime = ProjectNames.CarWaitValue;
    
    [Header("Gizmos")]
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private float gizmoSphereRadius = 0.05f;
    
    private bool _isWaiting;
    private bool _isRefueled;
    
    public static event Action OnRefueled;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isWaiting)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        if (TryGetObstacleAhead(out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("GasStation") && !_isRefueled)
                {
                    StartCoroutine(StopAndRefuel(hit.collider));
                    return;
                }
                else if (hit.collider.CompareTag("Car"))
                {
                    StartCoroutine(WaitForOtherCarToLeave(hit.collider));
                    return;
                }
            }
            //return;
        }
        
        _rb.velocity = transform.forward * _speed;
    }
    
    private bool TryGetObstacleAhead(out RaycastHit hit)
    {
        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 dir = transform.forward;
        return Physics.Raycast(origin, dir, out hit, _detectionDistance);
    }
    
    private IEnumerator StopAndRefuel(Collider station)
    {
        if (_isWaiting) yield break;
        _isWaiting = true;

        _rb.velocity = Vector3.zero;
        
        yield return new WaitForSeconds(_waitTime);

        _isRefueled = true;
        OnRefueled?.Invoke();

        _isWaiting = false;
    }

    private IEnumerator WaitForOtherCarToLeave(Collider otherCar)
    {
        if (_isWaiting) yield break;
        _isWaiting = true;

        _rb.velocity = Vector3.zero;

        while (otherCar != null)
        {
            if (Physics.Raycast(transform.position + Vector3.up * 1f, transform.forward, out RaycastHit hit, _detectionDistance))
            {
                if (hit.collider == otherCar)
                {
                    yield return null;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        _isWaiting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 dir = transform.forward;
        Vector3 end = origin + dir * _detectionDistance;

        Gizmos.DrawLine(origin, end);
        Gizmos.DrawSphere(end, gizmoSphereRadius);
    }
}
