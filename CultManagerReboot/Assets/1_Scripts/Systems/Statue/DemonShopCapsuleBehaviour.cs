using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class DemonShopCapsuleBehaviour : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [Header("Settings")]
        [SerializeField] private DemonName[] demons = default;
        [SerializeField] private DemonName[] previousDemons = default;
        [Header("Components")]
        [SerializeField] private UISwitch[] demonsUI = default;
        [Header("Gameobjects")]
        [SerializeField] private GameObject locked = default;
        [SerializeField] private GameObject unlocked = default;
        [SerializeField] private GameObject[] buttonDemons = default;

        private void OnEnable()
        {
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            for (int i = 0; i < demons.Length; i++)
            {
                UpdateDemon(i);
            }

            Unlock();
        }

        private void UpdateDemon(int _index)
        {
            StatueSet set = data.GetStatueSet(demons[_index]);

            if (!set.bought) demonsUI[_index].SetA();
            else if(!set.completed) demonsUI[_index].SetB();
            else  demonsUI[_index].SetC();

            if(buttonDemons.Length > 0 )buttonDemons[_index]?.SetActive(set.bought);
        }

        private bool CheckUnlocked()
        {
            for (int i = 0; i < previousDemons.Length; i++)
            {
                if (!data.GetStatueSet((int)previousDemons[i]).completed) return false;
            }

            return true;
        }

        public void Unlock()
        {
            locked.SetActive(!CheckUnlocked());
            unlocked.SetActive(CheckUnlocked());
        }
    }

    [System.Serializable]
    public struct DemonBrotherhood
    {
        public DemonName[] brothers;

        public DemonBrotherhood(params DemonName[] _brothers)
        {
            brothers = _brothers;
        }

        public bool Completed(StatuesData _data)
        {
            foreach (DemonName demon in brothers)
            {
                if (!_data.GetStatueSet(demon).completed) return false;
            }

            return true;
        }

        public bool Contains(DemonName _demon)
        {
            for (int i = 0; i < brothers.Length; i++)
            {
                if (brothers[i] == _demon) return true;
            }

            return false;
        }
    }
}