using TMPro;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScoreText;
    
    private void Start()
    {
        _highScoreText.text = ScoreManager.HighScore.ToString();
    }
}
