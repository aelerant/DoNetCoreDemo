using System.Net.WebSockets;
using System.Text;
using WebSocketDemo.SocketsManager;

namespace WebSocketDemo.Handlers
{
    /// <summary>
    /// 4.创建 WebSocket 管理子类
    /// 可以创建多个，用于个性化设置，主要是上面设置了接收的抽象方法，所以必须要重写 Receive 方法。如果不需要的话，其实把基类的抽象去掉，直接在基类中写也可以。
    /// </summary>
    public class WebSocketMessageHandler : SocketsHandler
    {
        public WebSocketMessageHandler(SocketsManager.SocketsManager sockets) : base(sockets)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketId = Sockets.GetId(socket);
            await SendMessageToAll($"{socketId}已加入");
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            await base.OnDisconnected(socket);
            var socketId = Sockets.GetId(socket);
            await SendMessageToAll($"{socketId}离开了");
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = Sockets.GetId(socket);
            var message = $"{socketId} 发送了消息：{Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendMessageToAll(message);
        }
    }
}
