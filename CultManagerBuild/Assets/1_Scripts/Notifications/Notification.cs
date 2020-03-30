using System;
#if UNITY_ANDROID || UNITY_EDITOR
using Unity.Notifications.Android;
#endif


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
#if UNITY_ANDROID || UNITY_EDITOR
            if (!cancelled) AndroidNotificationCenter.CancelNotification(id);
#endif
            cancelled = true;
        }
    }
}