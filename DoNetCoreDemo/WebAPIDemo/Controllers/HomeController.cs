using DoNetCoreDemo.Service;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly EmailService _emailService;

        public HomeController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("SendTestEmail")]
        public async Task<IActionResult> SendTestEmail()
        {
            try
            {
                await _emailService.SendEmailAsync(
                    "recipient@example.com",
                    "测试邮件主题",
                    "<h1>这是一封测试邮件</h1><p>来自 ASP.NET Core 应用</p>");

                return Content("邮件发送成功");
            }
            catch (SmtpCommandException ex)
            {
                return Content($"发送失败：{ex.Message} (状态码：{ex.StatusCode})");
            }
        }

        [HttpGet("SendQQEmail")]
        public async Task<IActionResult> SendQQEmail()
        {
            try
            {
                string path = "C:\\Users\\l1561\\Desktop\\lkpsbq.pdf";

                // 检验后缀名
                //var fileExtension = Path.GetExtension(file).ToLower();
                //if (!fileExtension.Equals(".xls") && !fileExtension.Equals(".xlsx"))
                //    return AjaxResult.GetErrorArgument(method, "file", "文件格式不正确");

                //校验文件是否存在
                if (!System.IO.File.Exists(path))
                    return Content("未找到文件");

                await _emailService.SendExcelToQQMailAsync(path);

                return Content("邮件发送成功");
            }
            catch (SmtpCommandException ex)
            {
                return Content($"发送失败：{ex.Message} (状态码：{ex.StatusCode})");
            }
        }

        [HttpGet("DeepSeekTest")]
        public async Task<IActionResult> DeepSeekTest()
        {
            try
            {
                await DeepSeekService.DeepSeekTestAsync();

                return Content("发送成功");
            }
            catch (Exception ex)
            {
                return Content($"{ex.Message}");
            }
        }
    }
}
