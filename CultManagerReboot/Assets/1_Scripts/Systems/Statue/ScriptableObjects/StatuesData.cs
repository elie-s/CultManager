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
        [SerializeField] private DemonBrotherhood[] _family = default;
        [SerializeField] private PuzzleDataSet puzzles = default;

        private List<DemonName> _demonsAvailable = new List<DemonName>();

        public DemonName currentDemon => _currentDemon;
        public float baseWorkForce => _baseWorkForce;
        public StatueSet currentStatueSet => (int)currentDemon < _statueSets.Length && _statueSets[(int)currentDemon] != null ? _statueSets[(int)currentDemon] : _defaultStatueSet;
        public DateTime timeRef { get; private set; }
        public DemonName[] demonsAvailable => _demonsAvailable.ToArray();
        public DemonBrotherhood[] family => _family;
        public List<PuzzleSegment> currentPuzzle => puzzles.GetPuzzle(_currentDemon);

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

        public StatueSet GetStatueSet(int _index)
        {
            if (_index < 0) return null;

            return _index < _statueSets.Length  && _statueSets[_index] != null ? _statueSets[_index] : _defaultStatueSet;
        }

        public StatueSet GetStatueSet(DemonName _demon)
        {
            return GetStatueSet((int)_demon);
        }

        public int GetBrotherhoodIndex(DemonName _demon)
        {
            for (int i = 0; i < _family.Length; i++)
            {
                if (family[i].Contains(_demon)) return i;
            }

            return -1;
        }

        public bool CheckCompletion(int _index)
        {
            return family[_index].Completed(this);
        }

        public void UpdateAvailability(DemonName _demon)
        {
            if (_demonsAvailable.Contains(_demon)) return;

            int brotherhoodIndex = GetBrotherhoodIndex(_demon);

            if (brotherhoodIndex == 0)
            {
                GetStatueSet(_demon).SetAvailable();
                _demonsAvailable.Add(_demon);

                return;
            }

            if (CheckCompletion(brotherhoodIndex - 1))
            {
                GetStatueSet(_demon).SetAvailable();
                _demonsAvailable.Add(_demon);
            }
        }

        public void BuyStatue(DemonName _demonName)
        {
            StatueSet statue = GetStatueSet(_demonName);
            statue.Buy();
            _currentDemon = _demonName;
        }

        public void CurrentSummoned()
        {
            currentStatueSet.SetSummoned();
            _currentDemon = DemonName.None;
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
            _currentDemon = DemonName.None;
            _demonsAvailable = new List<DemonName>();
        }

        public void LoadSave(Save _save)
        {
            for (int i = 0; i < _statueSets.Length; i++)
            {
                _statueSets[i].Load(_save.statueSetSaves[i]);
            }

            _currentDemon = (DemonName)_save.demonName;
            timeRef = _save.statueTimeRef;

            _demonsAvailable = new List<DemonName>();

            foreach (int value in _save.demonsBought)
            {
                _demonsAvailable.Add((DemonName)value);
            }
        }

        public int[] DemonsBoughtToIntArray()
        {
            List<int> result = new List<int>();

            foreach (DemonName demon in _demonsAvailable)
            {
                result.Add((int)demon);
            }

            return result.ToArray();
        }
    }
}