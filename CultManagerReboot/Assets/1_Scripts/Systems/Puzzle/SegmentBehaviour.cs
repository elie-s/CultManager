using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    public class SegmentBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sRenderer = default;
        [SerializeField] private Color[] bloodTypeColor = new Color[3];

        private PuzzleSegment segment;
        private bool selected = false;

        public void Init(Segment _segment, float _scale)
        {
            segment = new PuzzleSegment(_segment);
            SetRotation();
            transform.localScale = Vector3.one * _scale;
            SetColor();
        }

        public void Init(PuzzleSegment _segment, float _scale)
        {
            segment = _segment;
            SetRotation();
            transform.localScale = Vector3.one * _scale;
            SetColor();
        }

        private void SetRotation()
        {
            if (segment.a.x == segment.b.x + 1)
            {
                if (segment.a.y == segment.b.y + 1) transform.localEulerAngles = Vector3.forward * -30;
                if (segment.a.y == segment.b.y) transform.localEulerAngles = Vector3.forward * -90;
            }
            else if (segment.a.x == segment.b.x)
            {
                if (segment.a.y == segment.b.y + 1) transform.localEulerAngles = Vector3.forward * 30;
                if (segment.a.y == segment.b.y - 1) transform.localEulerAngles = Vector3.forward * -150;
            }
            else if (segment.a.x == segment.b.x - 1)
            {
                if (segment.a.y == segment.b.y ) transform.localEulerAngles = Vector3.forward * 90;
                if (segment.a.y == segment.b.y - 1) transform.localEulerAngles = Vector3.forward * 150;
            }
        }

        private void SetColor()
        {
            sRenderer.color = bloodTypeColor[(int)segment.type - 1];
        }

        public void Select(bool _value)
        {
            selected = _value;
            UpdateColor();
        }

        public void InverSelection()
        {
            Select(!selected);
        }

        private void UpdateColor()
        {
            sRenderer.color = selected ? Color.red : Color.black;
        }
    }
}