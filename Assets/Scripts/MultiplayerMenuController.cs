using UnityEngine;

public class MultiplayerMenuController : MonoBehaviour
{
    public delegate void OnStartGame();
    public static OnStartGame onStartGame;

    public void OnStart()
    {
        onStartGame.Invoke();
    }
}
