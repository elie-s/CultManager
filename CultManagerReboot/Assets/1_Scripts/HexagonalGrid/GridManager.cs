using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private HexagonalGrid grid = default;
    [SerializeField] private int shapeSegments = 5;
    [SerializeField] private bool startAtCenter = true;
    [SerializeField] private HexagonalGridPattern.Mode mode;

    private Vector2Int randomNode = default;
    [SerializeField] private HexagonalGridPattern pattern;

    // Start is called before the first frame update
    void Start()
    {
        grid.SetGrid();
        pattern = new HexagonalGridPattern(grid, startAtCenter, mode);
        //SetRandom();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) NewPattern();
    }

    public void NewPattern()
    {
        pattern.StartAtCenter(startAtCenter);
        pattern.NewMode(mode);
        pattern.NewShape(shapeSegments);
    }

    [ContextMenu("SetRandom")]
    public void SetRandom()
    {
        randomNode = grid.GetRandomNode();
        Debug.Log(grid.GetSlice(randomNode));
    }

    [ContextMenu("DoubleGrid")]
    public void DoubleGrid()
    {
        grid.DoubleGrid();
        pattern.DoubleSize();
    }

    private void OnDrawGizmosSelected()
    {
        if(grid != null)
        {
            Gizmos.color = Color.blue;

            foreach (Vector2Int node in grid.nodes)
            {
                Gizmos.DrawSphere(grid.NodeToWorldPosition(node), 0.05f);
            }

            Gizmos.color = Color.yellow;

            foreach (HexagonalGridSegment segment in pattern.segments)
            {
                Gizmos.DrawLine(grid.NodeToWorldPosition(segment.a), grid.NodeToWorldPosition(segment.b));
            }
        }
    }
}
