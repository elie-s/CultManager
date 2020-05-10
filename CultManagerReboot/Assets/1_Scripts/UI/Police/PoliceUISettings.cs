using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/UI/Police/UISettings")]
    public class PoliceUISettings : ScriptableObject
    {
        [Header("Sprite Reference")]
        public Sprite inflitrationEnabled;
        public Sprite inflitrationDisabled;
        public Sprite moneyLossEnabled;
        public Sprite moneyLossDisabled;
        public Sprite influenceEnabled;
        public Sprite influenceDisabled;
        public Sprite cultistArrestEnabled;
        public Sprite cultistArrestDisabled;
        public Sprite giveButtonEnabled;
        public Sprite giveButtonDisabled;
    }

}
