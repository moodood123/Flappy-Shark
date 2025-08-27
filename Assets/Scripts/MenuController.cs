using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;
    
    public delegate void OnStartGame();
    public static event OnStartGame onStartGame;

    private void OnEnable()
    {
        ResetPanel.Instance.onFadedOut += Reset;
    }

    private void OnDisable()
    {
        ResetPanel.Instance.onFadedOut -= Reset;
    }

    private void Reset()
    {
        _playButton.SetActive(true);
    }
    
    public void OnPlay()
    {
        onStartGame?.Invoke();
        _playButton.SetActive(false);
    }

}
