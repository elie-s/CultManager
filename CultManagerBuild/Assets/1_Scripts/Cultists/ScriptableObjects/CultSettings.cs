using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Cult/Settings")]
    public class CultSettings : ScriptableObject
    {
        public Sprite[] cultistThumbnails;
        public string[] cultistNames;
    }
}