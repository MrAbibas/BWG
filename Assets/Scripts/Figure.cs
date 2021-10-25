using UnityEngine;
using UnityEngine.EventSystems;

public class Figure : MonoBehaviour,IPointerClickHandler
{
    public static event FigureClick figureClick;
    public delegate void FigureClick(Figure figure);
    public RectTransform rectTransform;
    public Cell curentCell;
    private void Awake() => rectTransform = GetComponent<RectTransform>();
    public void OnPointerClick(PointerEventData eventData)
    {
        if (figureClick != null)
            figureClick(this);
    }
    public void Set(Cell cell)
    {
        curentCell = cell;
        curentCell.isOcupied = true;
        rectTransform.anchoredPosition = curentCell.position;
    }
}
