using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);   
    }
    private void OnEnable()
    {
        SwipeDetector.OnSwipe += OnSwipe;
    }
    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= OnSwipe;
    }
    public void OnSwipe(SwipeData data)
    {
        if(data.Direction == SwipeDirection.Right)
        {
            ReturnToPreviosScene();
        }
    }
    public void ReturnToPreviosScene()
    {
        if (SceneManager.GetActiveScene().name == "ModesMenu")
            SceneManager.LoadScene("StartMenu");
        else if (SceneManager.GetActiveScene().name == "Leaderboard")
            SceneManager.LoadScene("StartMenu");
        else if (SceneManager.GetActiveScene().name == "StartMenu") 
            return;
        else
            SceneManager.LoadScene("ModesMenu");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            ReturnToPreviosScene();
    }
}
