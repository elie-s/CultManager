using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField]
        private List<PuzzleNode> puzzleNodes;
        [SerializeField]
        private PuzzlePattern[] puzzlePatterns;

        private void Start()
        {
            
        }

        private void Update()
        {
        }

        [ContextMenu("Test Check Pattern")]
        private void TestCheckPatterns()
        {
            Debug.Log(CheckPatterns(puzzleNodes, puzzlePatterns));
            
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

