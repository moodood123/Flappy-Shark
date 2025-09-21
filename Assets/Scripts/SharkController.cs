using System.Collections;
using PrimeTween;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SharkController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TweenSettings<float> _timeOutSettings;
    
    [SerializeField] private int _inputIndex = 1;
    [SerializeField] private float _jumpForce = 5f;

    [SerializeField] private int _maxLives = 3;

    [SerializeField] private SharkUI _sharkUI;
    
    [Header("Events")] 
    [SerializeField] private UnityEvent _onJump;

    public int Lives { get; private set; }

    public bool IsAlive => Lives > 0;
    public bool IsTimedOut { get; private set; } = false;

    private float ScreenMax => Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
    
    private Rigidbody2D _rb;

    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable()
    {
        InputHandler.Instance.OnTap += OnTap;
        GameModeManager.OnStartGame += StartPlayer;
        GameModeManager.OnEndGame += EndPlayer;
        KillBox.onSharkHit += OnHit;
        ResetPanel.Instance.onFadedOut += Reset;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnTap -= OnTap;
        GameModeManager.OnStartGame -= StartPlayer;
        GameModeManager.OnEndGame -= EndPlayer;
        KillBox.onSharkHit -= OnHit;
        ResetPanel.Instance.onFadedOut -= Reset;
    }

    private void LateUpdate()
    {
        float newY = Mathf.Clamp(transform.position.y, -ScreenMax, ScreenMax);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void StartPlayer()
    {
        Debug.Log("StartPlayer");
        Lives = _maxLives;
        
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void EndPlayer()
    {
        Debug.Log("Ending player");
        
        _rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnTap(int index)
    {
        if (index != _inputIndex) return;
        
        Jump();
    }
    
    public void Jump()
    {
        if (_rb.bodyType != RigidbodyType2D.Dynamic) return; 
        _rb.linearVelocityY = _jumpForce;
        _onJump?.Invoke();
    }

    private void OnHit(SharkController shark, int damage)
    {
        if (shark != this) return;
        if (IsTimedOut) return;

        StartCoroutine(TimeOutSequence());
        
        Lives-= damage;
    }

    private void Reset()
    {
        transform.position = new Vector3(transform.position.x, 0f, 0f);
    }
    
    private IEnumerator TimeOutSequence()
    {
        IsTimedOut = true;
        yield return Tween.Alpha(_spriteRenderer, _timeOutSettings).ToYieldInstruction();
        IsTimedOut = false;
    }
}
