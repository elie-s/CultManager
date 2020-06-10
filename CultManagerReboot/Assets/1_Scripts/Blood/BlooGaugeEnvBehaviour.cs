using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class BlooGaugeEnvBehaviour : MonoBehaviour
    {
        [SerializeField] private BloodBankData data = default;
        [SerializeField] private BloodType type = default;
        [SerializeField] private Transform blood = default;
        [SerializeField] private float lerpValue = .2f;

        private void Update()
        {
            LerpBlood();
        }

        private void LerpBlood()
        {
            blood.localScale = Vector3.Lerp(blood.localScale, new Vector3(1.0f,  data.bloodBanks[(int)type].gauge.ratio, 1.0f), lerpValue);
        }
    }
}