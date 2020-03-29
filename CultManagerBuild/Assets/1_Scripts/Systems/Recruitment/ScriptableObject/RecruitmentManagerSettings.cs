using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Recruitment/Manager Settings")]
    public class RecruitmentManagerSettings : ScriptableObject
    {
        public int propositionsAmount = 10;
        public int validations = 2;
        public GameObject recruitmentCardPrefab = default;
        public Sprite[] recruitmentPhotos = default;
    }
}