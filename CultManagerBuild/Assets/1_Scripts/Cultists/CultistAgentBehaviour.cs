using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class CultistAgentBehaviour : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites = default;
        [SerializeField] private SpriteRenderer sRenderer = default;

        private void OnEnable()
        {
            SetSprite();
        }

        private void SetSprite()
        {
            sRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}