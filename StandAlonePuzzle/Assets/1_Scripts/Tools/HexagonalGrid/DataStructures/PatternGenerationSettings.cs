using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager.HexagonalGrid
{
    [System.Serializable]
    public class PatternGenerationSettings
    {
        public int shapeSegments = 1;
        public bool startAtCenter = true;
        public Pattern.Mode mode;
    }
}