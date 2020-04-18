using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class PuzzleGenerator : MonoBehaviour
    {
        [SerializeField] private float scale = 1.0f;
        [SerializeField] private HexagonalGridPatternGenerationsSettings[] settings;

        private HexagonalGrid grid;
        private HexagonalGridPattern pattern;

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

        [ContextMenu("Generate")]
        public void Generate()
        {
            pattern = new HexagonalGridPattern(grid, settings[0]);

            for (int i = 1; i < settings.Length; i++)
            {
                grid.DoubleGrid();
                pattern.DoubleSize();
                pattern.AddToShape(settings[i]);
            }
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
                Gizmos.color = Color.blue;

                foreach (Vector2Int node in grid.nodes)
                {
                    Gizmos.DrawSphere(grid.NodeToWorldPosition(node), 0.05f);
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