using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField]
    private Text _textBestScore;
    [SerializeField]
    private Text _textTimeRemaining;
    [SerializeField]
    private Text _textScore;

    private int _bestScore;
    public int BestScore
    {
        get => _bestScore;
        set
        {
            _bestScore = value;
            _textBestScore.text = value.ToString();
        }
    }

    private float _timeRemaining;
    public float TimeRemaining
    {
        get => _timeRemaining;
        set
        {
            _timeRemaining = value;
            _textTimeRemaining.text = $"{_timeRemaining:f2}";
        }
    }

    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _textScore.text = value.ToString();
        }
    }
}
