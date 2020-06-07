using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CultManager
{
    public class TinderCardDisplayer : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private Image candidateImage = default;
        [SerializeField] private TextMeshProUGUI candidateName = default;
        [SerializeField] private TextMeshProUGUI candidateMoney = default;
        [SerializeField] private TextMeshProUGUI candidatePolice = default;
        [SerializeField] private TextMeshProUGUI candidateRemaining = default;
        [SerializeField] private UISwitch[] bloodSwitch = default;
        [SerializeField] private ButtonInteraction acceptButton = default;
        [SerializeField] private ButtonInteraction rejectButton = default;
        [SerializeField] private UISwitch panelBackground = default;
        [SerializeField] private CanvasGroup card = default;
        [SerializeField] private Sprite[] images = default;
        [SerializeField] private float defaultDuration = 0.5f;

        private bool fading = false;
        private Candidate toDisplay;

        public bool Display()
        {
            candidateRemaining.text = data.candidatesCount.ToString();
            EnableInteractions(true);

            if (toDisplay == null || data.candidatesCount == 0)
            {
                candidateImage.sprite = images[0];
                candidateName.text = "Pierre";
                candidateMoney.text = 999999.Format();
                candidatePolice.text = 13.Format();

                CloseCard(0.0f);
                EnableInteractions(false);

                return false;
            }
            
            if(toDisplay.cultist == null)
            {
                Debug.LogError("Cultist is null");
                return false;
            }

            candidateImage.sprite = images[toDisplay.cultist.spriteIndex];
            candidateName.text = toDisplay.cultist.cultistName;
            candidateMoney.text = toDisplay.moneyValue.Format();
            candidatePolice.text = toDisplay.policeValue.Format();
            UpdateBlood(toDisplay.cultist.blood);

            OpenCard(defaultDuration);

            return true;
        }

        private void EnableInteractions(bool _value)
        {
            if(_value)
            {
                acceptButton.EnableButton();
                rejectButton.EnableButton();
                panelBackground.SetA();
            }
            else
            {
                acceptButton.DisableButton();
                rejectButton.DisableButton();
                panelBackground.SetB();
            }
        }

        public void OpenCard(float _fadeDuration)
        {
            if (!fading) StartCoroutine(FadeCard(true, _fadeDuration));
        }

        public void CloseCard(float _fadeDuration)
        {
            if (!fading) StartCoroutine(FadeCard(false, _fadeDuration));
        }

        public void SetCandidate(Candidate _candidate)
        {
            //Debug.LogError(_candidate.cultist == null ? "Cultist is null" : _candidate.cultist.cultistName);
            toDisplay = _candidate;
        }

        public IEnumerator FadeCard(bool _in, float _duration)
        {
            fading = true;

            Iteration iteration = new Iteration(_duration);

            while (iteration.isBelowOne)
            {
                card.alpha = _in ? iteration.fraction : 1 - iteration.fraction;

                yield return iteration.YieldIncrement();
            }

            card.alpha = _in ? 1.0f : 0.0f;
            fading = false;
        }

        private void UpdateBlood(BloodType _blood)
        {
            switch (_blood)
            {
                case BloodType.none:
                    break;

                case BloodType.O:
                    bloodSwitch[0].SetC();
                    bloodSwitch[1].SetC();
                    break;

                case BloodType.A:
                    bloodSwitch[0].SetA();
                    bloodSwitch[1].SetA();
                    break;

                case BloodType.B:
                    bloodSwitch[0].SetB();
                    bloodSwitch[1].SetB();
                    break;

                case BloodType.AB:
                    break;

                default:
                    break;
            }
        }
    }
}