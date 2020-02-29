using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Cult/Settings")]
    public class CultSettings : ScriptableObject
    {
        public Sprite[] cultistThumbnails;
        public string[] cultistNames;
        [Header("Test values")]
        public int testCultistsAmount = 50;
    }
}