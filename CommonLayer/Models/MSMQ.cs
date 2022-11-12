using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;

namespace CommonLayer.Models
{
    public class MSMQ
    {
        MessageQueue message = new MessageQueue();
        public void sendData2Queue(string token)
        {
            message.Path = @".\private$\Token";
            if(!MessageQueue.Exists(message.Path))
{
                //Exists
                MessageQueue.Create(message.Path);

            }
            else
{
                // Creates the new queue named "Bills"
                message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                message.ReceiveCompleted += Message_ReceiveCompleted;
                message.Send(token);
                message.BeginReceive();
                message.Close();
            }

            
        }

        private void Message_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = message.EndReceive(e.AsyncResult);
                var token = msg.Body.ToString();
                string subject = "Forget Password Token";
                string body = token;
                var SMPTClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("userdummy1719@gmail.com", "tidmjcsdlzmctzjg"),
                    EnableSsl = true,
                };
                SMPTClient.Send("userdummy1719@gmail.com", "userdummy1719@gmail.com", subject, body);
                message.BeginReceive();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
