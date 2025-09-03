using System.Collections;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-10000)]
public class ResetPanel : MonoBehaviour
{
    [SerializeField] private Image _panel;

    [SerializeField] private TweenSettings<float> _fadeInSettings;
    [SerializeField] private TweenSettings<float> _fadeOutSettings;

    public delegate void OnFadedOut();
    public event OnFadedOut onFadedOut;
    
    public static ResetPanel Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    private void Start()
    {
        WaitForFadeIn();
    }

    public Coroutine WaitForFadeOut()
    {
        return StartCoroutine(FadeOutSequence());
    }

    public Coroutine WaitForFadeIn()
    {
        return StartCoroutine(FadeInSequence());
    }

    private IEnumerator FadeOutSequence()
    {
        _panel.raycastTarget = true;
        yield return Tween.Alpha(_panel, _fadeOutSettings).ToYieldInstruction();
        Debug.LogWarning("Faded out");
        onFadedOut?.Invoke();
    }

    private IEnumerator FadeInSequence()
    {
        yield return Tween.Alpha(_panel, _fadeInSettings).ToYieldInstruction();
        _panel.raycastTarget = false;
    }
}
