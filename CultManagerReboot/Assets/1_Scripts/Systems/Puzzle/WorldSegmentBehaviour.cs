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

        public override void Init(PuzzleSegment _segment, float _scale)
        {
            sRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            segment = _segment;
            SetRotation();
            transform.localScale = Vector3.one * _scale;
            SetColor();
        }

        protected override void SetColor()
        {
            sRenderer.color = selected ? bloodTypeColor[(int)segment.type - 1] : bloodTypeColorDisable[(int)segment.type - 1];
        }
    }
}