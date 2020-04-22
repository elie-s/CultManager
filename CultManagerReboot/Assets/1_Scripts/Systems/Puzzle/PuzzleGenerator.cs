using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;
using HexGrid = CultManager.HexagonalGrid.HexGrid;

namespace CultManager
{
    public class PuzzleGenerator : MonoBehaviour
    {
        [SerializeField] private float scale = 1.0f;
        [SerializeField] private PatternGenerationSettings[] settings;
        [SerializeField] private PatternGenerationSettings patternSettings;
        [SerializeField, DrawScriptable] private PuzzleData data = default;

        

        // Start is called before the first frame update
        void Start()
        {
            data.grid = new HexGrid(scale, 1);
            data.grid.SetGrid();
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
            data.grid.DoubleGrid();
            data.pattern.DoubleSize();
        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            data.pattern = new Pattern(data.grid, settings[0]);

            for (int i = 1; i < settings.Length; i++)
            {
                data.grid.DoubleGrid();
                data.pattern.DoubleSize();
                data.pattern.AddToShape(settings[i], true);
            }

            data.grid = new HexGrid(data.pattern);
            data.pattern = new Pattern(data.grid, patternSettings);
            //Debug.Log("Segment amount: " + pattern.segments.Count);
        }

        [ContextMenu("Reset")]
        public void ResetGrid()
        {
            data.grid = new HexGrid(scale, 1);
            data.grid.SetGrid();

            data.pattern = null;
        }

        private void OnDrawGizmosSelected()
        {
            if (data.grid != null)
            {
                

                foreach (Segment segment in data.grid.segments)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(data.grid.NodeToWorldPosition(segment.a), 0.05f);
                    Gizmos.DrawSphere(data.grid.NodeToWorldPosition(segment.b), 0.05f);
                    //Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.85f);
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(data.grid.NodeToWorldPosition(segment.a), data.grid.NodeToWorldPosition(segment.b));
                }
            }

            if (data.pattern != null)
            {
                Gizmos.color = Color.yellow;

                foreach (Segment segment in data.pattern.segments)
                {
                    Gizmos.DrawLine(data.grid.NodeToWorldPosition(segment.a), data.grid.NodeToWorldPosition(segment.b));
                }
            }
        }

    }
}