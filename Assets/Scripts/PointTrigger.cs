using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    [SerializeField] private int _pointValue = 1;
    
    public delegate void OnPointsWon(int points);
    public static event OnPointsWon onPointsWon;

    public delegate void OnSharkPointsWon(int points, SharkController shark);
    public static event OnSharkPointsWon onSharkPointsWon;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            onPointsWon?.Invoke(_pointValue);
        }

        if (collision.TryGetComponent(out SharkController shark) && !shark.IsTimedOut)
        {
            onSharkPointsWon?.Invoke(_pointValue, shark);
        }
    }
}
