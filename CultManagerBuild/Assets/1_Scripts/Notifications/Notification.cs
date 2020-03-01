using System;
using Unity.Notifications.Android;


namespace CultManager
{
    [Serializable]
    public struct Notification
    {
        public DateTime due;
        public int id;

        public Notification(DateTime _date, int _id)
        {
            due = _date;
            id = _id;
        }

        public void Cancel()
        {
            AndroidNotificationCenter.CancelNotification(id);
        }
    }
}