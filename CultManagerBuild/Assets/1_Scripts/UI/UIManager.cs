using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Cult Database")]
    [SerializeField]
    CultistData cultistData;

    [Header("Faith Components")]
    public Image faithBar;
    float faithTotal;


    void Update()
    {
        UpdateFaith();
    }

    void UpdateFaith()
    {
        faithBar.fillAmount = cultistData.averageFaith / 100f;
    }
}
