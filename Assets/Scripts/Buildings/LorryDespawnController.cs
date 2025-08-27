using System;
using UnityEngine;

public sealed class LorryDespawnController : MonoBehaviour
{
    public static event Action OnLorryEntered;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        OnLorryEntered?.Invoke();
    }
}
