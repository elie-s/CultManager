using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Cult/Settings")]
    public class CultSettings : ScriptableObject
    {
        public Sprite[] cultistThumbnails;
        public string[] cultistNames;
        public string[] cultistLastNames;
        [Header("Test values")]
        public int testCultistsAmount = 50;
    }
}