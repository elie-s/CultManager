using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CultManager
{
    public class SpawnColor : MonoBehaviour
    {
        [SerializeField] private Image body;
        [SerializeField] private Image alpha;
        [SerializeField] private Gradient accuracyColor = default;

        public void ColorIt(float accuracy)
        {
            alpha.color = accuracyColor.Evaluate(accuracy);
        }
    }
}
