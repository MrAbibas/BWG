using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindMode : MonoBehaviour
{
    public Info info;
    public Button restart;
    public Slider slider;
    public Image sliderImage;
    private List<Figure> _occupiedFigures;
    private FigureSpawner _figureSpawner;
    private bool isPlay;
    [SerializeField]
    private int accelerationPeriod;
    [Range(0, 10)]
    [SerializeField]
    private float startMaxCountFigure;
    private float _maxCountFigure;
    [SerializeField]
    private float _increaseMaxCountFigure;
    private float _spawnColdown;
    [Range(0, 10)]
    [SerializeField]
    private float startSpawnColdown;
    [Range(0, 1)][SerializeField]
    private float spawnColdownAccelerationPercentage;
    private float _lastTime;

    private void OnEnable() => Figure.figureClick += FigureClick;
    private void OnDisable() => Figure.figureClick -= FigureClick;
    private void Awake()
    {
        _occupiedFigures = new List<Figure>();
        restart.onClick.AddListener(Start);
    }
    void Start()
    {
        _figureSpawner = FindObjectOfType<FigureSpawner>();
        restart.gameObject.SetActive(false);
        isPlay = true;
        info.Score = 0;
        _lastTime = Time.time;
        _spawnColdown = startSpawnColdown;
        _maxCountFigure = startMaxCountFigure;
        info.BestScore = PlayerPrefs.GetInt(PlayerPrefsKey.BEST_SCORE_FIND_MODE, 0);
        for (int i = 0; i < _occupiedFigures.Count; i++)
            _figureSpawner.ReleaseFigure(_occupiedFigures[i]);
        _occupiedFigures.Clear();
    }
    void Update()
    {
        if (!isPlay)
            return;

        SetValue();
        if (_occupiedFigures.Count > _maxCountFigure)
        {
            Losing();
            return;
        }
        if (_lastTime + _spawnColdown <= Time.time)
        {
            Tick();
        }
        info.TimeRemaining = _lastTime + _spawnColdown - Time.time;
    }
    private void Tick()
    {
        if (info.Score != 0 && info.Score % accelerationPeriod == 0)
        {
            if (_maxCountFigure + 1 < _figureSpawner.grid.cellsCount)
                _maxCountFigure += _increaseMaxCountFigure;
            _spawnColdown -= spawnColdownAccelerationPercentage * _spawnColdown;
        }
        _lastTime = Time.time;
        _occupiedFigures.Add(_figureSpawner.SpawnFigure());
        info.TimeRemaining = _spawnColdown;
    }
    private void SetValue()
    {
        float newValue = _occupiedFigures.Count != 0 ? (_occupiedFigures.Count * _spawnColdown + (Time.time - _lastTime)) / (((int)_maxCountFigure+1) * _spawnColdown) : 0;
        if (newValue < slider.value)
            newValue = Mathf.Lerp(slider.value, newValue, slider.value/20f);
        slider.value = newValue;
        sliderImage.color = Color.HSVToRGB(0, 0, slider.value);
    }
    private void FigureClick(Figure figure)
    {
        if (isPlay == false)
            return;
        _figureSpawner.ReleaseFigure(figure);
        _occupiedFigures.Remove(figure);
        info.Score++;
        SetValue();
    }
    private void Losing()
    {
        SetValue();
        info.TimeRemaining = 0f;
        if (info.Score > PlayerPrefs.GetInt(PlayerPrefsKey.BEST_SCORE_FIND_MODE))
            PlayerPrefs.SetInt(PlayerPrefsKey.BEST_SCORE_FIND_MODE, info.Score);
        isPlay = false;
        restart.gameObject.SetActive(true);
    }
}
