using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public struct Player
{
    public int id;
    public string nikname;
    public int scoreFindMode;
    public int scoreHalfMode;
}
[Serializable]
public struct PlayerArray
{
    public Player[] players;
}
public class Leaderboard : MonoBehaviour
{
    private const string url= "https://my-rock-paper-scissors.000webhostapp.com/bwg/players.php";
    public Player _curentPlayer;
    private List<Player> players;
    private LeaderboardView _leaderboardView;
    private IEnumerator Start()
    {
        _leaderboardView = FindObjectOfType<LeaderboardView>();
        _leaderboardView.Init();
        int id = PlayerPrefs.GetInt(PlayerPrefsKey.PLAYER_ID);
        string name = PlayerPrefs.GetString(PlayerPrefsKey.PLAYER_NAME);
        int bestScoreHM = PlayerPrefs.GetInt(PlayerPrefsKey.BEST_SCORE_HALF_MODE);
        int bestScoreFM = PlayerPrefs.GetInt(PlayerPrefsKey.BEST_SCORE_FIND_MODE);
        _curentPlayer = new Player() {id = id, nikname = name,scoreFindMode = bestScoreFM, scoreHalfMode = bestScoreHM };
        yield return StartCoroutine(SendGetRequest());
        if (players == null)
            yield break;
        if (players.Contains(_curentPlayer) == false)
        {
            if(players.Exists(x=> x.id == _curentPlayer.id))
            {
                yield return StartCoroutine(SendPutRequest(_curentPlayer));
            }
            else
            {
                yield return StartCoroutine(SendPostRequest(_curentPlayer));
            }
        }
        _leaderboardView.Show();
    }
    private IEnumerator SendGetRequest()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                PlayerArray playerArray = JsonUtility.FromJson<PlayerArray>(request.downloadHandler.text);
                players = playerArray.players.ToList();
            }
        }
    }
    private IEnumerator SendPostRequest(Player player)
    {
        WWWForm form = new WWWForm();
        form.AddField("nikname", player.nikname);
        form.AddField("scoreFM", player.scoreFindMode);
        form.AddField("scoreHM", player.scoreHalfMode);
        using(UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();
            int id = 0;
            if (request.responseCode == 200 && int.TryParse(request.downloadHandler.text, out id))
                PlayerPrefs.SetInt(PlayerPrefsKey.PLAYER_ID, id);
            Debug.Log("End post request with code " + request.responseCode + " id " + request.downloadHandler.text);
        }
    }
    private IEnumerator SendPutRequest(Player player)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", player.id);
        form.AddField("nikname", player.nikname);
        form.AddField("scoreFM", player.scoreFindMode);
        form.AddField("scoreHM", player.scoreHalfMode);
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].id == player.id)
                    {
                        players[i] = player;
                        break;
                    }
                }
            }
        }
    }
    public Player[] SortOrderBy(SwitcherValue value, bool isDescending)
    {
        Player[] result = new Player[1];
        if (value == SwitcherValue.FindMode)
        {
            result = (from player in players orderby player.scoreFindMode select player).ToArray();
        }
        else if(value == SwitcherValue.HalfMode)
        {
            result = (from player in players orderby player.scoreHalfMode select player).ToArray();
        }
        if (isDescending)
            result = result.Reverse().ToArray();
        return result;
    }
}
