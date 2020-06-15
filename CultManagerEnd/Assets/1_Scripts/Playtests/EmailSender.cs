using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UnityEngine;

namespace CultManager
{
    public class EmailSender : MonoBehaviour
    {
        [SerializeField] private SaveSettings save = default;
        [SerializeField] private string test = "";

        private string dataPath => Application.persistentDataPath + "/" + save.saveFolder + "/" + save.saveName + "." + save.saveExtension + save.version;

        [ContextMenu("Test")]
        private void PrepareEmail()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("eliesarfati675@gmail.com");
            mail.To.Add("e.sarfati@rubika-edu.com");
            mail.Subject = "Test Attachement";
            mail.Body = "test attachement";
            mail.Attachments.Add(Attach(test == "" ? dataPath : test));
    
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("data.demonsfordummies@gmail.com", "CEJMPS20") as ICredentialsByHost;
            SmtpServer.EnableSsl = true;
            SmtpServer.Timeout = 20000;
            SmtpServer.UseDefaultCredentials = false;
            try
            {
                SmtpServer.Send(mail);
            }
            catch (SmtpException myEx)
            {
                Debug.Log("Expection: \n" + myEx.ToString());
            }
        }

        private Attachment Attach(string _path)
        {
            return new Attachment(_path);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause) Debug.Log("paused");
            else Debug.Log("Unpaused");
        }
    }
}