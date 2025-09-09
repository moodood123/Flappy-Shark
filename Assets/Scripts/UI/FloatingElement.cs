using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class FloatingElement : MonoBehaviour
{
    [SerializeField] private TweenSettings _selectSettings;
    [SerializeField] private TweenSettings _deselectSettings;

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
        Tween.UIAnchoredPositionX(_rt, _moveXSettings);
        Tween.UIAnchoredPositionX(_rt, _moveYSettings);
        Tween.LocalRotation(_rt, _rotateSettings);
    }
    
    public void OnHover()
    {
        
    }

    public void OnUnhover()
    {
        
    }
}
