using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    public Transform content;
    public LeaderboardPlayerView playerViewPrefab;
    public LeaderboardPlayerView curentPlayerView;
    private Leaderboard _leaderboard;
    private OrderBySwitcherGameMode _switcher;
    private List<LeaderboardPlayerView> playerViews;
    public void Init()
    {
        _leaderboard = FindObjectOfType<Leaderboard>();
        _switcher = FindObjectOfType<OrderBySwitcherGameMode>();
        _switcher.Init();
        _switcher.changed.AddListener(Refresh);
        playerViews = new List<LeaderboardPlayerView>();
    }
    public void Show()
    {
        Player[] players = _leaderboard.SortOrderBy(_switcher.curentSwitch.value, _switcher.curentSwitch.isDescending);
        Player curent = _leaderboard._curentPlayer;
        Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].id == curent.id)
                curentPlayerView.SetInfo(curent, i + 1);

            if (i < 49)
            {
                LeaderboardPlayerView temp = Instantiate(playerViewPrefab, content);
                temp.SetInfo(players[i], i + 1);
                playerViews.Add(temp);
            }
        }
    }
    private void Clear()
    {
        for (int i = 0; i < playerViews.Count; i++)
            Destroy(playerViews[i].gameObject);
        playerViews.Clear();
    }
    public void Refresh(OrderBySwitcherEntity<SwitcherValue> switcher) => Show();
}
