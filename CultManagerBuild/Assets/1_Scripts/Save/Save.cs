using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [Serializable]
    public struct Save
    {
        public int savingSystemVersion;
        public DateTime dateTime;
        public ulong cultIdIndex;
        public Cultist[] cultists;
        public SystemRegistration[] roomsRegistrations;

        public Save(int _savingSystemVersion, CultData _data)
        {
            savingSystemVersion = _savingSystemVersion;
         
            dateTime = DateTime.Now;
            cultIdIndex = _data.idIndex;
            cultists = _data.cultists.ToArray();
            roomsRegistrations = _data.roomsRegistrations;
        }
    }
}