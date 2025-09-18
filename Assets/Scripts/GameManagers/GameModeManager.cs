using UnityEngine;

public abstract class GameModeManager : MonoBehaviour
{
    public delegate void OnUpdateGameStatus();
    public static event OnUpdateGameStatus OnStartGame;
    public static event OnUpdateGameStatus OnEndGame;
    
    protected virtual void OnEnable()
    {
        MenuController.onStartGame += StartGame;
    }

    protected virtual void OnDisable()
    {
        MenuController.onStartGame -= StartGame;
    }

    protected virtual void StartGame()
    {
        OnStartGame?.Invoke();
    }

    protected virtual void EndGame()
    {
        OnEndGame?.Invoke();
    }

}
