using TMPro;
using UnityEngine;

public class TargetScoreSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _targetScoreText;
    
    private int _targetScore = 10;
    
    public delegate void OnTargetScoreChanged(int newScore);
    public static event OnTargetScoreChanged onTargetScoreChanged;

    public void OnLowerTargetScore()
    {
        _targetScore--;
        onTargetScoreChanged?.Invoke(_targetScore);
        _targetScoreText.text = _targetScore.ToString();
    }

    public void OnRaiseTargetScore()
    {
        _targetScore++;
        onTargetScoreChanged?.Invoke(_targetScore);
        _targetScoreText.text = _targetScore.ToString();
    }
}
