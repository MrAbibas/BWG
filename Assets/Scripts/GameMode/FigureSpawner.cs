using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigureSpawner : MonoBehaviour
{
    public Figure figurePrefab;
    public Sprite[] figureSprites;
    public RectTransform exitPoint;
    private Stack<Figure> _freeFigures;
    public Grid grid;
    private Vector2 _sizeFigure;
    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        _freeFigures = new Stack<Figure>();
        _sizeFigure = grid.cellSize;
    }
    private void AddStack()
    {
        Figure temp = Instantiate(figurePrefab, grid.spawnTransform);
        temp.GetComponent<Image>().sprite = figureSprites[Random.Range(0, figureSprites.Length)];
        temp.rectTransform.sizeDelta = _sizeFigure;
        ReleaseFigure(temp);
    }
    private Figure TakeFigure()
    {
        if (_freeFigures.Count == 0)
            AddStack();
        Figure figure = _freeFigures.Pop();
        return figure;
    }
    public void ReleaseFigure(Figure figure)
    {
        if (figure.curentCell != null)
        {
            figure.curentCell.isOcupied = false;
        }
        figure.rectTransform.anchoredPosition = exitPoint.anchoredPosition;
        _freeFigures.Push(figure);
    }
    public Figure SpawnFigure()
    {
        Figure figure = TakeFigure();        
        figure.Set(grid.GetRandomCell());
        return figure;
    }
}
