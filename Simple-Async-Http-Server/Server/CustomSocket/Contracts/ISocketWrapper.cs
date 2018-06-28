using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Async_Http_Server.Server.CustomSocket.Contracts
{
    public interface ISocketWrapper
    {
        Task SendAsync(ArraySegment<byte> buffer, SocketFlags flags);

        Task<string> ReceiveAsync(SocketFlags flags);

        void ShutDown(SocketShutdown shutFlag);
    }
}
