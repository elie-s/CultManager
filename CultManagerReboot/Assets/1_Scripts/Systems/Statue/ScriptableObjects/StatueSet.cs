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
        [SerializeField] private StatueStatus status = StatueStatus.Unavailable;
        [SerializeField] private int _puzzleSegments = 0;
        [SerializeField] private int _demonSegments = 0;
        [SerializeField] private Sprite[] _statue = default;
        [SerializeField] private string _effectText = default;

        public int cost => _cost;
        public DemonName demon => _demon;
        public StatuePart[] parts => _parts;
        public bool available => (int)status > 0;
        public bool bought => (int)status > 1;
        public bool completed => (int)status > 2;
        public bool summoned => (int)status > 3;
        public int index { get; private set; }
        public StatuePart currentPart => parts[index];
        public int puzzleSegments => _puzzleSegments;
        public int demonSegments => _demonSegments;
        public Sprite[] statue => _statue;
        public string effectText => _effectText;

        public GameObject Instantiate(Vector3 _worldPos, Transform _parent)
        {
            GameObject statue = GameObject.Instantiate(statuePrefab, _worldPos, Quaternion.identity, _parent);
            statue.GetComponent<StatuePrefabManager>().Init(parts);
            index = 0;

            return statue;
        }

        public void Buy()
        {
            SetStatus(StatueStatus.Bought);
        }

        public void Break()
        {
            parts[0].Reset();
            parts[0].Buy();
            status = StatueStatus.Bought;
        }

        public void SetAvailable()
        {
            SetStatus(StatueStatus.Available);
        }


        public void SetSummoned()
        {
            SetStatus(StatueStatus.Resurrected);
        }
        private void SetStatus(StatueStatus _status)
        {
            if ((int)_status > (int)status) status = _status;
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

            bool tmp = completed;
            bool tmpCompleted = true;

            foreach (StatuePart part in parts)
            {
                if (!part.completion.isFull)
                {
                    tmpCompleted = false;
                    break;
                }
            }

            if (!tmp && tmpCompleted)
            {
                SetStatus(StatueStatus.Completed);
                _onSetCompleted();
            }
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
            SetStatus(StatueStatus.Available);

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].Load(_save.partsSave[i]);
            }
        }

        public StatueSetSave Save()
        {
            return new StatueSetSave(status, parts);
        }

        public void Reset()
        {
            status = StatueStatus.Unavailable;
            Debug.Log(_demon.ToString() + " status: " + status.ToString());

            foreach (StatuePart part in _parts)
            {
                part.Reset();
            }
        }
    }

    [System.Serializable]
    public struct StatueSetSave
    {
        public int status;
        public StatuePartSave[] partsSave;

        public StatueSetSave(StatueStatus _status, StatuePart[] _parts)
        {
            status = (int)_status;

            List<StatuePartSave> tmpSave = new List<StatuePartSave>();

            foreach (StatuePart part in _parts)
            {
                tmpSave.Add(part.Save());
            }

            partsSave = tmpSave.ToArray();
        }
    }
}
