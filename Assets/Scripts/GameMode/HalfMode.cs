using UnityEngine;
using UnityEngine.UI;

public class HalfMode : MonoBehaviour
{
    public Image imageUp;
    public Image imageDown;
    public GameObject restart;
    public Info info;
    private float _lastTime;
    private float _colorUP;
    private float _colorDown;
    private bool _isPlay;
    private float _coldownTime;
    [SerializeField]
    private float _startColdownTime;
    [SerializeField]
    private float _coldownAcceleration;

    private void Start()
    {
        _coldownTime = _startColdownTime;
        info = FindObjectOfType<Info>();
        restart.SetActive(false);
        info.Score = 0;
        _isPlay = true;
        _lastTime = Time.time;
        info.BestScore = PlayerPrefs.GetInt(PlayerPrefsKey.BEST_SCORE_HALF_MODE, 0);
        info.TimeRemaining = 0;
        info.Score = 0;
        NextTik();
    }
    private void NextTik()
    {
        _lastTime = Time.time;
        _colorUP = Random.Range(0, 85);
        _colorDown = Random.Range(_colorUP-15>0?_colorUP - 15:0, _colorUP + 15);
        if (Mathf.Abs(_colorUP - _colorDown) < 5) _colorDown += Random.Range(5,10);
        _colorUP = _colorUP / 100;
        _colorDown = _colorDown / 100;
        imageUp.color = Color.HSVToRGB(0, 0, _colorUP);
        imageDown.color = Color.HSVToRGB(0, 0, _colorDown);
    }
    public void Click(int idSide)
    {
        if (_isPlay)
        {
            if ((_colorUP < _colorDown && idSide == 0) || (_colorUP > _colorDown && idSide == 1))
            {
                info.Score++;
                _coldownTime *= _coldownAcceleration;
                NextTik();
            }
            else
            {
                Losing();
            }
        }
    }
    public void Restart()
    {
        Start();
    }
    void Update()
    {
        if (_isPlay)
        {
            if (Time.time - _lastTime < _coldownTime)
                info.TimeRemaining = _coldownTime - Time.time + _lastTime;
            else
                Losing();
        }
    }
    private void Losing()
    {
        info.TimeRemaining = 0;
        if (info.Score > PlayerPrefs.GetInt(PlayerPrefsKey.BEST_SCORE_HALF_MODE))
            PlayerPrefs.SetInt(PlayerPrefsKey.BEST_SCORE_HALF_MODE, info.Score);

        _isPlay = false;
        restart.SetActive(true);
    }
}
