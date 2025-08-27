using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    private Rigidbody2D _rb;

    private bool _canMove = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerController.onPlayerDeath += Stop;
        ResetPanel.Instance.onFadedOut += Cleanup;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerDeath -= Stop;
        ResetPanel.Instance.onFadedOut -= Cleanup;
    }
    
    public void Initialize(Vector2 endPoint, Vector2 startPoint, float duration)
    {
        StartCoroutine(MoveSequence(endPoint, startPoint, duration));
    }

    private IEnumerator MoveSequence(Vector2 endPoint, Vector2 startPoint, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            if (!_canMove) yield break;
            
            Vector2 position = startPoint + (endPoint - startPoint) * (time / duration);
            _rb.MovePosition(position);
            
            time += Time.deltaTime;
            yield return null;
        }
        
        Cleanup();
    }

    private void Stop()
    {
        _canMove = false;
    }

    private void Cleanup()
    {
        Destroy(gameObject);
    }
}
