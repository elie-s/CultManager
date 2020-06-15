using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Playtests/Sender Settings")]
    public class DataSenderSettings : ScriptableObject
    {
        public string smtpClient = "smtp.gmail.com";
        public string sendingAdress = "data.demonsfordummies@gmail.com";
        public string password = "CEJMPS20";
        public string receivingAdress = "e.sarfati@rubika-edu.com";
        public string subject = "Test Subject";
        public string body = "Test body";
    }
}
