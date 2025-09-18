using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SharkController : MonoBehaviour
{
    [SerializeField] private int _inputIndex = 1;
    [SerializeField] private float _jumpForce = 5f;

    [SerializeField] private int _maxLives = 3;

    [SerializeField] private SharkUI _sharkUI;
    
    [Header("Events")] 
    [SerializeField] private UnityEvent _onJump;
    [SerializeField] private UnityEvent _onImpact;
    [SerializeField] private UnityEvent _onScore;

    public int Lives { get; private set; }

    public bool IsAlive => Lives > 0;
    
    private Rigidbody2D _rb;

    public delegate void OnDeath(SharkController shark);
    public static event OnDeath onDeath;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable()
    {
        InputHandler.Instance.OnTap += OnTap;
        GameModeManager.OnStartGame += StartPlayer;
        KillBox.onSharkHit += OnHit;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnTap -= OnTap;
        GameModeManager.OnStartGame -= StartPlayer;
        KillBox.onSharkHit -= OnHit;
    }
    
    private void StartPlayer()
    {
        Lives = _maxLives;
        
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void EndPlayer()
    {
        _rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnTap(int index)
    {
        if (index != _inputIndex) return;
        
        Jump();
    }
    
    public void Jump()
    {
        Debug.Log("Jump");
        
        if (_rb.bodyType != RigidbodyType2D.Dynamic) return; 
        _rb.linearVelocityY = _jumpForce;
        _onJump?.Invoke();
    }

    private void OnHit(SharkController shark, int damage)
    {
        if (shark != this) return;

        Lives-= damage;

        if (!IsAlive) onDeath?.Invoke(this);
        
    }
    
    
}
