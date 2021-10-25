using UnityEngine;

public class Cell
{
    public Vector2 position;
    public bool isOcupied;
    public Cell(float x, float y)
    {
        position = new Vector2(x, y);
        isOcupied = false;
    }
}
