using System;
using Unity.Notifications.Android;


namespace CultManager
{
    [Serializable]
    public struct Notification
    {
        public DateTime due;
        public int id;
        public bool cancelled { get; private set; }

        public Notification(DateTime _date, int _id)
        {
            due = _date;
            id = _id;
            cancelled = false;
        }

        public void Cancel()
        {
            if (!cancelled) AndroidNotificationCenter.CancelNotification(id);

            cancelled = true;
        }
    }
}