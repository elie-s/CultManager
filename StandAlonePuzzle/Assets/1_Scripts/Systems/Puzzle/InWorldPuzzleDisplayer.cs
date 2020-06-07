using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class InWorldPuzzleDisplayer : MonoBehaviour
    {
        [SerializeField] private PuzzleDisplay displayer = default;
        [SerializeField] private float scale = 1.0f;
        private void OnEnable()
        {
            displayer.DisplayPuzzle(scale);
        }
    }
}