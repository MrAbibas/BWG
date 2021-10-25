using UnityEngine;

public static class GridCellsGenerator
{
    public static Cell[,] GenerateGrid(Vector2 areaSize, int rowCount, int columnCount)
    {
        Vector2 cellSize = new Vector2();
        cellSize.x = areaSize.x / columnCount;
        cellSize.y = areaSize.y / rowCount;
        return GenerateGrid(areaSize, cellSize, new Vector2Int(columnCount,rowCount));
    }
    public static Cell[,] GenerateGrid(Vector2 areaSize, Vector2 cellSize)
    {
        Vector2Int cellCount = new Vector2Int();
        cellCount.x = (int)(areaSize.x / cellSize.x);
        cellCount.y = (int)(areaSize.y / cellSize.y);
        return GenerateGrid(areaSize, cellSize, cellCount);
    }
    private static Cell[,] GenerateGrid(Vector2 areaSize, Vector2 cellSize, Vector2Int cellCount)
    {
        Vector2 offset = new Vector2();
        offset.x = (areaSize.x - cellCount.x * cellSize.x) / 2;
        offset.y = (areaSize.y - cellCount.y * cellSize.y) / 2;
        Cell[,] cells = new Cell[cellCount.x, cellCount.y];
        for (int i = 0; i < cellCount.x; i++)
        {
            for (int y = 0; y < cellCount.y; y++)
            {
                float posX = offset.x + cellSize.x / 2 + i * cellSize.x;
                float posY = offset.y + cellSize.y / 2 + y * cellSize.y;
                cells[i, y] = new Cell(posX, posY);
            }
        }
        return cells;
    }
}
