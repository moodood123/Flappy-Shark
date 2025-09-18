using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextAnimator : MonoBehaviour
{
    [Header("Options")] 
    [SerializeField] private AnimationCurve _rotationCurve;
    [SerializeField] private AnimationCurve _positionCurve;
    
    private string _positionControlImplant = "<voffset=#em>";
    private string _rotationControlImplant = "<rotate=#>";
    
    private string _baseValue;
    
    private TextMeshProUGUI _text;
    
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _baseValue = _text.text;
    }

    private void Start()
    {
        _text.text = GetRichTextVariant(_baseValue);
    }

    private string GetRichTextVariant(string baseValue)
    {
        char[] letters = baseValue.ToCharArray();

        string newText = string.Empty;

        for (int i = 0; i < letters.Length; i++)
        {
            float ratio = (float)i / (letters.Length - 1);

            newText += GetPositionImplant(_positionCurve.Evaluate(ratio)) + GetRotationImplant(_rotationCurve.Evaluate(ratio)) +
                       letters[i];
        }

        return newText;
    }

    private string GetRotationImplant(float rotation) => _rotationControlImplant.Replace("#", rotation.ToString());
    private string GetPositionImplant(float position) => _positionControlImplant.Replace("#", position.ToString());
    
    
    
    
}
