using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Display/Sprites")]
    public class CultistsSprites : ScriptableObject
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private RuntimeAnimatorController[] animatorControllers;

        public (Sprite sprite, RuntimeAnimatorController animations) GetData(int _index)
        {
            return (sprites[_index], animatorControllers[_index]);
        }

        public Sprite GetSprite(int _index)
        {
            return sprites[_index];
        }

        public RuntimeAnimatorController GetAnimatorController(int _index)
        {
            return animatorControllers[_index];
        }
    }
}