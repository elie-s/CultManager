using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    public class PuzzleSetSaver : MonoBehaviour
    {
        [SerializeField] private PuzzleData puzzleData = default;
        [SerializeField] private PuzzleDisplay displayer = default;
        [SerializeField] private DemonName demon = DemonName.Mortimer;
        [SerializeField, DrawScriptable] private PuzzleDataSet data = default;

        public void SaveCurrentPuzzle()
        {
            data.AddPuzzle(demon, puzzleData.puzzle);
        }

        public void Display()
        {
            displayer.DisplayPuzzle(1.0f, data.GetPuzzle(demon));
        }

        private void OnDrawGizmosSelected()
        {
            if (data.GetPuzzle(demon) != null)
            {
                foreach (PuzzleSegment segment in data.GetPuzzle(demon))
                {
                    Gizmos.color = segment.patternSegment ? Color.yellow : Color.blue;
                    Gizmos.DrawLine(Node.WorldPosition(segment.a, 1.0f) + (Vector2)transform.position, Node.WorldPosition(segment.b, 1.0f) + (Vector2)transform.position);
                }
            }
        }
    }
}