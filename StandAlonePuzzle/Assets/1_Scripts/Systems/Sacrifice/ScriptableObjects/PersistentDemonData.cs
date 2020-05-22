using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Demon/PersistentData")]
    public class PersistentDemonData : ScriptableObject
    {
        public List<PersistentDemon> persistentDemons;
        public int idIndex;

        public void Reset()
        {
            persistentDemons = new List<PersistentDemon>();
            idIndex = 0;
        }

        public void LoadSave(Save _save)
        {
            persistentDemons = _save.persistentDemons.ToList();
            idIndex = _save.persistentdemonIdIndex;
        }

        public PersistentDemon CreatePersistentDemon(int spriteIndex)
        {
            PersistentDemon current = new PersistentDemon(idIndex, spriteIndex);
            AddPersistentDemon(current);

            idIndex++;

            return current;
        }

        public void RemovePersistentDemon(PersistentDemon persistentDemon)
        {
            persistentDemons.Remove(persistentDemon);
        }

        void AddPersistentDemon(PersistentDemon persistentDemon)
        {
            persistentDemons.Add(persistentDemon);
        }
    }
}

