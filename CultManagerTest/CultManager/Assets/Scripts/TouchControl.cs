using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchControl : MonoBehaviour
{
    public Controls controlMap;
    void Awake()
    {
        controlMap = new Controls();
        controlMap.God.Touch0.performed += ctx => TouchFunc();
    }

    void TouchFunc()
    {
        Debug.Log("Touch");
    }

    private void OnEnable()
    {
        controlMap.Enable();
    }
    private void OnDisable()
    {
        controlMap.Disable();
    }
}
