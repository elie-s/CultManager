using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class PuzzleGenerator : MonoBehaviour
    {
        [SerializeField] private float scale = 1.0f;
        [SerializeField] private HexagonalGridPatternGenerationsSettings[] settings;
        [SerializeField] private HexagonalGridPatternGenerationsSettings patternSettings;

        private HexagonalGrid grid;
        [SerializeField] private HexagonalGridPattern pattern;

        // Start is called before the first frame update
        void Start()
        {
            grid = new HexagonalGrid(scale, 1);
            grid.SetGrid();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetGrid();
                Generate();
            }
        }

        [ContextMenu("Double")]
        public void DoubleElements()
        {
            grid.DoubleGrid();
            pattern.DoubleSize();
        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            pattern = new HexagonalGridPattern(grid, settings[0]);

            for (int i = 1; i < settings.Length; i++)
            {
                grid.DoubleGrid();
                pattern.DoubleSize();
                pattern.AddToShape(settings[i], true);
            }

            grid = new HexagonalGrid(pattern);
            pattern = new HexagonalGridPattern(grid, patternSettings);
            //Debug.Log("Segment amount: " + pattern.segments.Count);
        }

        [ContextMenu("Reset")]
        public void ResetGrid()
        {
            grid = new HexagonalGrid(scale, 1);
            grid.SetGrid();

            pattern = null;
        }

        private void OnDrawGizmosSelected()
        {
            if (grid != null)
            {
                

                foreach (HexagonalGridSegment segment in grid.segments)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(grid.NodeToWorldPosition(segment.a), 0.05f);
                    Gizmos.DrawSphere(grid.NodeToWorldPosition(segment.b), 0.05f);
                    //Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.85f);
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(grid.NodeToWorldPosition(segment.a), grid.NodeToWorldPosition(segment.b));
                }
            }

            if (pattern != null)
            {
                Gizmos.color = Color.yellow;

                foreach (HexagonalGridSegment segment in pattern.segments)
                {
                    Gizmos.DrawLine(grid.NodeToWorldPosition(segment.a), grid.NodeToWorldPosition(segment.b));
                }
            }
        }

    }
}