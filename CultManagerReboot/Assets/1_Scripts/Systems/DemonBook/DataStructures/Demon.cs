using CultManager.HexagonalGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class Demon
    {
        public Segment[] segments;
        public bool isStarred;
        public int spriteIndex;

        public float patternAccuracy;
        public int lootBonus;
        public DateTime spawnTime;

        public Demon(Segment[] _segments,int _lootBonus)
        {
            segments = _segments;
            isStarred = false;
            spriteIndex = 0;

            lootBonus = _lootBonus;
        }


        public void SetRandomLoot()
        {
            lootBonus = Mathf.RoundToInt(UnityEngine.Random.Range(0, 100));
        }

        public void ToggleStar()
        {
            isStarred = !isStarred;
        }

        public void SetSpawnTime()
        {
            spawnTime = System.DateTime.Now;
        }

        public void ComputePatternAccuracy(Segment[] patternSegments)
        {
            int ctr = 0;
            for (int i = 0; i < patternSegments.Length; i++)
            {
                for (int j = 0; j < segments.Length; j++)
                {
                    if (patternSegments[i].Equals(segments[j]))
                    {
                        ctr++;
                    }
                }
            }
            patternAccuracy = (float)ctr / patternSegments.Length;
        }

        public void AddPattern(Segment[] _segments)
        {
            segments = _segments;
        }
    }
}

