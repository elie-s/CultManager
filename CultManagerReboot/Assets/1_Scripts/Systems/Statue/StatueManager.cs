using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class StatueManager : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [SerializeField] private Transform statueParent = default;

        
        private StatuePrefabManager statue;

        private void Start()
        {
            LoadStatue();
        }

        private void Update()
        {
            Build();
        }

        public void LoadStatue()
        {
            statue = data.currentStatueSet.Instantiate(statueParent.position, statueParent).GetComponent<StatuePrefabManager>();
        }

        public int AssignWorkers(int _amount, int _index)
        {
            return data.currentStatueSet.AssignWorkers(_amount, _index);
        }

        public int RemoveWorkers(int _amount, int _index)
        {
            return data.currentStatueSet.RemoveWorkers(_amount, _index);
        }

        public void Build()
        {
            if(data.SecondsFromTimeRef() > 1.0f)
            {
                data.currentStatueSet.UpdateCompletion(data.baseWorkForce * data.SecondsFromTimeRef());
                data.UpdateTimeRef();
                statue.UpdateAllPartsSprites();
            }
        }

        public void ResetData()
        {
            data.Reset();
        }
    }
}