using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class RoomsManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private CultData data = default;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RegisterCultistsToRoom(Room _room, params Cultist[] _cultists)
        {
            if(_room == Room.none)
            {
                debug.LogWarning("You can't register cultists to \'Room.none\'.", DebugInstance.Importance.Highest);
                return;
            }

            data.roomsRegistrations[(int)_room] = new SystemRegistration(_cultists);
        }

        public void UnregisterCultistsFromRoom(Room _room)
        {
            if (_room == Room.none)
            {
                debug.LogWarning("You can't unregister cultists from \'Room.none\'.", DebugInstance.Importance.Highest);
                return;
            }

            data.roomsRegistrations[(int)_room] = SystemRegistration.Empty;
        }
    }
}