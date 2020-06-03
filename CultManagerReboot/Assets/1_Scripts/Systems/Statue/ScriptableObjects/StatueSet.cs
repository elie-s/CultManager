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
        public int index { get; private set; }
        public StatuePart currentPart => parts[index];

        public GameObject Instantiate(Vector3 _worldPos, Transform _parent)
        {
            GameObject statue = GameObject.Instantiate(statuePrefab, _worldPos, Quaternion.identity, _parent);
            statue.GetComponent<StatuePrefabManager>().Init(parts);
            index = 0;

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

        public bool UpdateCompletionPart(float _workerForce, float _delay, int _index)
        {
            if (!bought && !completed) return false;

            return parts[_index].Build(_workerForce, _delay);
        }

        public void UpdateCompletion(float _workerForce, float _delay, System.Action _onPartCompleted, System.Action _onSetCompleted)
        {
            bool testPart = false;

            for (int i = 0; i < parts.Length; i++)
            {
                if (UpdateCompletionPart(_workerForce, _delay, i)) testPart = true; ;
            }

            if (testPart) _onPartCompleted();

            bool tmp = _completed;
            _completed = true;

            foreach (StatuePart part in parts)
            {
                if (!part.completion.isFull)
                {
                    _completed = false;
                    break;
                }
            }

            if (!tmp && _completed) _onSetCompleted();
        }

        public int AssignWorkersCurrent(int _amount)
        {
            if (!parts[index].bought) return -1;

            int assigned = _amount <= parts[index].workers.amountLeft ? _amount : parts[index].workers.amountLeft;
            parts[index].workers.Increment(assigned);

            return assigned;
        }

        public int RemoveWorkersCurrent(int _amount)
        {
            if (!parts[index].bought) return -1;

            int removed = _amount <= parts[index].workers.value ? _amount : parts[index].workers.value;
            parts[index].workers.Decrement(removed);

            return removed;
        }

        public int AssignWorkers(int _amount, int _index)
        {
            if (!parts[index].bought) return -1;

            int assigned = _amount <= parts[index].workers.amountLeft ? _amount : parts[index].workers.amountLeft;
            parts[index].workers.Increment(assigned);

            return  assigned;
        }

        public int RemoveWorkers(int _amount, int _index)
        {
            if (!parts[index].bought) return -1;

            int removed = _amount <= parts[index].workers.value ? _amount : parts[index].workers.value;
            parts[index].workers.Decrement(removed);

            return removed;
        }

        public void IncreaseIndex()
        {
            index = (index + 1) % parts.Length;
        }

        public void DecreaseIndex()
        {
            index--;
            index = index < 0 ? parts.Length - 1 : index;
        }

        public void SetIndex(int _index)
        {
            index = Mathf.Clamp(_index, 0, parts.Length - 1);
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
