using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;

    [SerializeField] private Vector2 _spawnIntervalRange = new Vector2(1f, 2f);
    [SerializeField] private float _spawnPosition = 12f;
    [SerializeField] private float _endPosition = -12f;
    [SerializeField] private float _duration = 2f;

    [SerializeField] private Vector2 _heightRange;
    
    private Coroutine _spawnLoop;

    private void OnEnable()
    {
        MenuController.onStartGame += StartSpawning;
        MultiplayerMenuController.onStartGame += StartSpawning;
        PlayerController.onPlayerDeath += EndSpawning;
    }

    private void OnDisable()
    {
        MenuController.onStartGame -= StartSpawning;
        MultiplayerMenuController.onStartGame -= StartSpawning;
        PlayerController.onPlayerDeath -= EndSpawning;
    }
    
    private void StartSpawning()
    {
        _spawnLoop = StartCoroutine(SpawnLoop());
    }

    private void EndSpawning()
    {
        if (_spawnLoop != null) StopCoroutine(_spawnLoop);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(Random.Range(_spawnIntervalRange.x, _spawnIntervalRange.y));
        }
    }

    private void SpawnObstacle()
    {
        if (Instantiate(_obstaclePrefab, transform.position, Quaternion.identity)
            .TryGetComponent(out Obstacle obstacle))
        {
            float height = Random.Range(_heightRange.x, _heightRange.y);
            
            obstacle.Initialize(new Vector2(_endPosition, height), new Vector2(_spawnPosition, height), _duration);
        }
    }
}
