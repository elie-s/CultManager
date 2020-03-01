using System;
using UnityEngine;

namespace CultManager
{
    [Serializable]
    public struct SystemRegistration : IEquatable<SystemRegistration>
    {
        public DateTime startDate;
        public DateTime endDate;
        public ulong[] cultistsID;
        public Notification notification;
        private bool empty;

        public bool isEmpty => empty;
        public bool hasPassed => endDate < DateTime.Now;

        public static SystemRegistration Empty => new SystemRegistration() { startDate = DateTime.MinValue, endDate = DateTime.MaxValue, cultistsID = new ulong[0], empty = true };

        public SystemRegistration(Notification _notification, float _duration, params Cultist[] _cultists)
        {
            startDate = DateTime.Now;
            endDate = DateTime.Now.AddHours(_duration);
            cultistsID = new ulong[_cultists.Length];
            notification = _notification;
            empty = false;

            for (int i = 0; i < _cultists.Length; i++)
            {
                cultistsID[i] = _cultists[i].id;
            }
        }

        public SystemRegistration(Notification _notification, float _duration, params ulong[] _ids)
        {
            startDate = DateTime.Now;
            endDate = DateTime.Now.AddHours(_duration);
            cultistsID = _ids;
            notification = _notification;

            empty = false;
        }

        public SystemRegistration(Notification _notification, DateTime _date, float _duration, params ulong[] _ids)
        {
            startDate = _date;
            endDate = startDate.AddHours(_duration);
            cultistsID = _ids;
            notification = _notification;

            empty = false;
        }

        public bool Equals(SystemRegistration other)
        {
            return startDate == other.startDate && endDate == other.endDate && cultistsID.Length == other.cultistsID.Length;
        }

        public override string ToString()
        {
            string result = startDate.ToString() + " -> " + endDate.ToString();

            foreach (ulong id in cultistsID)
            {
                result+= "\nCultist #"+id;
            }

            return result;
        }
    }
}