using System;

namespace CultManager
{
    [Serializable]
    public struct SystemRegistration : IEquatable<SystemRegistration>
    {
        public DateTime date;
        public ulong[] cultistsID;

        public bool isEmpty => Equals(Empty);

        public static SystemRegistration Empty => new SystemRegistration() { date = DateTime.MinValue, cultistsID = null };

        public SystemRegistration(params Cultist[] _cultists)
        {
            date = DateTime.Now;
            cultistsID = new ulong[_cultists.Length];

            for (int i = 0; i < _cultists.Length; i++)
            {
                cultistsID[i] = _cultists[i].id;
            }
        }

        public SystemRegistration(params ulong[] _ids)
        {
            date = DateTime.Now;
            cultistsID = _ids;
        }

        public SystemRegistration(DateTime _date, params ulong[] _ids)
        {
            date = _date;
            cultistsID = _ids;
        }

        public bool Equals(SystemRegistration other)
        {
            return date == other.date && cultistsID == other.cultistsID;
        }
    }
}