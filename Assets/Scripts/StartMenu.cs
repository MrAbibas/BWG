using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Image ExitButton;
    public Image PlayButton;
    public Image Background;
    public float animationTime;
    [SerializeField]
    private AnimationCurve _colorChangeFunction;
    private float _startAnimationTime;

    private void Start()
    { 
        _startAnimationTime = 0;
    }
    private void FixedUpdate()
    {
        if(Time.time - _startAnimationTime > animationTime)
        {
            _startAnimationTime = Time.time;
        }
        ExitButton.color = Color.HSVToRGB(0, 0,_colorChangeFunction.Evaluate((Time.time - _startAnimationTime) / animationTime * 2));
        PlayButton.color = Color.HSVToRGB(0, 0, _colorChangeFunction.Evaluate((Time.time - _startAnimationTime) / animationTime * 2));
        Background.color = Color.HSVToRGB(0, 0,1- _colorChangeFunction.Evaluate((Time.time - _startAnimationTime) / animationTime* 2));
    }

    public void PlayClick()
    {
        SceneManager.LoadScene("ModesMenu");
    }
    public void ExitClick()
    {
        Application.Quit();
    }
}
