using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class HexagonalGridPatternGenerationsSettings
    {
        public int shapeSegments = 1;
        public bool startAtCenter = true;
        public HexagonalGridPattern.Mode mode;
    }
}