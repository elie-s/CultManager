using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuickTests : MonoBehaviour
{
    [SerializeField] private UnityEvent onKeyDownA = default;
    [SerializeField] private UnityEvent onKeyDownZ = default;
    [SerializeField] private UnityEvent onKeyDownE = default;
    [SerializeField] private UnityEvent onKeyDownR = default;
    [SerializeField] private UnityEvent onKeyDownT = default;
    [SerializeField] private UnityEvent onKeyDownY = default;
    [SerializeField] private UnityEvent onKeyDownU = default;
    [SerializeField] private UnityEvent onKeyDownI = default;
    [SerializeField] private UnityEvent onKeyDownO = default;
    [SerializeField] private UnityEvent onKeyDownP = default;
    [SerializeField] private UnityEvent onKeyDownSpace = default;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) onKeyDownA.Invoke();
        if (Input.GetKeyDown(KeyCode.Z)) onKeyDownZ.Invoke();
        if (Input.GetKeyDown(KeyCode.E)) onKeyDownE.Invoke();
        if (Input.GetKeyDown(KeyCode.R)) onKeyDownR.Invoke();
        if (Input.GetKeyDown(KeyCode.T)) onKeyDownT.Invoke();
        if (Input.GetKeyDown(KeyCode.Y)) onKeyDownY.Invoke();
        if (Input.GetKeyDown(KeyCode.U)) onKeyDownU.Invoke();
        if (Input.GetKeyDown(KeyCode.I)) onKeyDownI.Invoke();
        if (Input.GetKeyDown(KeyCode.O)) onKeyDownO.Invoke();
        if (Input.GetKeyDown(KeyCode.P)) onKeyDownP.Invoke();
        if (Input.GetKeyDown(KeyCode.Space)) onKeyDownSpace.Invoke();
    }
}
