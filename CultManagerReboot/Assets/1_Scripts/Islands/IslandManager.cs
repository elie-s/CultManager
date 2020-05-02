﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class IslandManager : MonoBehaviour
    {
        [SerializeField] private Platform[] platforms = default;
        [SerializeField] private Transform cultistParent = default;

        public Dictionary<Cultist, GameObject> cultists { get; private set; }

        private void Awake()
        {
            cultists = new Dictionary<Cultist, GameObject>();
        }

        public void SpawnCultists(Cultist[] _cultists, GameObject _prefab)
        {
            if(cultists == null) cultists = new Dictionary<Cultist, GameObject>();

            for (int i = 0; i < _cultists.Length; i++)
            {
                cultists.Add(_cultists[i], platforms[i % platforms.Length].SpawnCultist(_prefab, cultistParent, true));
            }
        }

        public bool HoldCultist(Cultist _cultist)
        {
            return cultists.ContainsKey(_cultist);
        }

        public bool RemoveCultist(Cultist _cultist)
        {
            if(HoldCultist(_cultist))
            {
                Destroy(cultists[_cultist]);
                cultists.Remove(_cultist);
                return true;
            }

            return false;
        }
    }
}