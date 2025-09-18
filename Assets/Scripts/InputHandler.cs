using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-10000)]
public class InputHandler : MonoBehaviour
{
    [SerializeField] private Button _player1Button;
    [SerializeField] private Button _player2Button;
    
    public static InputHandler Instance { get; private set; }
    
    public delegate void OnInput(int playerIndex);
    public event OnInput OnTap;
    
    private void Awake()
    {
        if (!Instance) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    private void OnEnable()
    {
        _player1Button.onClick.AddListener(OnPlayer1Tap);
        _player2Button.onClick.AddListener(OnPlayer2Tap);
    }

    private void OnDisable()
    {
        _player1Button.onClick.RemoveListener(OnPlayer1Tap);
        _player2Button.onClick.RemoveListener(OnPlayer2Tap);
    }

    private void OnPlayer1Tap()
    {
        OnTap?.Invoke(1);
    }

    private void OnPlayer2Tap()
    {
        OnTap?.Invoke(2);
    }
    
    
}
