using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Demon/DemonEffects")]
    public class DemonEffects : ScriptableObject
    {
        public Modifier[] SegmentModifiers;
        public Modifier[] PatternSegmentModifiers;
        public Modifier[] DemonModifiers;
    }
}

