using Simple_Async_Http_Server.Server.CustomSocket.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Simple_Async_Http_Server.Server.Common;
using System.Threading;

namespace Simple_Async_Http_Server.Server.CustomSocket
{
    public class SocketWrapper : ISocketWrapper
    {
        private readonly Socket client;
        private readonly ManualResetEvent sendDone;
        private readonly ManualResetEvent receiveDone;

        public SocketWrapper(Socket client)
        {
            CommonValidator.ThrowIfNull(client, nameof(client));

            this.client = client;
            this.sendDone = new ManualResetEvent(false);
            this.receiveDone = new ManualResetEvent(false);
        }

        public async Task SendAsync(ArraySegment<byte> buffer, SocketFlags flags)
        {
            var byteBuf = buffer.ToArray();
            await this.Send(byteBuf, flags);
        }

        public async Task<string> ReceiveAsync(SocketFlags flags)
        {
            return await this.Receive(flags);
        }
        
        public void ShutDown(SocketShutdown shutFlag)
        {
            this.client.Shutdown(shutFlag);
        }

        private async Task Send(byte[] byteData, SocketFlags flags)
        {
            // Begin sending the data to the remote device.  
            this.client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None,
                new AsyncCallback(SendCallback), client);

            sendDone.WaitOne();
        }
        private async void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                sendDone.Set();
            }
        }

        private async Task<string> Receive(SocketFlags flags)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = this.client;
                state.flags = flags;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);

                receiveDone.WaitOne();
                return state.textData.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return string.Empty;
            }
                       
        }
        private async void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.textData.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    //  Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, state.flags,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                { 
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                receiveDone.Set();
            }
        }
    }
}
