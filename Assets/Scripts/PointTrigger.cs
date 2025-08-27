using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    [SerializeField] private int _pointValue = 1;
    
    public delegate void OnPointsWon(int points);
    public static event OnPointsWon onPointsWon;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            onPointsWon?.Invoke(_pointValue);
        }
    }
}
