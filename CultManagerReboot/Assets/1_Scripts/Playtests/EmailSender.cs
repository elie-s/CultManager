using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class EmailSender : MonoBehaviour
{
    [ContextMenu("Test")]
    private void PrepareEmail()
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress("eliesarfati675@gmail.com");
        mail.To.Add("e.sarfati@rubika-edu.com");
        mail.Subject = "Test";
        mail.Body = "test body";

        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.Port = 587;
        SmtpServer.Credentials = new NetworkCredential("eliesarfati675@gmail.com", "59itafraS") as ICredentialsByHost;
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
}
