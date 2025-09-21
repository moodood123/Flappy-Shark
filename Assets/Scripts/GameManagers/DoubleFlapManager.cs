using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DoubleFlapManager : GameModeManager
{
    [Header("Player References")]
    [SerializeField] private SharkController _player1;
    [SerializeField] private SharkController _player2;
    
    [Header("Settings")]
    [SerializeField] private int _targetScore = 10;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreTextPlayer1;
    [SerializeField] private TextMeshProUGUI _scoreTextPlayer2;
    [SerializeField] private RectTransform _scorePanelPlayer1;
    [SerializeField] private RectTransform _scorePanelPlayer2;
    [SerializeField] private RectTransform _screenCenter;
    [SerializeField] private RectTransform _menuButton;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _gameUI;
    
    [Header("In-Game Animation Settings")]
    [SerializeField] private ShakeSettings _gainPointsSettings;
    [SerializeField] private ShakeSettings _losePointsSettings;

    [Header("Endgame Animation Settings")] 
    [SerializeField] private TweenSettings<float> _scaleUpWinnerSettings;
    [SerializeField] private TweenSettings<float> _hideElementSettings;
    [SerializeField] private TweenSettings<float> _showElementSettings;
    [SerializeField] private TweenSettings _moveWinnerSettings;

    [Header("Events")] 
    [SerializeField] private UnityEvent _onGameStart;
    [SerializeField] private UnityEvent _onGameEnd;
    [SerializeField] private UnityEvent _onPointGained;
    [SerializeField] private UnityEvent _onPointsLost;
    
    private int _player1Score;
    private int _player2Score;

    private Vector3 _panel1OriginalPosition;
    private Vector3 _panel2OriginalPosition;

    private bool _isGameOver = false;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        KillBox.onSharkHit += OnSharkHitObstacle;
        PointTrigger.onSharkPointsWon += OnPointsReceived;
        MultiplayerMenuController.onStartGame += StartGame;
        ResetPanel.Instance.onFadedOut += ResetGame;
        TargetScoreSetter.onTargetScoreChanged += SetTargetScore;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        KillBox.onSharkHit -= OnSharkHitObstacle;
        PointTrigger.onSharkPointsWon -= OnPointsReceived;
        MultiplayerMenuController.onStartGame -= StartGame;
        ResetPanel.Instance.onFadedOut -= ResetGame;
        TargetScoreSetter.onTargetScoreChanged -= SetTargetScore;
    }

    protected override void StartGame()
    {
        base.StartGame();
        _isGameOver = false;
        
        _player1Score = 0;
        _player2Score = 0;
        _scoreTextPlayer1.text = _player1Score.ToString();
        _scoreTextPlayer2.text = _player2Score.ToString();

        _panel1OriginalPosition = _scorePanelPlayer1.anchoredPosition;
        _panel2OriginalPosition = _scorePanelPlayer2.anchoredPosition;

        _menuButton.gameObject.SetActive(true);
    }

    protected override void EndGame()
    {
        base.EndGame();
    }

    private void ResetGame()
    {
        // TODO: Add reset logic
        _menuUI.SetActive(true);
        _gameUI.SetActive(false);
        _scorePanelPlayer1.anchoredPosition = _panel1OriginalPosition;
        _scorePanelPlayer2.anchoredPosition = _panel2OriginalPosition;
        _scorePanelPlayer1.localScale = new Vector3(1f, 1f, 1f);
        _scorePanelPlayer2.localScale = new Vector3(1f, 1f, 1f);

        _menuButton.transform.localScale = new Vector3(1f, 1f, 1f);
        
        ResetPanel.Instance.WaitForFadeIn();
    }

    public void EndEarly()
    {
        EndGame();
        
        _isGameOver = true;
        
        if (ResetPanel.Instance) ResetPanel.Instance.WaitForFadeOut();
    }

    private void EndGame(SharkController winner)
    {
        EndGame();
        
        _isGameOver = true;
        
        Debug.Log("EndGame");
        RectTransform winningPanel = winner == _player1 ? _scorePanelPlayer1 : _scorePanelPlayer2;
        RectTransform losingPanel = winner == _player1 ? _scorePanelPlayer2 : _scorePanelPlayer1;

        Tween.Scale(winningPanel, _scaleUpWinnerSettings);
        Tween.Position(winningPanel, _screenCenter.position, _moveWinnerSettings);
        Tween.Scale(losingPanel, _hideElementSettings);

        Tween.Scale(_menuButton, _showElementSettings);

        if (ResetPanel.Instance) ResetPanel.Instance.WaitForFadeOut();
    }

    private void SetTargetScore(int newTargetScore)
    {
        _targetScore = newTargetScore;
    }

    private void OnSharkHitObstacle(SharkController shark, int damage)
    {
        _onPointsLost.Invoke();
        
        if (_isGameOver) return;
        
        if (shark == _player1)
        {
            _player1Score = 0;
            _scoreTextPlayer1.text = _player1Score.ToString();
            Tween.ShakeScale(_scoreTextPlayer1.transform, _losePointsSettings);
        }

        if (shark == _player2)
        {
            _player2Score = 0;
            _scoreTextPlayer2.text = _player2Score.ToString();
            Tween.ShakeScale(_scoreTextPlayer2.transform, _losePointsSettings);
        }
    }

    private void OnPointsReceived(int points, SharkController shark)
    {
        _onPointGained.Invoke();
        
        if (_isGameOver) return;
        
        if (shark == _player1)
        {
            _player1Score += points;
            _scoreTextPlayer1.text = _player1Score.ToString();
            
            if (_player1Score >= _targetScore)
            {
                EndGame(shark);
                return;
            }
            
            Tween.ShakeScale(_scoreTextPlayer1.transform, _gainPointsSettings);
        }

        if (shark == _player2)
        {
            _player2Score += points;
            _scoreTextPlayer2.text = _player2Score.ToString();
            
            if (_player2Score >= _targetScore)
            {
                EndGame(shark);
            }

            Tween.ShakeScale(_scoreTextPlayer2.transform, _gainPointsSettings);
        }
    }
}
