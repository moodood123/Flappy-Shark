using System;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public delegate void OnObstacleHit();
    public static event OnObstacleHit onObstacleHit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            onObstacleHit?.Invoke();
            Debug.LogWarning("Player hit");
        }
    }
}
