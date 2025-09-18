using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private List<RawImage> _images = new List<RawImage>();
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private Rect _uvRect;

    private bool _isScrolling = true;
    private float _offset;
    
    private void Update()
    {
        if (_isScrolling) Scroll();
    }

    private void Scroll()
    {
        _offset += Time.deltaTime * _scrollSpeed;
        
        foreach (RawImage image in _images)
        {
            _uvRect = image.uvRect;
            _uvRect.x = _offset;
            image.uvRect = _uvRect;
        }
    }
}
