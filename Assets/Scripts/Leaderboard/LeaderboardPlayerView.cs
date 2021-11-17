using TMPro;
using UnityEngine;

public class LeaderboardPlayerView : MonoBehaviour
{
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI niknameText;
    public TextMeshProUGUI halfModeScoreText;
    public TextMeshProUGUI findModeScoreText;

    public void SetInfo(Player player, int position)
    {
        positionText.text = position.ToString();
        niknameText.text = player.nikname;
        halfModeScoreText.text = player.scoreHalfMode.ToString();
        findModeScoreText.text = player.scoreFindMode.ToString();
    }
}
