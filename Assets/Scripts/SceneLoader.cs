using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void LoadScene()
    {
        if (SceneTransitionHelper.Instance) SceneTransitionHelper.Instance.LoadScene(_sceneName);
        else SceneManager.LoadScene(_sceneName);
    }
}
