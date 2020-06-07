using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TMProTextFieldObserved : TextMeshProUGUI
{
    [SerializeField] private UnityEvent onValueChanged = default;

    public void SetText(params string[] _lines)
    {
        text = "";
        foreach (string line in _lines)
        {
            text += line + '\n';
        }

        onValueChanged.Invoke();
    }
}
