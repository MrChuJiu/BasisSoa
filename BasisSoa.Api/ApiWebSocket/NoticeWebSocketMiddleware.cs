using BasisSoa.Api.Jwt;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasisSoa.Api.ApiWebSocket
{
    public class NoticeWebSocketMiddleware
    {
        private WebSocket socket = null;
        //创建链接
        public async Task Connect(HttpContext context, Func<Task> n)
        {
            try
            {
                //执行接收
                WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();
                this.socket = socket;
                //执行监听
                await EchoLoop();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 响应处理
        /// </summary>
        /// <returns></returns>
        async Task EchoLoop()
        {
            var buffer = new byte[1024];
            var seg = new ArraySegment<byte>(buffer);
            while (this.socket.State == WebSocketState.Open)
            {
                var incoming = await this.socket.ReceiveAsync(seg, CancellationToken.None);

                byte[] backInfo = System.Text.UTF8Encoding.Default.GetBytes("服务端相应内容");
                var outgoing = new ArraySegment<byte>(backInfo, 0, incoming.Count);
                await this.socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }



        //private static ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> _sockets = new ConcurrentDictionary<string, System.Net.WebSockets.WebSocket>();

        //private readonly RequestDelegate _next;
        //public NoticeWebSocketMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}
        //public async Task Invoke(HttpContext context)
        //{
        //    if (!context.WebSockets.IsWebSocketRequest)
        //    {
        //        await _next.Invoke(context);
        //        return;
        //    }
        //    System.Net.WebSockets.WebSocket dummy;

        //    CancellationToken ct = context.RequestAborted;
        //    var currentSocket = await context.WebSockets.AcceptWebSocketAsync();
        //    //大哥你点啥了  F1 a  ha?
        //    //currentSocket.ReceiveAsync
        //    //string socketId = Guid.NewGuid().ToString();
        //    TokenModelBeta tokenModel = JwtToken.SerializeJWT(context.Request.Query["Authorization"].ToString());
           
        //    if (!_sockets.ContainsKey(tokenModel.Id))
        //    {
        //        _sockets.TryAdd(tokenModel.Id, currentSocket);
        //    }
        //    //_sockets.TryRemove(socketId, out dummy);
        //    //_sockets.TryAdd(socketId, currentSocket);

        //    while (true)
        //    {
        //        if (ct.IsCancellationRequested)
        //        {
        //            break;
        //        }

        //        string response = await ReceiveStringAsync(currentSocket, ct);
        //        NoticetemplateModule msg = JsonConvert.DeserializeObject<NoticetemplateModule>(response);

        //        if (string.IsNullOrEmpty(response))
        //        {
        //            if (currentSocket.State != WebSocketState.Open)
        //            {
        //                break;
        //            }

        //            continue;
        //        }

        //        foreach (var socket in _sockets)
        //        {
        //            if (socket.Value.State != WebSocketState.Open)
        //            {
        //                continue;
        //            }
        //            if (socket.Key == msg.RecriverId || socket.Key == tokenModel.Id)
        //            {
        //                await SendStringAsync(socket.Value, JsonConvert.SerializeObject(msg), ct);
        //            }
        //        }
        //    }


        //    await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
        //    currentSocket.Dispose();
        //}

        //private static Task SendStringAsync(System.Net.WebSockets.WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        //{
        //    var buffer = Encoding.UTF8.GetBytes(data);
        //    var segment = new ArraySegment<byte>(buffer);
        //    return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        //}

        //private static async Task<string> ReceiveStringAsync(System.Net.WebSockets.WebSocket socket, CancellationToken ct = default(CancellationToken))
        //{
        //    var buffer = new ArraySegment<byte>(new byte[8192]);
        //    using (var ms = new MemoryStream())
        //    {
        //        WebSocketReceiveResult result;
        //        do
        //        {
        //            ct.ThrowIfCancellationRequested();

        //            result = await socket.ReceiveAsync(buffer, ct);
        //            ms.Write(buffer.Array, buffer.Offset, result.Count);
        //        }
        //        while (!result.EndOfMessage);

        //        ms.Seek(0, SeekOrigin.Begin);
        //        if (result.MessageType != WebSocketMessageType.Text)
        //        {
        //            return null;
        //        }

        //        using (var reader = new StreamReader(ms, Encoding.UTF8))
        //        {
        //            return await reader.ReadToEndAsync();
        //        }
        //    }
        //}

}
