using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Text;

namespace Vera.Infrastructure
{
            public class EmailService
            {
            internal async Task SendEmailAsync(string? email, string? subject, string? message) {

                    //string path = "~/Content/Upload/" + Guid.NewGuid() + "/";

                    //var path_file = Path.GetFileName(path);

                    // отправитель - устанавливаем адрес и отображаемое в письме имя
                    MailAddress? from = new("serega-a-02@yandex.ru", "Администрация сервиса дум.su/vera");

                    // кому отправляем
                    MailAddress? to = email != null ? new MailAddress(email) : new MailAddress("s_antonov02@rambler.ru");

            // создаем объект сообщения
            using MailMessage m = new(from, to);

            //К письму мы можем прикрепить вложения с помощью свойства Attachments. Каждое вложение представляет объект System.Net.Mail.Attachment:
            //if (path != null) m.Attachments.Add(new Attachment(path));
            m.BodyEncoding = Encoding.UTF8;
            m.SubjectEncoding = Encoding.UTF8;
            m.IsBodyHtml = true;

            m.Subject = subject;

            m.Body = message;

            using (var client = new SmtpClient("smtp.yandex.ru", 587))
            {

                client.EnableSsl = true;

                client.Credentials = new NetworkCredential("serega-a-02@yandex.ru", "yandexsergy7");

                await client.SendMailAsync(m);

            }
            m.Dispose();// 06/08/23

            email = null;
            subject = null;
            message = null;
            //path = null;
            from = null;
            to = null;
        }
        }
}