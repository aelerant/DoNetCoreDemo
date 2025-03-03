using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System;
using System.Net.Mime;
using System.Net;

namespace DoNetCoreDemo.Service
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailSettings = _config.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                emailSettings["SenderName"],
                emailSettings["UserName"]));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            // 支持纯文本或 HTML 格式
            message.Body = new TextPart("html") { Text = body };

#if true
            #region 添加附件

            var multipart = new Multipart("mixed");
            multipart.Add(new TextPart("plain") { Text = "正文内容" });

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(File.OpenRead("report.pdf")),
                ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName("report.pdf")
            };
            multipart.Add(attachment);

            message.Body = multipart;

            #endregion
#endif

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(
                emailSettings["SmtpServer"],
                int.Parse(emailSettings["Port"]),
                bool.Parse(emailSettings["UseSsl"]),
                cancellationToken: new CancellationTokenSource(30000).Token);//添加超时参数（单位：毫秒）

            // 认证（注意：Gmail 需开启"低安全性应用访问"或使用 OAuth2）
            await client.AuthenticateAsync(
                emailSettings["UserName"],
                emailSettings["Password"]);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }


        /// <summary>
        /// 使用using语句确保SmtpClient和Attachment资源释放
        /// 异步操作完成后自动释放网络连接
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <returns></returns>
        public async Task SendExcelToQQMailAsync(string excelFilePath)
        {
            // SMTP配置
            //发件人必须与SMTP认证邮箱一致
            using var smtpClient = new System.Net.Mail.SmtpClient("smtp.qq.com")
            {
                Port = 587, // 或 465                            
                Credentials = new NetworkCredential("1409609156@qq.com", "eppaybzqfsicggba"),//("your_qq@qq.com", "授权码"),
                EnableSsl = true
            };

            // 创建邮件对象
            using var mailMessage = new MailMessage
            {
                From = new MailAddress("1409609156@qq.com"),//("your_qq@qq.com")
                Subject = "Excel文件推送",
                Body = "请查收附件中的Excel文件",
                IsBodyHtml = false
            };

            // 添加收件人
            mailMessage.To.Add("1409609156@qq.com");

            // 添加Excel附件
            // QQ邮箱单附件最大50MB
            Attachment attachment = new Attachment(excelFilePath, MediaTypeNames.Application.Octet);
            mailMessage.Attachments.Add(attachment);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("邮件发送成功");
            }
            catch (SmtpException ex)
            {
                //单独捕获SMTP协议级异常（状态码400-599）
                Console.WriteLine($"SMTP错误：{ex.StatusCode} - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送失败：{ex.Message}");
            }

        }

    }
}
