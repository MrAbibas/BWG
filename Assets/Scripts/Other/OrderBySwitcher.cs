using UnityEngine;
using UnityEngine.Events;

public class OrderBySwitcher<T> : MonoBehaviour
{
    public UnityEvent<OrderBySwitcherEntity<T>> changed;
    [SerializeField]
    private OrderBySwitcherEntity<T>[] switches;
    public OrderBySwitcherEntity<T> curentSwitch { get; private set; }
    public void Init()
    {
        changed = new UnityEvent<OrderBySwitcherEntity<T>>();
        if (switches.Length == 0)
            return;

        for (int i = 0; i < switches.Length; i++)
            switches[i].onClick.AddListener(OnClick);

        Switch(switches[0]);
    }
    public void OnClick(OrderBySwitcherEntity<T> switcher)
    {
        if (switcher == curentSwitch)
            curentSwitch.ChangeDirection();
        else
            Switch(switcher);
        changed.Invoke(curentSwitch);
    }
    public void Switch(OrderBySwitcherEntity<T> switcher)
    {
        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i] == switcher)
                switches[i].Select();
            else
                switches[i].Diselect();
        }
        curentSwitch = switcher;
    }
}

