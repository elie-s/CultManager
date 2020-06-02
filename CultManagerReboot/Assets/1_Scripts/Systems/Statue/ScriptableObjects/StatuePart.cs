﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Statue/Statue Part")]
    public class StatuePart : ScriptableObject
    {
        [SerializeField] private Sprite _completedSprite = default;
        [SerializeField] private Sprite _uncompletedSprite = default;
        [SerializeField] private Gauge _completion = default;
        [SerializeField] private IntGauge _workers = default;
        [SerializeField] private int _moneyCost = default;
        [SerializeField] private int _relicCost = default;
        [SerializeField] private bool _bought = default;
        [SerializeField] private bool _completed = false;

        public Sprite completedSprite => _completedSprite;
        public Sprite uncompletedSprite => _uncompletedSprite;
        public Gauge completion => _completion;
        public IntGauge workers => _workers;
        public (int money, int relic) cost => (_moneyCost, _relicCost);
        public bool bought => _bought;
        public bool completed => _completed;

        public void Reset()
        {
            _completion.Reset();
            _workers.Reset();
            _bought = false;
        }

        public (bool failed, int moneySpent, int relicSpent) Buy(int _money, int _relic)
        {
            if (_money >= cost.money && _relic >= cost.relic)
            {
                _bought = true;
                return (false, cost.money, cost.relic);
            }

            return (true, 0, 0);
        }

        public void Build(float _workerForce)
        {
            if (!bought && !completed) return;

            float increment = _workerForce * workers.value * (workers.isFull ? 2.0f : 1.0f);

            completion.Increment(increment);

            _completed = completion.isFull;
        }


        public void Load(StatuePartSave _save)
        {
            _completion.SetValue(_save.completionValue);
            _workers.SetValue(_save.workersValue);
            _bought = _save.boughtValue;
            _completed = _save.completedValue;
        }

        public StatuePartSave Save()
        {
            return new StatuePartSave(_completion.value, _workers.value, _bought, _completed) ;
        }
    }

    [System.Serializable]
    public struct StatuePartSave
    {
        public float completionValue;
        public int workersValue;
        public bool boughtValue;
        public bool completedValue;

        public StatuePartSave(float _completion, int _workers, bool _bought, bool _completed)
        {
            completionValue = _completion;
            workersValue = _workers;
            boughtValue = _bought;
            completedValue = _completed;
        }
    }
}