using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Statue/Data")]
    public class StatuesData : ScriptableObject
    {
        [SerializeField] StatueSet[] _statueSets = default;
        [SerializeField] private StatueSet _defaultStatueSet = default;
        [SerializeField] DemonName _currentDemon = default;
        [SerializeField] private float _baseWorkForce = 1.0f;

        public StatueSet[] statueSets => _statueSets;
        public DemonName currentDemon => _currentDemon;
        public float baseWorkForce => _baseWorkForce;
        public StatueSet currentStatueSet => statueSets.Length < (int)currentDemon && statueSets[(int)currentDemon] != null ? statueSets[(int)currentDemon] : _defaultStatueSet;
        public DateTime timeRef { get; private set; }

        public void SetDemon(DemonName _demon)
        {
            _currentDemon = _demon;
        }

        public void UpdateTimeRef()
        {
            timeRef = DateTime.Now;
        }

        public float SecondsFromTimeRef()
        {
            TimeSpan timeSpan = DateTime.Now - timeRef;

            return (float)timeSpan.TotalSeconds;
        }

        public StatueSetSave[] SaveSets()
        {
            List<StatueSetSave> result = new List<StatueSetSave>();

            foreach (StatueSet set in _statueSets)
            {
                result.Add(set.Save());
            }

            return result.ToArray();
        }

        public void Reset()
        {
            foreach (StatueSet set in _statueSets)
            {
                set.Reset();
            }

            UpdateTimeRef();
        }

        public void LoadSave(Save _save)
        {
            for (int i = 0; i < _statueSets.Length; i++)
            {
                _statueSets[i].Load(_save.statueSetSaves[i]);
            }

            _currentDemon = (DemonName)_save.demonName;
            timeRef = _save.statueTimeRef;
        }
    }
}