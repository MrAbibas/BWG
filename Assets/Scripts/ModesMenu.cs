using UnityEngine;
using UnityEngine.SceneManagement;

public class ModesMenu : MonoBehaviour
{
    public void OpenMode(string mode)
    {
        SceneManager.LoadScene(mode);
    }
}
