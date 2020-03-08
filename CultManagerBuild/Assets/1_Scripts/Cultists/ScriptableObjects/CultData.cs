using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Cult/Data")]
    public class CultData : ScriptableObject
    {
        [SerializeField] private DebugInstance debug = default;

        public SystemRegistration[] roomsRegistrations { get; private set; }
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

        public void RegisterTo(Room _room, Notification _notification, float _duration, ulong[] _ids)
        {
            roomsRegistrations[(int)_room] = new SystemRegistration(_notification, _duration, _ids);

            for (int i = 0; i < _ids.Length; i++)
            {
                GetCultist(_ids[i]).SetToRoom(_room);
                debug.Log(GetCultist(_ids[i]) + " registered to the room " + GetCultist(_ids[i]).room + ".", DebugInstance.Importance.Lesser);
            }
        }

        public void Unregister(Room _room)
        {
            if(roomsRegistrations[(int)_room].cultistsID == null || roomsRegistrations[(int)_room].cultistsID.Length == 0)
            {
                debug.Log("There is no cultist to unregister from " + _room + ".", DebugInstance.Importance.Lesser);
            }

            for (int i = 0; i < roomsRegistrations[(int)_room].cultistsID.Length; i++)
            {
                GetCultist(roomsRegistrations[(int)_room].cultistsID[i]).SetToRoom(Room.none);
                debug.Log(GetCultist(roomsRegistrations[(int)_room].cultistsID[i]) + " unregistered from the room " + _room + ".", DebugInstance.Importance.Lesser);
            }

            roomsRegistrations[(int)_room].notification.Cancel();
            roomsRegistrations[(int)_room] = SystemRegistration.Empty;
        }

        public void Unregister(ulong[] _ids)
        {

        }
    }
}