﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class PuzzleDisplayGroup : MonoBehaviour
    {

        [SerializeField]private GameObject puzzleDisplayPrefab = default;

        public void SpawnDisplay(Demon[] demons,float scale,int startIndex)
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
            for (int i = demons.Length-1; i >=0; i--)
            {
                GameObject instance = Instantiate(puzzleDisplayPrefab, transform.position, Quaternion.identity, transform);
                PuzzleDisplay current = instance.GetComponent<PuzzleDisplay>();
                current.DisplayPuzzle(scale);
                current.HighlightShape(demons[i].segments);
                DemonDisplayAction demonDisplay = instance.GetComponent<DemonDisplayAction>();
                demonDisplay.Init(demons[i].patternSegments, demons[i].segments.Length,startIndex++);
            }
        }
    }
}
