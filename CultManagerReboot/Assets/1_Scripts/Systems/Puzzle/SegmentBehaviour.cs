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
        [SerializeField] private Color[] bloodTypeColorDisable = new Color[3];
        [SerializeField] private Sprite[] sprites = default;
        [SerializeField] private PuzzleData data = default;

        private PuzzleSegment segment;
        private bool selected = false;

        public void Init(Segment _segment, float _scale)
        {
            sRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            segment = new PuzzleSegment(_segment);
            SetRotation();
            transform.localScale = Vector3.one * _scale;
            SetColor();
        }

        public void Init(PuzzleSegment _segment, float _scale)
        {
            sRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
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
                if (segment.a.y == segment.b.y) transform.localEulerAngles = Vector3.forward * 90;
                if (segment.a.y == segment.b.y - 1) transform.localEulerAngles = Vector3.forward * 150;
            }
        }

        private void SetColor()
        {
            sRenderer.color = selected ? bloodTypeColor[(int)segment.type - 1] : bloodTypeColorDisable[(int)segment.type - 1];
        }

        public void Select(bool _value)
        {
            selected = _value;
            segment.selected = _value;
            SetColor();
        }

        public void InverSelection()
        {
            if (segment.canBeSelected)
            {
                Select(!selected);
                ToggleNeighbours();
            }
            else
            {
                CheckFirstSelection();
            }
        }

        public void ToggleNeighbours()
        {
            for (int i = 0; i < data.puzzle.Count; i++)
            {
                if (data.puzzle[i].IsConnected(segment) && !data.puzzle[i].IsSegment(segment.segment))
                {
                    if (segment.selected)
                    {
                        data.puzzle[i].EnableSegment();
                    }
                    else
                    {
                        data.puzzle[i].DisableSegment();
                    }
                    
                }
            }
        }

        public void CheckFirstSelection()
        {
            int ctr = 0;
            for (int i = 0; i < data.puzzle.Count; i++)
            {
                if (data.puzzle[i].selected)
                {
                    ctr++;
                }
            }
            if (ctr == 0)
            {
                segment.canBeSelected = true;
                Select(!selected);
                ToggleNeighbours();
            }
        }


    }
}