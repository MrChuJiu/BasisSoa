using BasisSoa.Api.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasisSoa.Api.ApiWebSocket
{
    /// <summary>
    /// WebSocket服务
    /// </summary>
    public class NoticeHandler
    {
        //存储链接在上面的用户
        private static ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> _sockets = new ConcurrentDictionary<string, System.Net.WebSockets.WebSocket>();
        //存储用户发来的消息
        private static ConcurrentDictionary<string,List<NoticetemplateModule>> _socketMessages = new ConcurrentDictionary<string, List<NoticetemplateModule>>();

        private static WebSocket socket;

        static async Task Acceptor(HttpContext context, Func<Task> n)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;
            CancellationToken ct = context.RequestAborted;
            socket = await context.WebSockets.AcceptWebSocketAsync();

            TokenModelBeta tokenModel = JwtToken.SerializeJWT(context.Request.Query["Authorization"].ToString());
            //处理用户连接
            //添加Socket进集合 存在就取出 不存在就加入
            if (!_sockets.ContainsKey(tokenModel.Id))
            {
                 _sockets.TryAdd(tokenModel.Id, socket);
            }
            else {
                if (_sockets[tokenModel.Id] != socket) {
                    _sockets[tokenModel.Id] = socket;
                }
            }

            //处理消息
            await EchoLoop(tokenModel.Id,ct);

        }

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <returns></returns>
        static async Task EchoLoop(string userId, CancellationToken ct)
        {
            List<NoticetemplateModule> dummy;
            System.Net.WebSockets.WebSocket socketdummy;
            //处理消息
            if (_socketMessages.ContainsKey(userId))
            {
                List<NoticetemplateModule> msgs = _socketMessages[userId];
                foreach (NoticetemplateModule item in msgs)
                {
                    await SendStringAsync(socket, JsonConvert.SerializeObject(item));
                    _socketMessages.TryRemove(userId, out dummy);
                }
               
            }

            //循环监听
            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                //处理数据
                string response = await ReceiveStringAsync(socket, ct);
                NoticetemplateModule msg = JsonConvert.DeserializeObject<NoticetemplateModule>(response);
                msg.CreateTime = DateTime.Now;

                //是否有这个用户的消息池
                if (!_socketMessages.ContainsKey(userId))
                {
                    _socketMessages.TryAdd(userId, new List<NoticetemplateModule>());
                }
                //消息池加数据
                _socketMessages[userId].Add(msg);


               
              
                //不停地去跑循环
                foreach (var socket in _sockets)
                {
                    //移除
                    if (socket.Value.State != WebSocketState.Open)
                    {
                        if (_sockets.ContainsKey(userId))
                        {
                            _sockets.TryRemove(userId, out socketdummy);
                        }
                        break;
                    }
                    if (socket.Key == msg.RecriverId || socket.Key == userId)
                    {
                        await SendStringAsync(socket.Value, JsonConvert.SerializeObject(msg), ct);
                    }
                }

            }



        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private static Task SendStringAsync(System.Net.WebSockets.WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }

        /// <summary>
        /// 处理收到的数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private static async Task<string> ReceiveStringAsync(System.Net.WebSockets.WebSocket socket, CancellationToken ct = default(CancellationToken))
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    ct.ThrowIfCancellationRequested();

                    result = await socket.ReceiveAsync(buffer, ct);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return null;
                }

                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        /// <summary>
        /// 路由绑定处理
        /// </summary>
        /// <param name="app"></param>
        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(NoticeHandler.Acceptor);
        }
    }
}
