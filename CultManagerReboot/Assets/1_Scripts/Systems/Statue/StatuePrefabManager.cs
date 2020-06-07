using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class StatuePrefabManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] partRenderers = default;

        private StatuePart[] parts = default;

        public void Init(StatuePart[] _parts)
        {
            parts = _parts;
        }

        public void UpdatePartSprite(int _index)
        {
            partRenderers[_index].sprite = parts[_index].completion.isFull ? parts[_index].completedSprite : parts[_index].uncompletedSprite;
        }

        public void UpdateAllPartsSprites()
        {
            for (int i = 0; i < partRenderers.Length; i++)
            {
                UpdatePartSprite(i);
            }
        }
    }
}