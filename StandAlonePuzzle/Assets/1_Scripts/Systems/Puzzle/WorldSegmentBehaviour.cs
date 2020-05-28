using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    public class WorldSegmentBehaviour : SegmentBehaviour
    {
        [SerializeField] protected SpriteRenderer sRenderer = default;
        [SerializeField] protected SpriteRenderer outline = default;
        [SerializeField] protected Color[] bloodTypeColor = new Color[3];
        [SerializeField] protected Color[] bloodTypeColorDisable = new Color[3];
        [SerializeField] protected Gradient outlineColor = default;
        [SerializeField] BloodType blood = default;

        private BloodBankManager bloodManager;
        private bool isFading;

        public override void Init(PuzzleSegment _segment, float _scale)
        {
            bloodManager = FindObjectOfType<BloodBankManager>();
            sRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            segment = _segment;
            SetRotation();
            transform.localScale = Vector3.one * _scale;
            SetColor();
            blood = segment.type;

            transform.localPosition = Node.WorldPosition(segment.b, _scale);
        }

        public void ResetSegment()
        {
            GetComponent<AreaInteraction>().enabled = true;
            outline.color = outlineColor.Evaluate(0.0f);
        }

        public void FadeOutUnselected(float _duration, float _alphaLimit, AnimationCurve _fadeCurve)
        {
            GetComponent<AreaInteraction>().enabled = false;
            if (isFading) return;

            if(!selected)StartCoroutine(FadeOut(_duration, _alphaLimit, _fadeCurve));
            else StartCoroutine(OutlineRoutine(_duration, _fadeCurve));
        }

        private IEnumerator FadeOut(float _duration, float _alphaLimit, AnimationCurve _fadeCurve)
        {
            Color startColor = sRenderer.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, _alphaLimit);
            Iteration iteration = new Iteration(_duration, _fadeCurve);

            while (iteration.isBelowOne)
            {
                sRenderer.color = Color.Lerp(startColor, endColor, iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            sRenderer.color = endColor;
        }

        private IEnumerator OutlineRoutine(float _duration, AnimationCurve _fadeCurve)
        {
            Iteration iteration = new Iteration(_duration, _fadeCurve);

            while (iteration.isBelowOne)
            {
                outline.color = outlineColor.Evaluate(iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            outline.color = outlineColor.Evaluate(1.0f);
        }

        protected override void SetColor()
        {
            sRenderer.color = selected ? bloodTypeColor[(int)segment.type] : bloodTypeColorDisable[(int)segment.type];
        }

        public void InverSelection()
        {
            if (segment.canBeSelected)
            {
                if (selected)
                {
                    if (bloodManager.CanIncrease(segment.type, 10))
                    {
                        Debug.Log("UnSelected");
                        Select(!selected);
                        ToggleNeighbours();
                        
                        bloodManager.IncreaseBloodOfType(segment.type, 10);
                        bloodManager.UseOfBloodOfType(segment.type);
                    }
                }
                else if (!selected)
                {
                    if (bloodManager.CanDecrease(segment.type, 10))
                    {
                        Debug.Log("Selected");
                        Select(!selected);
                        ToggleNeighbours();
                        //segment.DisableSegment();
                        bloodManager.DecreaseBloodOfType(segment.type, 10);
                        bloodManager.UseOfBloodOfType(segment.type);
                    }
                    else
                    {
                        bloodManager.InAdequateBloodOfType(segment.type);
                    }
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
                    if (segment.canBeSelected)
                    {
                        if (!data.puzzle[i].selected)
                            data.puzzle[i].EnableSegment();
                        else
                            data.puzzle[i].DisableSegment();
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
                if (!selected)
                {
                    if (bloodManager.CanDecrease(segment.type, 10))
                    {
                        segment.canBeSelected = true;
                        Select(!selected);
                        ToggleNeighbours();
                        //segment.DisableSegment();
                        bloodManager.DecreaseBloodOfType(segment.type, 10);
                        bloodManager.UseOfBloodOfType(segment.type);
                    }
                    else
                    {
                        bloodManager.InAdequateBloodOfType(segment.type);
                    }
                }
            }
            else
            {
                bloodManager.InAdequateBloodOfType(segment.type);
            }
        }
    }
}