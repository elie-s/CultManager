using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CultManager.HexagonalGrid;

namespace CultManager
{
    public class NoteTabSegmentBehavior : SegmentBehaviour
    {
        [SerializeField] private Image UIRenderer = default;
        [SerializeField] private Color[] selectionColors = default;
        [SerializeField] private int index=0;

        [SerializeField]private NoteTabPanelBehavior panelBehavior;
        [SerializeField]private NoteTabData noteTabData;

        private Color currentColor => selectionColors[index];

        private void OnEnable()
        {
            panelBehavior = FindObjectOfType<NoteTabPanelBehavior>();
        }

        public override void Init(PuzzleSegment _segment, float _scale)
        {
            panelBehavior = FindObjectOfType<NoteTabPanelBehavior>();
            UIRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            segment = _segment;
            SetRotation(UIRenderer.rectTransform);
            UIRenderer.rectTransform.localScale = Vector3.one * _scale;
            //transform.parent.localScale = Vector3.one;
            index = panelBehavior.GetIndex(segment.segment);
            SetColor();

            UIRenderer.rectTransform.anchoredPosition = Node.WorldPosition(segment.b, _scale * 90);
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
            panelBehavior.SetIndex(segment.segment, index);

        }
    }
}
