using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class Modifier
    {
        public EffectType effect;
        public float value;
        [TextArea(3,5)]
        public string description;

        public void RandomizeEffect()
        {
            effect = (EffectType)Mathf.RoundToInt(Random.Range(-1, 1));
            value = Mathf.RoundToInt(Random.Range(1, 10)) * 5;
            value = value / 100;
        }
    }
}

