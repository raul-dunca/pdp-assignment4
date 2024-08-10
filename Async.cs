/*using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Program
    {
        public static async Task Main()
        {
            var entry = Dns.GetHostEntry(State.Host);
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            var endpoint = new IPEndPoint(entry.AddressList[0], State.Port);
            var state = new State(socket);

            await Connect(state.Socket, endpoint, state);
            await ConnectCallback(state);

            //state.ReceiveDone.WaitOne();
            state.Socket.Close();
        }

        static Task<int> Connect(Socket conn, IPEndPoint endpoint, State state)
        {
            TaskCompletionSource<int> promise = new TaskCompletionSource<int>();
            conn.BeginConnect(endpoint, (IAsyncResult ar) =>
            {
                try
                {
                    conn.EndConnect(ar);
                    promise.SetResult(0); // Set result to 0 upon successful connection
                }
                catch (Exception ex)
                {
                    promise.SetException(ex); // Set an exception if the connection fails
                }
            },
                state);
            return promise.Task;
        }

        private static async Task ConnectCallback(State ar)
        {
            var state = ar;
            state.ConnectDone.Set();
            var requestText = $"GET /documente-utile/ HTTP/1.1\r\nHost: {State.Host}\r\n\r\n";
            var requestBytes = Encoding.UTF8.GetBytes(requestText);

            await Send(state.Socket, requestBytes, 0, requestBytes.Length, state);
            await SendCallback(state);

        }

        static Task<int> Send(Socket conn, byte[] buf, int index, int count, State state)
        {
            TaskCompletionSource<int> promise = new TaskCompletionSource<int>();
            conn.BeginSend(buf, index, count, SocketFlags.None,
                (IAsyncResult ar) => promise.SetResult(conn.EndSend(ar)),
                state);
            return promise.Task;
        }

        private static async Task SendCallback(State ar)
        {
            var state = ar;
            state.SendDone.Set();

            await ReceiveCallback(state);
        }

        static Task<int> Receive(Socket conn, byte[] buf, int index, int count, State s)
        {
            TaskCompletionSource<int> promise = new TaskCompletionSource<int>();
            conn.BeginReceive(buf, index, count, SocketFlags.None,
                (IAsyncResult ar) => promise.SetResult(conn.EndReceive(ar)),
                s);
            return promise.Task;
        }
        private async static Task ReceiveCallback(State s)
        {
            var state = s;
            var bytesReceived = await Receive(state.Socket, state.Buffer, 0, State.BufferLength, state);
            if (bytesReceived == 0)
            {
                Console.WriteLine(state.Content.ToString());
                state.ReceiveDone.Set();
            }
            else
            {
                var responseText = Encoding.UTF8.GetString(state.Buffer, 0, bytesReceived);
                state.Content.Append(responseText);

                await ReceiveCallback(state);
            }
        }

        public sealed class State
        {
            public const string Host = "www.cnatdcu.ro";
            public const int Port = 80;
            public const int BufferLength = 1024;
            public readonly byte[] Buffer = new byte[BufferLength];
            public readonly ManualResetEvent ConnectDone = new ManualResetEvent(false);
            public readonly StringBuilder Content = new StringBuilder();
            public readonly ManualResetEvent ReceiveDone = new ManualResetEvent(false);
            public readonly ManualResetEvent SendDone = new ManualResetEvent(false);
            public readonly Socket Socket;

            public State(Socket socket)
            {
                Socket = socket;
            }
        }
    }
}
*/