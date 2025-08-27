using UnityEngine;

public sealed class LorrySpawner : MonoBehaviour
{
    private float _spawnSec = ProjectNames.LorrySecsToSpawn;
    private float _secLeft;

    [SerializeField] private Vector3 rotation;
    [SerializeField] private GameObject lorry;
    private void Update()
    {
        if (_secLeft > 0)
        {
            _secLeft -= Time.deltaTime;
        }
        else
        {
            Instantiate(lorry, transform.position, Quaternion.Euler(rotation));
            _secLeft = _spawnSec;
        }
    }
}
