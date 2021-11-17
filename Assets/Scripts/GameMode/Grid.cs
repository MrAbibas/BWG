using UnityEngine;

public class Grid: MonoBehaviour
{
    public RectTransform spawnTransform;
    public bool useCellSize;
    public Vector2 cellSize;
    public Vector2Int cellCount;
    private Cell[,] _cells;
    public int cellsCount => _cells.Length;
    private void Start()
    {
        if (useCellSize)
            _cells = GridCellsGenerator.GenerateGrid(spawnTransform.rect.size, cellSize);
        else
            _cells = GridCellsGenerator.GenerateGrid(spawnTransform.rect.size, cellCount.x, cellCount.y);
    }
    public Cell GetRandomCell()
    {
        int x, y;
        do
        {
            x = Random.Range(0, _cells.GetLength(0));
            y = Random.Range(0, _cells.GetLength(1));
        }
        while (_cells[x, y].isOcupied);
        return _cells[x, y];
    }
    public void Clear()
    {
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            for (int j = 0; j < _cells.GetLength(1); j++)
                _cells[i, j].isOcupied = false;
        }
    }
}
