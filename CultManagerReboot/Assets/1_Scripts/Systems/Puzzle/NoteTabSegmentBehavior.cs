using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CultManager
{
    public class NoteTabSegmentBehavior : SegmentBehaviour
    {
        [SerializeField] private Image UIRenderer = default;
        [SerializeField] private Color[] selectionColors = default;
        [SerializeField] private int index=0;

        private NoteTabPanelBehavior panelBehavior;

        private Color currentColor => selectionColors[index];

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
            UIRenderer.color = currentColor;
        }

        public void ToggleSelction()
        {
            if (index < selectionColors.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            SetColor();
        }
    }
}
