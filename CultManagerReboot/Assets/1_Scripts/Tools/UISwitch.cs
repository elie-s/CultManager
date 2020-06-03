using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISwitch : MonoBehaviour
{
    [SerializeField] private Sprite imageA = default;
    [SerializeField] private Sprite imageB = default;
    [SerializeField] private Image image = default;
    [SerializeField] private Color colorA = default;
    [SerializeField] private Color colorB = default;
    [SerializeField] private TextMeshProUGUI textField = default;

    public void Switch()
    {
        if(image) image.sprite = image.sprite == imageA ? imageB : imageA;
        if (textField) textField.color = textField.color == colorA ? colorB : colorA;
    }

    public void SetA()
    {
        image.sprite = imageA;
        textField.color = colorA;
    }

    public void SetB()
    {
        image.sprite = imageB;
        textField.color = colorB;
    }

    public void Set(bool _situation)
    {
        if (_situation) SetA();
        else SetB();
    }
}
