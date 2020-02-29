using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Cult/Data")]
    public class CultData : ScriptableObject
    {
        [SerializeField] private DebugInstance debug = default;
        public SystemRegistration[] roomsRegistrations;
        public List<Cultist> cultists { get; private set; }
        public ulong idIndex { get; private set; }

        public void Reset()
        {
            cultists = new List<Cultist>();

            roomsRegistrations = new SystemRegistration[8];
            for (int i = 0; i < roomsRegistrations.Length; i++)
            {
                roomsRegistrations[i] = SystemRegistration.Empty;
            }

            debug.Log("Cult's data reset.", DebugInstance.Importance.Highest);
        }

        public void Init()
        {
            idIndex = 0;
            Reset();
        }

        public void LoadSave(Save _save)
        {
            cultists = _save.cultists.ToList();
            roomsRegistrations = _save.roomsRegistrations;
            idIndex = _save.cultIdIndex;

            debug.Log("Cult's data load from save.", DebugInstance.Importance.Highest);
        }

        public void AddCultist(Cultist _cultist)
        {
            cultists.Add(_cultist);
            debug.Log(_cultist + " added to the Cult's list.", DebugInstance.Importance.Lesser);
        }

        public void RemoveCultist(Cultist _cultist)
        {
            cultists.Remove(_cultist);
            debug.Log(_cultist + " removed from the Cult's list.", DebugInstance.Importance.Lesser);
        }

        public Cultist GetCultist(ulong _id)
        {
            for (int i = 0; i < cultists.Count; i++)
            {
                if (cultists[i].id == _id) return cultists[i];
            }

            return null;
        }

        public Cultist CreateCultist(string _name, int _sprite)
        {
            Cultist result = new Cultist(idIndex, _name, _sprite);
            idIndex++;

            debug.Log(result + " created.", DebugInstance.Importance.Lesser);

            return result;
        }
    }
}