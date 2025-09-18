using PrimeTween;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private RectTransform _menuUi;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("Animation Settings")] 
    [SerializeField] private TweenSettings _viewMenuSettings;
    [SerializeField] private TweenSettings _viewCreditsSettings;
    
    private bool _isViewingCredits;
    
    public delegate void OnStartGame();
    public static event OnStartGame onStartGame;

    private void Awake()
    {
        _scoreText.text = ScoreManager.HighScore.ToString();
    }
    
    private void OnEnable()
    {
        ResetPanel.Instance.onFadedOut += Reset;
    }

    private void OnDisable()
    {
        ResetPanel.Instance.onFadedOut -= Reset;
    }

    public void ViewCredits()
    {
        _isViewingCredits = true;
        Debug.Log(-Screen.height);
        Tween.UIAnchoredPositionY(_menuUi, 0f, Screen.height / 2f, _viewCreditsSettings);
    }

    public void ViewGamePanel()
    {
        _isViewingCredits = false;
        Debug.Log(-Screen.height);
        Tween.UIAnchoredPositionY(_menuUi, Screen.height / 2f, 0f, _viewMenuSettings);
    }

    private void Reset()
    {
        Debug.Log("ResetMenu");
        _scoreText.text = ScoreManager.HighScore.ToString();
        _menuUi.gameObject.SetActive(true);
    }
    
    public void OnPlay()
    {
        if (_isViewingCredits) return;
        onStartGame?.Invoke();
        _menuUi.gameObject.SetActive(false);
    }

}
