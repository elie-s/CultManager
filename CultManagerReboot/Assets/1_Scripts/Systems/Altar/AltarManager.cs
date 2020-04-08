using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class AltarManager : MonoBehaviour
    {
        [Header("Altar Data")]
        [SerializeField] private AltarData altarData = default;
        [SerializeField] private AltarPartBehavior[] altarPartBehaviors;

        [Header("Cult Data")]
        [SerializeField] private CultData data = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private MoneyManager money = default;

        
        

    }
}

