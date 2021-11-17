using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public TMP_InputField inputField;
    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKey.PLAYER_NAME) == false)
            EndInputEdit("");
        else
            inputField.text = PlayerPrefs.GetString(PlayerPrefsKey.PLAYER_NAME);
        inputField.onEndEdit.AddListener(EndInputEdit);
    }
    public void EndInputEdit(string newNikname)
    {
        if (string.IsNullOrWhiteSpace(newNikname) && string.IsNullOrEmpty(newNikname))
        {
            newNikname = "Unnamed";
            inputField.text = newNikname;
        }
        PlayerPrefs.SetString(PlayerPrefsKey.PLAYER_NAME, newNikname);
    }
    public void PlayClick() => SceneManager.LoadScene("ModesMenu");
    public void LeaderboardClick() => SceneManager.LoadScene("Leaderboard");
    public void ExitClick() => Application.Quit();
}
