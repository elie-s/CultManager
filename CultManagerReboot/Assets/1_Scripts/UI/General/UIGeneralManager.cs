using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class UIGeneralManager : MonoBehaviour
    {
        [SerializeField] private CultData cult = default;
        [SerializeField] private InfluenceData influence = default;
        [SerializeField] private PoliceData police = default;
        [SerializeField] private MoneyData money = default;
        [SerializeField] private UIGeneralDisplayer displayer = default;

        public void UpdateDisplayer()
        {
            Debug.Log(influence.value);
            displayer.UpdateDisplay(cult.cultists.Count.Format(), money.money.Format(), influence.value.Format(), money.relics.Format(), police.ratio);
        }
    }
}