using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Blood/ColorSet")]
    public class BloodColor : ScriptableObject
    {
        [SerializeField] private Color typeA = default;
        [SerializeField] private Color typeB = default;
        [SerializeField] private Color typeO = default;

        public Color GetColor(BloodType _blood)
        {
            switch (_blood)
            {

                case BloodType.O:
                    return typeO;

                case BloodType.A:
                    return typeA;

                case BloodType.B:
                    return typeB;

                case BloodType.AB:
                    break;
            }

            return Color.white;
        }
    }
}