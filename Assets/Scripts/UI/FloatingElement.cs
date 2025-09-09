using System.Collections;
using PrimeTween;
using UnityEngine;

public class FloatingElement : MonoBehaviour
{
    [SerializeField] private TweenSettings<float> _moveXSettings;
    [SerializeField] private TweenSettings<float> _moveYSettings;
    [SerializeField] private TweenSettings<Quaternion> _rotateSettings;
    
    private RectTransform _rt;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        StartCoroutine(PositionXLoop());
        StartCoroutine(PositionYLoop());
        StartCoroutine(RotationLoop());
    }

    private IEnumerator PositionXLoop()
    {
        while (true)
        {
            yield return Tween.UIAnchoredPositionX(_rt, _moveXSettings).ToYieldInstruction();
        }
    }
    
    private IEnumerator PositionYLoop()
    {
        while (true)
        {
            yield return Tween.UIAnchoredPositionY(_rt, _moveYSettings).ToYieldInstruction();
        }
    }

    private IEnumerator RotationLoop()
    {
        while (true)
        {
            yield return Tween.LocalRotation(_rt, _rotateSettings).ToYieldInstruction();
        }
    }
}
