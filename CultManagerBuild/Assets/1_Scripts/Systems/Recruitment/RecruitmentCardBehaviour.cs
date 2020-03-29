using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CultManager
{
    public class RecruitmentCardBehaviour : MonoBehaviour
    {
        [SerializeField] private Image photo = default;
        [SerializeField] private TextMeshProUGUI descriptionField = default;
        [SerializeField] private TextMeshProUGUI moneyField = default;
        [SerializeField] private TextMeshProUGUI policeField = default;
        [SerializeField] private GameObject[] traits = default;
        [SerializeField] private RectTransform rectTransform = default;
        [SerializeField, DrawScriptable] private RecruitmentCardBehaviourSettings settings = default;

        private Vector2 startPosition;
        private Camera cam;
        private bool onRoutine;
        private System.Action onSwipedLeft;
        private System.Action onSwipedRight;

        // Start is called before the first frame update
        void Start()
        {
            startPosition = rectTransform.anchoredPosition;
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            Swipe();
        }

        private void Swipe()
        {
            if (onRoutine) return;

            Vector2 newPosition = new Vector2(cam.ViewportToScreenPoint(Gesture.Movement).x, startPosition.y);

            rectTransform.anchoredPosition = newPosition;

            if (Mathf.Abs(Gesture.Movement.x) > settings.swipeMovementThreshold && Gesture.ReleasedMovementTouch) StartCoroutine(GetOutRoutine());
        }

        private IEnumerator GetOutRoutine()
        {
            onRoutine = true;
            Vector2 initPosition = cam.ScreenToViewportPoint(rectTransform.anchoredPosition);
            Vector2 outPosition = new Vector2(initPosition.x > 0 ? 2.0f : -2.0f, initPosition.y);
            Vector2 newPosition = new Vector2(cam.ViewportToScreenPoint(Gesture.Movement).x, startPosition.y);
            Iteration iteration = new Iteration(settings.outDuration);

            while (iteration.isBelowOne)
            {
                newPosition = new Vector2(cam.ViewportToScreenPoint(Vector2.Lerp(initPosition, outPosition, iteration.fraction)).x, startPosition.y);

                rectTransform.anchoredPosition = newPosition;

               yield return iteration.YieldFixedIncrement();
            }

            if (initPosition.x > 0) onSwipedRight();
            else onSwipedLeft();

        }

        public void SetCultist(Cultist _cultist)
        {
            //photo.sprite = _cultist.spriteIndex;
            descriptionField.text = _cultist.cultistName + ", " + _cultist.age + ".";
        }

        public void SetCandidate(Candidate _candidate)
        {
            descriptionField.text = _candidate.cultist.cultistName + ", " + _candidate.cultist.age + ".";
            moneyField.text = _candidate.money.ToString();
            policeField.text = _candidate.policeValue.ToString();

            for (int i = 0; i < traits.Length; i++)
            {
                traits[i].SetActive(_candidate.cultist.traits.HasFlag((CultistTraits)i));
            }
        }

        public void SetPhoto(Sprite _sprite)
        {
            photo.sprite = _sprite;
        }


        public void SetCallbacks(System.Action _onSwipedLeft, System.Action _onSwipedRight)
        {
            onSwipedLeft = _onSwipedLeft;
            onSwipedRight = _onSwipedRight;
        }
    }
}