using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ColorAnimationEntity
{
    public MaskableGraphic graphic;
    public bool first;
    public bool canColorChange = true;
    public TMP_InputField parentInputField;
    public void InputFieldSubscription()
    {
        if (parentInputField != null)
        {
            parentInputField.onSelect.AddListener((string x) => canColorChange = false);
            parentInputField.onEndEdit.AddListener((string x) => canColorChange = true);
        }
    }
}
public class ColorAnimation : MonoBehaviour
{
    public ColorAnimationEntity[] entities;
    public float animationTime;
    public AnimationCurve _colorChangeFunction;
    private float _startAnimationTime;
    private void Start()
    {
        _startAnimationTime = 0;

        for (int i = 0; i < entities.Length; i++)
            entities[i].InputFieldSubscription();
    }
    private void FixedUpdate()
    {
        if (Time.time - _startAnimationTime > animationTime)
            _startAnimationTime = Time.time;
        
        Color first = Color.HSVToRGB(0, 0, _colorChangeFunction.Evaluate((Time.time - _startAnimationTime) / animationTime * 2));
        Color second = Color.HSVToRGB(0, 0, 1 - _colorChangeFunction.Evaluate((Time.time - _startAnimationTime) / animationTime * 2));
        
        for (int i = 0; i < entities.Length; i++)
            if (entities[i].canColorChange)
                entities[i].graphic.color = entities[i].first ? first : second;
    }
}
