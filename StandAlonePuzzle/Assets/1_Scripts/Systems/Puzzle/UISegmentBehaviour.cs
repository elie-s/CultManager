using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CultManager
{
    public class UISegmentBehaviour : SegmentBehaviour
    {
        [SerializeField] private Image UIRenderer = default;
        [SerializeField] private Color selectedColor = default;
        [SerializeField] private Color unselectedColor = default;
        public override void Init(PuzzleSegment _segment, float _scale)
        {
            UIRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            segment = _segment;
            SetRotation();
            UIRenderer.rectTransform.localScale = Vector3.one;
            transform.parent.localScale = Vector3.one * _scale;
            SetColor();
        }

        protected override void SetColor()
        {
            UIRenderer.color = selected ? selectedColor : unselectedColor;
        }
    }
}
