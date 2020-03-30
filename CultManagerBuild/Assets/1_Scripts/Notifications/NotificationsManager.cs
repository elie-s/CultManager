using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID || UNITY_EDITOR
using Unity.Notifications.Android;
#endif

namespace CultManager
{
    public class NotificationsManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [Header("Channel Settings")]
        [SerializeField] private string channelID = default;
        [SerializeField] private string channelName = default;
#if UNITY_ANDROID || UNITY_EDITOR
        [SerializeField] private Importance channelImportance = default;
#endif
        [SerializeField] private string channelDescription = default;
        [Header("Test Notification Settings")]
        [SerializeField] private string notificationTitle = default;
        [SerializeField] private string notificationText = default;

        private void Awake()
        {
            CreateChannel();
        }

        private void CreateChannel()
        {
#if UNITY_ANDROID || UNITY_EDITOR
            AndroidNotificationChannel channel = new AndroidNotificationChannel()
            {
                Id = channelID,
                Name = channelName,
                Importance = channelImportance,
                Description = channelDescription,
            };

            AndroidNotificationCenter.RegisterNotificationChannel(channel);
            debug.Log("Channel: \"" + channelName + "\" (" + channelID + ") registered.", DebugInstance.Importance.Mandatory);
#endif
        }

        public Notification SendNotificationIn(int _minutes)
        {
            int id = 0;
#if UNITY_ANDROID || UNITY_EDITOR
            AndroidNotification notification = new AndroidNotification();

            notification.Title = notificationTitle;
            notification.Text = notificationText;
            notification.FireTime = System.DateTime.Now.AddMinutes(_minutes);
            notification.Style = NotificationStyle.BigTextStyle;

            id = AndroidNotificationCenter.SendNotification(notification, channelID);
            debug.Log("Notification: \"" + notificationTitle + "\" ("+ id + ") registered on channel: " + channelID + ". Notification is scheduled at " + System.DateTime.Now.AddMinutes(_minutes), DebugInstance.Importance.Mandatory);
#endif
            return new Notification(System.DateTime.Now.AddMinutes(_minutes), id);
        }
    }
}