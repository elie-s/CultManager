using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

#pragma warning disable CS0414
namespace CultManager
{
    public class CultistAnimationBehaviour : MonoBehaviour
    {
        [SerializeField] private Platform platform;
        [SerializeField] private CultistAnimationState state = default;
        [SerializeField] private CultistsSprites sprites = default;
        [SerializeField] private SpriteRenderer sRenderer = default;
        [SerializeField] private Animator animator = default;
        [SerializeField] private InvestigatorBehaviour investigatorBehaviour = default;
        [SerializeField, DrawScriptable] private CultistAnimationSettings settings = default;

        public Cultist cultist { get; private set; }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            Stop();   
        }

        public void Init(Platform _platform, Cultist _cultist, int _orderInLayer)
        {
            platform = _platform;
            platform.RegisterCultist();
            cultist = _cultist;

            sRenderer.sortingOrder = _orderInLayer;
            sRenderer.sprite = sprites.GetSprite(cultist.spriteIndex);
            animator.runtimeAnimatorController = sprites.GetAnimatorController(cultist.spriteIndex);

            if (!cultist.isInvestigator) Destroy(investigatorBehaviour);

            StartCoroutine(BehavioursRoutine());
        }

        public void OnDestroy()
        {
            platform.UnregisterCultist();
        }

        public void Stop()
        {
            StopAllCoroutines();
            SetState(CultistAnimationState.lifted);
            sRenderer.sortingLayerName = "Investigators";
        }

        private IEnumerator BehavioursRoutine()
        {
            if (settings.idleWanderRandom.Binary) yield return IdlingRoutine();
            else yield return WanderingRoutine();

            SetState(CultistAnimationState.idle);

            yield return new WaitForSeconds(1.0f);

            StartCoroutine(BehavioursRoutine());
        }

        private void SetState(CultistAnimationState _state)
        {
            state = _state;
            animator.SetInteger("State", (int)_state);

            if(state == CultistAnimationState.idle) sRenderer.sprite = sprites.GetSprite(cultist.spriteIndex);
        }

        private IEnumerator WanderingRoutine()
        {
            SetState(CultistAnimationState.wandering);

            Vector3 endPos = GetNewPos();
            Vector3 startPos = transform.localPosition;
            Iteration iteration = new Iteration(Vector2.Distance(startPos, endPos) / settings.movementSpeed);
            if (startPos.x > endPos.x) sRenderer.flipX = true;

            while (iteration.isBelowOne)
            {
                transform.localPosition = Vector3.Lerp(startPos, endPos, iteration.fraction);

                yield return iteration.YieldIncrement();
            }

            transform.localPosition = endPos;
            sRenderer.flipX = false;
        }


        private IEnumerator IdlingRoutine()
        {
            SetState(CultistAnimationState.idle);

            yield return new WaitForSeconds(Random.Range(settings.minIdleDuration, settings.maxIdleDuration));
        }

        private Vector3 GetNewPos()
        {
            if (!platform) return transform.localPosition;

            Vector3 result = default;

            do
            {
                result = platform.Evaluate(Random.value);
                
            } while (Vector3.Distance(result, transform.position) < settings.minimumDistance);

            result = new Vector3(result.x, result.y, transform.position.y);

            Vector3 tmpPos = transform.position;
            transform.position = result;
            result = transform.localPosition;
            transform.position = tmpPos;

            return result;
        }
    }
}