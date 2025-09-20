using System;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    
    public delegate void OnSharkHit(SharkController shark, int damage);
    public static event OnSharkHit onSharkHit;
    
    public delegate void OnObstacleHit();
    public static event OnObstacleHit onObstacleHit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            onObstacleHit?.Invoke();
            Debug.LogWarning("Player hit");
        }

        if (other.TryGetComponent(out SharkController shark) && !shark.IsTimedOut)
        {
            onSharkHit?.Invoke(shark, _damage);
            Debug.LogWarning("Shark hit");
        }
    }
}
