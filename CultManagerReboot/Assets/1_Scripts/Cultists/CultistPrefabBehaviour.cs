using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class CultistPrefabBehaviour : MonoBehaviour
    {
        [SerializeField] private CultistsSprites sprites = default;
        [SerializeField] private SpriteRenderer sRenderer = default;
        [SerializeField] private Animator animator = default;
        [SerializeField] private CultistAnimationBehaviour animationBehaviour = default;
        [SerializeField] private SacrificedBehaviour sacrificedBehaviour = default;
        [SerializeField] private InvestigatorBehaviour investigatorBehaviour = default;

        private CultManager cultManager = default;
        private Cultist cultist;

        public void Init(Cultist _cultist, Platform _platform)
        {
            cultist = _cultist;
        }

        public Cultist GetCultist()
        {
            return cultist;
        }

        public SacrificedBehaviour GetSacrificeBehaviour()
        {
            return sacrificedBehaviour;
        }

        public void KillCultist()
        {
            cultManager.RemoveCutlist(cultist);
        }
    }
}