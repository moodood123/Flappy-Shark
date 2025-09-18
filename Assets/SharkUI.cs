using System.Collections.Generic;
using PrimeTween;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SharkUI : MonoBehaviour
{
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor = Color.black;
    
    [SerializeField] private List<Image> _livesIndicators = new List<Image>();
    
    private int _livesRemaining;

    public void SetLives(int value)
    {
        if (value < _livesRemaining)
        {
            for (int i = 0; i < _livesIndicators.Count; i++)
            {
                
            }
        }
        else if (value > _livesRemaining)
        {
            
        }

        _livesRemaining = value;
    }

    private void FadeImage(Image image)
    {
        Tween.Color(image, _inactiveColor, 0.5f);
    }

    private void RestoreImage(Image image)
    {
        Tween.Color(image, _activeColor, 0.5f);
    }
}
