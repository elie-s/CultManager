using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CultManager.HexagonalGrid;

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
            SetRotation(UIRenderer.rectTransform);
            UIRenderer.rectTransform.localScale = Vector3.one * _scale;
            SetColor();

            UIRenderer.rectTransform.anchoredPosition = Node.WorldPosition(segment.b, _scale*90);
        }

        protected override void SetColor()
        {
            UIRenderer.color = selected ? selectedColor : unselectedColor;
        }
    }
}
