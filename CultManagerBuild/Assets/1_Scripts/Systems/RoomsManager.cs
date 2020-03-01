using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class RoomsManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private RecruitmentManager recruitment = default;
        [SerializeField] private CultData data = default;

        [ContextMenu("Test Register Cultist")]
        private void TestRegisterCultist()
        {
            RegisterCultistsToRoom(Room.Recruitment, data.cultists[0].id);
        }

        [ContextMenu("Test Unregister Room")]
        private void TestUnregisterRoom()
        {
            UnregisterCultistsFromRoom(Room.Recruitment);
        }

        public void RegisterCultistsToRoom(Room _room, params ulong[] _cultistsID)
        {
            if(_room == Room.none)
            {
                debug.LogWarning("You can't register cultists to \'Room.none\'.", DebugInstance.Importance.Highest);
                return;
            }

            data.RegisterTo(_room, _cultistsID);
        }

        public void UnregisterCultistsFromRoom(Room _room)
        {
            if (_room == Room.none)
            {
                debug.LogWarning("You can't unregister cultists from \'Room.none\'.", DebugInstance.Importance.Highest);
                return;
            }

            data.Unregister(_room);
        }
    }
}