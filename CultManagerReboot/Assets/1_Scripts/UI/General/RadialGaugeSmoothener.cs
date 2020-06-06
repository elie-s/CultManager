using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CultManager
{
    public class RadialGaugeSmoothener : MonoBehaviour
    {
        [SerializeField] private RectTransform arrow = default;
        [SerializeField] private Image gauge = default;
        [SerializeField] private float smoothValue = 0.2f;
        [SerializeField] private float radialMaxValue = 90.0f;

        private float reachValue;

        void Update()
        {
            LerpGauge();
        }

        public void SetValue(float _value)
        {
            reachValue = _value;
        }

        private void LerpGauge()
        {
            gauge.fillAmount = Mathf.Lerp(gauge.fillAmount, reachValue, smoothValue);
            arrow.localEulerAngles = Vector3.Lerp(arrow.localEulerAngles, new Vector3(0.0f, 0.0f, Mathf.Lerp(0.0f, radialMaxValue, reachValue)), smoothValue);
        }

        public void SetGauge(float _value)
        {
            reachValue = _value;
            gauge.fillAmount = _value;
            arrow.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Lerp(0.0f, radialMaxValue, reachValue));
        }
    }
}