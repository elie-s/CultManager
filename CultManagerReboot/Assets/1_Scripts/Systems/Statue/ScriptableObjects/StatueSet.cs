using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Statue/Statue Set")]
    public class StatueSet : ScriptableObject
    {
        [SerializeField] private DemonName _demon = default;
        [SerializeField] private GameObject statuePrefab = default;
        [SerializeField] private StatuePart[] _parts = default;
        [SerializeField] private int _cost = 100;
        [SerializeField] private bool _bought = false;
        [SerializeField] private bool _completed = false;

        public int cost => _cost;
        public DemonName demon => _demon;
        public StatuePart[] parts => _parts;
        public bool bought => _bought;
        public bool completed => _completed;

        public GameObject Instantiate(Vector3 _worldPos, Transform _parent)
        {
            GameObject statue = GameObject.Instantiate(statuePrefab, _worldPos, Quaternion.identity, _parent);
            statue.GetComponent<StatuePrefabManager>().Init(parts);

            return statue;
        }

        public (bool failed, int moneySpent) Buy(int _money)
        {
            if (_money >= cost)
            {
                _bought = true;
                return (false, cost);
            }

            return (true, 0);
        }

        public void UpdateCompletionPart(float _workerForce, int _index)
        {
            if (!bought && !completed) return;

            float increment = _workerForce * parts[_index].workers.value * (parts[_index].workers.isFull ? 2.0f : 1.0f);

            parts[_index].Build(increment);
        }

        public void UpdateCompletion(float _workerForce)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                UpdateCompletionPart(_workerForce, i);
            }

            _completed = true;

            foreach (StatuePart part in parts)
            {
                if (!part.completion.isFull)
                {
                    _completed = false;
                    break;
                }
            }
        }

        public int AssignWorkers(int _amount, int _index)
        {
            if (!parts[_index].bought) return -1;

            int assigned = _amount <= parts[_index].workers.amountLeft ? _amount : parts[_index].workers.amountLeft;
            parts[_index].workers.Increment(assigned);

            return _amount - assigned;
        }

        public int RemoveWorkers(int _amount, int _index)
        {
            if (!parts[_index].bought) return -1;

            int removed = _amount <= parts[_index].workers.value ? _amount : parts[_index].workers.value;
            parts[_index].workers.Decrement(removed);

            return _amount - removed;
        }

        public void Load(StatueSetSave _save)
        {
            _bought = _save.boughtValue;

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].Load(_save.partsSave[i]);
            }
        }

        public StatueSetSave Save()
        {
            return new StatueSetSave(_bought, parts);
        }

        public void Reset()
        {
            Debug.Log("reset");

            _bought = false;
            foreach (StatuePart part in _parts)
            {
                part.Reset();
            }
        }
    }

    [System.Serializable]
    public struct StatueSetSave
    {
        public bool boughtValue;
        public StatuePartSave[] partsSave;

        public StatueSetSave(bool _bought, StatuePart[] _parts)
        {
            boughtValue = _bought;

            List<StatuePartSave> tmpSave = new List<StatuePartSave>();

            foreach (StatuePart part in _parts)
            {
                tmpSave.Add(part.Save());
            }

            partsSave = tmpSave.ToArray();
        }
    }
}
