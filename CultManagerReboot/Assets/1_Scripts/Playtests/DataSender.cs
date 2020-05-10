using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.IO;
using UnityEngine;

namespace CultManager
{
    public class DataSender : MonoBehaviour
    {
        [SerializeField] private DataRecorderSettings recorderSata = default;
        [SerializeField, DrawScriptable] private DataSenderSettings settings = default;

        [ContextMenu("Send Data")]
        public void SendData()
        {
            if (File.Exists(recorderSata.gamedataPath)) SendEmail("[TEST]GameData", "[TEST] Gamedata sent at" + System.DateTime.Now.ToString(), recorderSata.gamedataPath);
            if (File.Exists(recorderSata.gesturesPath)) SendEmail("[TEST]Gestures", "[TEST] Gestures sent at" + System.DateTime.Now.ToString(), recorderSata.gesturesPath);
            if (File.Exists(recorderSata.touchPath)) SendEmail("[TEST]Touches", "[TEST] Touches sent at" + System.DateTime.Now.ToString(), recorderSata.touchPath);
        }

        
        private void SendEmail(string _subject, string _body, string _attachmentPath)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(settings.smtpClient);
            mail.From = new MailAddress(settings.sendingAdress);
            mail.To.Add(settings.receivingAdress);
            mail.Subject = _subject;
            mail.Body = _body;
            mail.Attachments.Add(Attach(_attachmentPath));

            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential(settings.sendingAdress, settings.password) as ICredentialsByHost;
            SmtpServer.EnableSsl = true;
            SmtpServer.Timeout = 20000;
            SmtpServer.UseDefaultCredentials = false;

            try
            {
                SmtpServer.Send(mail);
            }
            catch (SmtpException myEx)
            {
                Debug.LogError("Expection: \n" + myEx.ToString());
            }
        }

        private Attachment Attach(string _path)
        {
            return new Attachment(_path);
        }
    }
}