using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class Backdrop : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Transform _tileGroup;
    [SerializeField] private float _tileCount;
    [SerializeField] private GameObject _tileExample;

    private List<GameObject> _tiles = new List<GameObject>();
    private List<Vector3> _positions = new List<Vector3>();
    
    private Coroutine _coroutine;
    
    private float TileWidth => _tileExample.transform.localScale.x;
    private float TotalWidth => TileWidth * _tileCount;
    
    private void Awake()
    {
        for (int i = 0; i < _tileCount; i++)
        {
            Vector3 spawnPosition = new Vector3((0f - TotalWidth / 2f) + i * (TileWidth), 0, 0);

            GameObject tile = Instantiate(_tileExample, spawnPosition, Quaternion.identity,
                _tileGroup);
            tile.SetActive(true);
            _tiles.Add(tile);
            _positions.Add(tile.transform.localPosition);
        }
    }

    private void OnEnable()
    {
        PlayerController.onPlayerDeath += StopSweep;
        MenuController.onStartGame += StartSweep;
        if (ResetPanel.Instance) ResetPanel.Instance.onFadedOut += Reset;
        else Debug.LogError("No reset panel found");
    }

    private void OnDisable()
    {
        PlayerController.onPlayerDeath -= StopSweep;
        MenuController.onStartGame -= StartSweep;
        if (ResetPanel.Instance) ResetPanel.Instance.onFadedOut -= Reset;
    }

    private void StartSweep()
    {
        _coroutine = StartCoroutine(SweepLoop());
    }

    private void StopSweep()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = null;
        Tween.StopAll(_tileGroup);
    }

    private void Reset()
    {
        Debug.Log("Reset");
        _tileGroup.transform.position = Vector3.zero;
        for (int i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].transform.localPosition = _positions[i];
        }
    }

    private IEnumerator SweepLoop()
    {
        int moveableIndex = 0;
        
        while (true)
        {
            yield return Tween.LocalPositionX(_tileGroup, _tileGroup.position.x - TileWidth, _duration, Ease.Linear)
                .ToYieldInstruction();
            _tiles[moveableIndex].transform.position += new Vector3(TotalWidth, 0, 0);
            
            moveableIndex++;
            if (moveableIndex >= _tiles.Count) moveableIndex = 0;
        }
    }

    
}
