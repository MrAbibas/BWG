using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderBySwitcherEntity<T>: MonoBehaviour, IPointerClickHandler
{
    public UnityEvent<OrderBySwitcherEntity<T>> onClick;
    public Image _arrowImage;
    public T value;
    private bool _isDescending;
    public bool isDescending
    {
        get => _isDescending;
        private set
        {
            _isDescending = value;
            _arrowImage.transform.localScale = _isDescending ? new Vector3(1,-1,1): Vector3.one;
        }
    }
    private void Awake()
    {
        onClick = new UnityEvent<OrderBySwitcherEntity<T>>();
        _arrowImage.enabled = false;
        isDescending = true;
    }
    public void Select() => ChangeSelectState(true);
    public void Diselect() => ChangeSelectState(false);
    private void ChangeSelectState(bool state)
    {
        isDescending = state;
        _arrowImage.enabled = state;
    }
    public void ChangeDirection() => isDescending = !isDescending;
    public void OnPointerClick(PointerEventData eventData)=> onClick.Invoke(this);
}
