using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class MaskLocation
    {
        [Header("Mask")]
        public Transform maskPosition;
        public float scaleFactor;
        public bool maskActive;

        [Header("Text")]
        public string displayText;
        public RectTransform textPosition;
        public bool textInActive;
    }
}

