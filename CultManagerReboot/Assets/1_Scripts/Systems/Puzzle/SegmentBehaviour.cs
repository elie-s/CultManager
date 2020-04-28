using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public abstract class SegmentBehaviour : MonoBehaviour
    {

        [SerializeField] protected Sprite[] sprites = default;
        [SerializeField] protected PuzzleData data = default;

        public PuzzleSegment segment { get; protected set; }
        [SerializeField]protected bool selected = false;

        public virtual void Init(PuzzleSegment _segment, float _scale) { }

        protected void SetRotation()
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

        protected abstract void SetColor();

        public void Select(bool _value)
        {
            selected = _value;
            segment.selected = _value;
            SetColor();
        }

        public void LocalSelect(bool _value)
        {
            selected = _value;
            SetColor();
        }

        public void InverSelection()
        {
            //Debug.Log("t");
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
                        if (!data.puzzle[i].selected)
                        {
                            data.puzzle[i].DisableSegment();
                        }
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