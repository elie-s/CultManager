using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class CloudBehaviour : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites = default;
        [SerializeField] private AnimationCurve scaleSpeedRatio = default;
        [SerializeField] private float baseSpeed = 1.0f;
        [SerializeField] private float maxX = -10.0f;
        [SerializeField] private float ySpan = 10.0f;
        [SerializeField] private SpriteRenderer sRenderer = default;

        private float speed;
        private Vector3 startPos;

        private void OnEnable()
        {
            startPos = transform.position;
            StartCoroutine(Delay());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void SetSize()
        {
            float seed = Random.value;

            sRenderer.transform.localScale = Vector3.one * (0.5f + seed / 2.0f);
            speed = baseSpeed * scaleSpeedRatio.Evaluate(seed);
        }

        private void SetSprite()
        {
            sRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        private void SetPos()
        {
            transform.position = startPos + Vector3.up * Random.Range(-ySpan, ySpan);
        }

        private IEnumerator Move()
        {
            while (transform.position.x > maxX)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;

                yield return null;
            }

            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            SetSprite();
            SetSize();
            SetPos();

            yield return new WaitForSeconds(Random.value * 2.0f);

            StartCoroutine(Move());
        }
    }
}