using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class SacrificeIslandManager : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private CultManager manager = default;
        [SerializeField] private BloodBankManager blood = default;
        [SerializeField] private Platform aPlatform = default;
        [SerializeField] private Platform bPlatform = default;
        [SerializeField] private Platform oPlatform = default;
        [SerializeField] private Transform cultistParent = default;
        [SerializeField] private Transform fountain = default;

        [SerializeField] private int bloodPerCultist;

        public Dictionary<Cultist, SacrificedBehaviour> cultists { get; private set; }

        private void Awake()
        {
            cultists = new Dictionary<Cultist, SacrificedBehaviour>();
            SetBloodPerCultist();
        }

        public void SetBloodPerCultist()
        {
            switch (GameManager.currentLevel)
            {
                case 1:
                    {
                        bloodPerCultist = 10;
                    }
                    break;
                case 2:
                    {
                        bloodPerCultist = 15;
                    }
                    break;
                case 3:
                    {
                        bloodPerCultist = 20;
                    }
                    break;
                case 4:
                    {
                        bloodPerCultist = 25;
                    }
                    break;
                case 5:
                    {
                        bloodPerCultist = 35;
                    }
                    break;
                default:
                    {
                        bloodPerCultist = 10;
                    }
                    break;
            }
        }

        public void SpawnCultists(Cultist[] _cultists, GameObject _prefab)
        {
            if (cultists == null) cultists = new Dictionary<Cultist, SacrificedBehaviour>();

            for (int i = 0; i < _cultists.Length; i++)
            {
                switch (_cultists[i].blood)
                {
                    case BloodType.none:
                        break;
                    case BloodType.O:
                        cultists.Add(_cultists[i], oPlatform.SpawnCultist(_prefab, cultistParent, _cultists[i], true).GetComponent<SacrificedBehaviour>());
                        break;
                    case BloodType.A:
                        cultists.Add(_cultists[i], aPlatform.SpawnCultist(_prefab, cultistParent, _cultists[i], true).GetComponent<SacrificedBehaviour>());
                        break;
                    case BloodType.B:
                        cultists.Add(_cultists[i], bPlatform.SpawnCultist(_prefab, cultistParent, _cultists[i], true).GetComponent<SacrificedBehaviour>());
                        break;
                    case BloodType.AB:
                        break;
                    default:
                        break;
                }

                cultists[_cultists[i]].SetCultist(_cultists[i]);
                cultists[_cultists[i]].GetComponent<InvestigatiorBehavior>().enabled = false;
            }
        }

        public bool HoldCultist(Cultist _cultist)
        {
            return cultists.ContainsKey(_cultist);
        }

        public void RemoveCultist(Cultist _cultist)
        {
            if (HoldCultist(_cultist))
            {
                Destroy(cultists[_cultist].gameObject);
                cultists.Remove(_cultist);
            }
        }

        public void KillCultist(Cultist _cultist)
        {
            RemoveCultist(_cultist);
            manager.RemoveCutlist(_cultist);

            blood?.IncreaseBloodOfType(_cultist.blood, bloodPerCultist);
        }

        public void RemoveACultist()
        {
            Cultist sacrificed = null;

            foreach (Cultist cultist in data.cultists)
            {
                if (cultist.blood == BloodType.A)
                {
                    sacrificed = cultist;
                    break;
                }
            }

            if (sacrificed == null || !HoldCultist(sacrificed)) return;

            StartCoroutine(cultists[sacrificed].DieRoutine(fountain.position, KillCultist));
        }

        public void RemoveBCultist()
        {
            Cultist sacrificed = null;

            foreach (Cultist cultist in data.cultists)
            {
                if (cultist.blood == BloodType.B)
                {
                    sacrificed = cultist;
                    break;
                }
            }

            if (sacrificed == null || !HoldCultist(sacrificed)) return;

            StartCoroutine(cultists[sacrificed].DieRoutine(fountain.position, KillCultist));
        }

        public void RemoveOCultist()
        {
            Cultist sacrificed = null;

            foreach (Cultist cultist in data.cultists)
            {
                if (cultist.blood == BloodType.O)
                {
                    sacrificed = cultist;
                    break;
                }
            }

            if (sacrificed == null || !HoldCultist(sacrificed)) return;

            StartCoroutine(cultists[sacrificed].DieRoutine(fountain.position, KillCultist));
        }
    }
}