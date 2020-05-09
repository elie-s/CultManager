using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Demon/DemonEffects")]
    public class DemonEffects : ScriptableObject
    {
        public Modifier[] SpawnModifiers;
        public Modifier[] DemonModifiers;
    }
}

