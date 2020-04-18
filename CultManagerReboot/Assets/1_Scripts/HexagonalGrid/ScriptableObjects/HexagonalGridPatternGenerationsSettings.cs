using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CultManager/Puzzle/Generation Settings", fileName = "Generation Settings 1")]
public class HexagonalGridPatternGenerationsSettings : ScriptableObject
{
    public int shapeSegments = 1;
    public bool startAtCenter = true;
    public HexagonalGridPattern.Mode mode;
}
