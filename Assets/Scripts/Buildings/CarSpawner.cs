using UnityEngine;

public sealed class CarSpawner : MonoBehaviour
{
    private float _spawnSec = ProjectNames.CarSecsToSpawn;
    private float _secLeft;

    [SerializeField] private Vector3 rotation;
    [SerializeField] private GameObject car;
    private void Update()
    {
        if (_secLeft > 0)
        {
            _secLeft -= Time.deltaTime;
        }
        else
        {
            Instantiate(car, transform.position, transform.rotation);
            _secLeft = _spawnSec;
        }
    }
}
