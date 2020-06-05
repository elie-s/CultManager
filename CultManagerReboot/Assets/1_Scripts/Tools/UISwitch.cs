using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISwitch : MonoBehaviour
{
    [SerializeField] private Sprite imageA = default;
    [SerializeField] private Sprite imageB = default;
    [SerializeField] private Sprite imageC = default;
    [SerializeField] private Image image = default;
    [SerializeField] private Color colorA = Color.white;
    [SerializeField] private Color colorB = Color.white;
    [SerializeField] private Color colorC = Color.white;
    [SerializeField] private TextMeshProUGUI textField = default;

    public void SetA()
    {
        if (image && imageA) image.sprite = imageA;
        if (textField) textField.color = colorA;
    }

    public void SetB()
    {
        if (image && imageB) image.sprite = imageB;
        if (textField) textField.color = colorB;
    }

    public void SetC()
    {
        if (image && imageC) image.sprite = imageC;
        if (textField) textField.color = colorC;
    }

    public void Set(bool _situation)
    {
        if (_situation) SetA();
        else SetB();
    }
}
