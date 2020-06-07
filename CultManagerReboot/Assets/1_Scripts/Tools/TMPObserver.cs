using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TMPObserver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI observedComponent = default;
    [SerializeField] private UnityEvent onValueChanged = default;

    private string lastValue = "";

    void Update()
    {
        CheckValue();
    }

    private void CheckValue()
    {
        if(observedComponent.text != lastValue)
        {
            lastValue = observedComponent.text;
            onValueChanged.Invoke();
        }
    }
}
