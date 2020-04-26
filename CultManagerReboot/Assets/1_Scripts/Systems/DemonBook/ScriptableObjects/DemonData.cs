using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Demon/Data")]
    public class DemonData : ScriptableObject
    {
        public Demon[] demons;

        public void ToggleStar(int index)
        {
            demons[index].ToggleStar();
        }
    }
}

