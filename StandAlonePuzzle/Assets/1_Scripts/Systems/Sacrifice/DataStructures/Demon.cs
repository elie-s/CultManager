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
        public int id;
        public Segment[] segments;
        public bool isStarred;
        public int spriteIndex;
        public DemonName demon;

        public int patternSegments;
        public int totalPatternSegments;
        public int lootBonus;
        public DateTime deathTime;

        public string description;

        public float accuracy => (float)patternSegments / (float)totalPatternSegments <= 1.0f ?
                                (float)patternSegments / (totalPatternSegments <= segments.Length ? (float)segments.Length : (float)totalPatternSegments) :
                                (float)totalPatternSegments / (float)segments.Length;


        public Demon(int _id, Segment[] _segments,DateTime _deathTime,int _patternSegments,int _totalPatternSegments, DemonName _demon = DemonName.Demon1)
        {
            id = _id;
            segments = _segments;
            isStarred = false;
            spriteIndex = 0;
            patternSegments = _patternSegments;
            totalPatternSegments = _totalPatternSegments;
            SetRandomLoot();
            demon = _demon;
        }

        public Demon()
        {

        }

        public void SetRandomLoot()
        {
            lootBonus = Mathf.RoundToInt(UnityEngine.Random.Range(0, 100));
        }

        public void ToggleStar()
        {
            isStarred = !isStarred;
        }

        public void AddPattern(Segment[] _segments)
        {
            segments = _segments;
        }
    }
}

