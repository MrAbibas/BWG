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
    FigureSpawner figureSpawner;
    private bool isPlay;
    [SerializeField]
    private int accelerationPeriod;
    [Range(0, 10)][SerializeField]
    private float maxCountFigure;
    [SerializeField]
    private float _increaseMaxCountFigure;
    [Range(0, 10)][SerializeField]
    private float spawnColdown;
    [Range(0, 1)][SerializeField]
    private float spawnColdownAccelerationPercentage;
    private float lastTime;

    private void Awake()
    {
        _occupiedFigures = new List<Figure>();
        restart.onClick.AddListener(Start);
    }
    void Start()
    {
        figureSpawner = FindObjectOfType<FigureSpawner>();
        isPlay = true;
        info.BestScore = PlayerPrefs.GetInt(PlayerPrefsKey.FINDMODE_BEST_SCORE, 0);
        restart.gameObject.SetActive(false);
        for (int i = 0; i < _occupiedFigures.Count; i++)
            figureSpawner.ReleaseFigure(_occupiedFigures[i]);
        _occupiedFigures.Clear();
        info.Score = 0;
        lastTime = Time.time;
    }
    private void OnEnable() => Figure.figureClick += FigureClick;
    private void OnDisable() => Figure.figureClick -= FigureClick;
    void Update()
    {
        if (!isPlay)
            return;
        if (_occupiedFigures.Count > maxCountFigure)
        {
            Losing();
            return;
        }
        SetValue();
        if (lastTime + spawnColdown <= Time.time)
        {
            Tick();
        }
        info.TimeRemaining = lastTime + spawnColdown - Time.time;
    }
    private void Tick()
    {
        if (info.Score != 0 && info.Score % accelerationPeriod == 0)
        {
            if (maxCountFigure + 1 < figureSpawner.grid.cellsCount)
                maxCountFigure += _increaseMaxCountFigure;
            spawnColdown -= spawnColdownAccelerationPercentage * spawnColdown;
        }
        lastTime = Time.time;
        _occupiedFigures.Add(figureSpawner.SpawnFigure());
        info.TimeRemaining = spawnColdown;
    }
    private void SetValue()
    { 
        float newValue = _occupiedFigures.Count != 0 ? (_occupiedFigures.Count * spawnColdown + (Time.time - lastTime)) / ((maxCountFigure+1) * spawnColdown) : 0;
        if (newValue < slider.value)
            newValue = Mathf.Lerp(slider.value, newValue, slider.value/20f);
        slider.value = newValue;
        sliderImage.color = Color.HSVToRGB(0, 0, slider.value);
    }
    private void FigureClick(Figure figure)
    {
        if (isPlay)
        {
            figureSpawner.ReleaseFigure(figure);
            _occupiedFigures.Remove(figure);
            info.Score++;
            SetValue();
        }
    }
    private void Losing()
    {
        info.TimeRemaining = 0f;
        if (info.Score > PlayerPrefs.GetInt(PlayerPrefsKey.FINDMODE_BEST_SCORE))
            PlayerPrefs.SetInt(PlayerPrefsKey.FINDMODE_BEST_SCORE, info.Score);
        isPlay = false;
        restart.gameObject.SetActive(true);
    }
}
