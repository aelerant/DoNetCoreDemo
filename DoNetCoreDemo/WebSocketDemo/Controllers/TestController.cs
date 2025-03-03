using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;
using System.Net.WebSockets;
using System.Text;

namespace WebSocketDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        // GET: api/<WebSocketController>
        [HttpGet("Requset")]
        public async Task<IActionResult> Resquest()
        {
            using (MiniProfiler.Current.Step("Web Stock request"))
            using (ClientWebSocket webSocket = new ClientWebSocket())
            {
                Uri uri = new Uri("ws://localhost:5046/ws");
                //创建连接
                await webSocket.ConnectAsync(uri, CancellationToken.None);
                Console.WriteLine("webSocker Connect Successfully!");

                //发送消息
                string sendMessage = "你好，我是WebSocket请求者！";
                var bytes = Encoding.UTF8.GetBytes(sendMessage);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine($"Send:{sendMessage}");

                //接收消息
                byte[] buffer = new byte[1024 * 4];
                await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var receiveMessage = Encoding.UTF8.GetString(buffer);
                Console.WriteLine($"Receive:{receiveMessage}");

                //关闭连接
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                Console.WriteLine("webSocket Close Successfully");
            }
            return Ok();
        }

        [HttpGet("Test")]
        public string Test()
        {
            return "Success";
        }

    }
}