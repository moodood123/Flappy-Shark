using System.Collections;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private GameObject _playerUi;
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    private Rigidbody2D _rb;

    private int _points;

    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath onPlayerDeath;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PointTrigger.onPointsWon += AddPoints;
        MenuController.onStartGame += StartPlayer;
        KillBox.onObstacleHit += StopPlayer;
    }

    private void OnDisable()
    {
        PointTrigger.onPointsWon -= AddPoints;
        MenuController.onStartGame -= StartPlayer;
        KillBox.onObstacleHit -= StopPlayer;
    }

    private void StartPlayer()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _playerUi.SetActive(true);

        _scoreText.gameObject.SetActive(true);
    }

    private void StopPlayer()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        //_playerUi.SetActive(false);


        StartCoroutine(ResetPlayer());
    }

    private IEnumerator ResetPlayer()
    {
        ScoreManager.TrySetHighScore(_points);
        onPlayerDeath?.Invoke();
        yield return ResetPanel.Instance.WaitForFadeOut();

        transform.position = new Vector2(transform.position.x, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        _scoreText.gameObject.SetActive(false);

        _points = 0;
        _scoreText.text = _points.ToString();
        
        yield return ResetPanel.Instance.WaitForFadeIn();
    }

    private void AddPoints(int amount)
    {
        _points += amount;
        _scoreText.text = _points.ToString();
    }
    
    public void OnJump()
    {
        //_rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _rb.linearVelocityY = _jumpForce;
    }
}