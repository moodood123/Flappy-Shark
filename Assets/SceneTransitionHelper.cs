using System.Collections;
using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionHelper : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private TweenSettings<float> _fadeOutSettings;
    [SerializeField] private TweenSettings<float> _fadeInSettings;

    [SerializeField] private bool _fadeInOnLoad = true;
    
    public static SceneTransitionHelper Instance { get; private set; }

    private void Awake()
    {
        if (!Instance) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    private void Start()
    {
        if (_fadeInOnLoad)
        {
            Color color = _fadeImage.color;
            color.a = _fadeInSettings.startValue;
            _fadeImage.color = color;
            StartCoroutine(FadeSequence(_fadeInSettings));
        }
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneSequence(sceneName));
    }

    private IEnumerator LoadSceneSequence(string sceneName)
    {
        yield return StartCoroutine(FadeSequence(_fadeOutSettings));
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeSequence(TweenSettings<float> fadeSettings)
    {
        yield return Tween.Alpha(_fadeImage, fadeSettings).ToYieldInstruction();
    }
}
