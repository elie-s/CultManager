using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{

    [CreateAssetMenu(menuName = "CultManager/Tools/LerpCurve")]
    public class LerpCurve : ScriptableObject
    {
        public AnimationCurve lerpCurve;
        public float lerpValue;

    }
}

