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
        [SerializeField] private GameManager gameManager = default;
        [SerializeField] private GameObject panel = default;
        [SerializeField] private SaveSettings save = default;
        [SerializeField] private DataRecorderSettings recorderData = default;
        [SerializeField, DrawScriptable] private DataSenderSettings settings = default;

        [ContextMenu("Send Data")]
        public void SendData()
        {
            if (File.Exists(recorderData.gamedataPath)) SendEmail("[TEST]GameData", "[TEST] Gamedata sent at" + System.DateTime.Now.ToString(), recorderData.gamedataPath);
            if (File.Exists(recorderData.gesturesPath)) SendEmail("[TEST]Gestures", "[TEST] Gestures sent at" + System.DateTime.Now.ToString(), recorderData.gesturesPath);
            if (File.Exists(recorderData.touchPath)) SendEmail("[TEST]Touches", "[TEST] Touches sent at" + System.DateTime.Now.ToString(), recorderData.touchPath);
        }

        public void SendAllData()
        {
            gameManager.SaveGame();

            string[] files = Directory.GetFiles(recorderData.folder);

            foreach (string file in files)
            {
                SendEmail("[" + recorderData.testerName + "] Session n°" + recorderData.currentSession, "File location: " + file, file);
            }

            SendSave();

            recorderData.currentSession++;

            if (panel) StartCoroutine(DisplayPanel());
        }

        public void SendSave()
        {
            SendEmail("[" + recorderData.testerName + "] Session n°" + recorderData.currentSession, "File location: " + save.dataPath, save.dataPath);
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

        private IEnumerator DisplayPanel()
        {
            panel.SetActive(true);

            yield return new WaitForSeconds(3.5f);

            panel.SetActive(false);
        }
    }
}