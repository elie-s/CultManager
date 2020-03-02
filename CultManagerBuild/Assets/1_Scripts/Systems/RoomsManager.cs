using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class RoomsManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private RecruitmentManager recruitment = default;
        [SerializeField] private NotificationsManager notificationsManager = default;
        [SerializeField] private CultData data = default;

        private void Update()
        {
            CheckScheduledRoomsActions();
        }

        private void CheckScheduledRoomsActions()
        {
            //for (int i = 0; i < data.roomsRegistrations.Length; i++)
            //{
            //    if(data.roomsRegistrations[i].hasPassed)
            //    {
                    
            //    }
            //}

            foreach (SystemRegistration systemRegistration in data.roomsRegistrations)
            {
                if (!systemRegistration.isEmpty && systemRegistration.hasPassed)
                {
                    debug.Log(systemRegistration + " has passed.", DebugInstance.Importance.Lesser);
                }
            }
        }

        [ContextMenu("Test Register Cultist")]
        public void TestRegisterCultist()
        {
            RegisterCultistsToRoom(Room.Recruitment, 1.0f/60.0f, data.cultists[0].id);
        }

        [ContextMenu("Test Unregister Room")]
        private void TestUnregisterRoom()
        {
            UnregisterCultistsFromRoom(Room.Recruitment);
        }

        public void RegisterCultistsToRoom(Room _room, float _duration, params ulong[] _cultistsID)
        {
            if(_room == Room.none)
            {
                debug.LogWarning("You can't register cultists to \'Room.none\'.", DebugInstance.Importance.Highest);
                return;
            }

            data.RegisterTo(_room,notificationsManager.SendNotificationIn(Mathf.RoundToInt(_duration * 60)), _duration, _cultistsID);
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