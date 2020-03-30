﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField]
        SegmentManager segmentManager;
        /*public List<PuzzleNode> puzzleNodes;
        [SerializeField]
        private PuzzlePattern[] puzzlePatterns;*/
        public Token[] cultistTokens;
        public bool summonActivation;


        private void Start()
        {
            
        }

        private void Update()
        {
        }

        public void SegmentActivation()
        {
            segmentManager.Summon();
        }

        [ContextMenu("Test Check Pattern")]
        public void TestCheckPatterns()
        {
            //Debug.Log(CheckPatterns(puzzleNodes, puzzlePatterns));
        }

        int CheckPatterns(List<PuzzleNode> _puzzleNodes, PuzzlePattern[] _puzzlePatterns)
        {
            for (int i = 0; i < _puzzlePatterns.Length; i++)
            {
                if (_puzzlePatterns[i].CompareNodeList(_puzzleNodes))
                {
                    return i;
                }                
            }
            return -1;
        }

    }
}

