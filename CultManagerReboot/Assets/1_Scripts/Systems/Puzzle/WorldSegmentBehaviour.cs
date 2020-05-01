using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class WorldSegmentBehaviour : SegmentBehaviour
    {
        [SerializeField] protected SpriteRenderer sRenderer = default;
        [SerializeField] protected Color[] bloodTypeColor = new Color[3];
        [SerializeField] protected Color[] bloodTypeColorDisable = new Color[3];
        private BloodBankManager bloodManager;

        public override void Init(PuzzleSegment _segment, float _scale)
        {
            sRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            segment = _segment;
            SetRotation();
            transform.localScale = Vector3.one * _scale;
            SetColor();
            bloodManager = FindObjectOfType<BloodBankManager>();
        }

        protected override void SetColor()
        {
            sRenderer.color = selected ? bloodTypeColor[(int)segment.type - 1] : bloodTypeColorDisable[(int)segment.type - 1];
        }

        public void InverSelection()
        {
            if (segment.canBeSelected)
            {
                if (selected && bloodManager.CanIncrease(segment.type, 10))
                {
                    Debug.Log("UnSelected");
                    Select(!selected);
                    ToggleNeighbours();
                    bloodManager.IncreaseBloodOfType(segment.type, 10);
                }
                else if (!selected && bloodManager.CanDecrease(segment.type, 10))
                {
                    Debug.Log("Selected");
                    Select(!selected);
                    ToggleNeighbours();
                    bloodManager.DecreaseBloodOfType(segment.type, 10);
                }
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
                if (!selected && bloodManager.CanDecrease(segment.type, 10))
                {
                    segment.canBeSelected = true;
                    Select(!selected);
                    ToggleNeighbours();
                    bloodManager.DecreaseBloodOfType(segment.type, 10);
                }
            }
        }
    }
}