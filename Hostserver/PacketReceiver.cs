using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Hostserver
{
    public class PacketReceiver
    {
        private byte[] _buffer;
        private Socket _receiveSocket;
        private int Id {get; set;}

        public PacketReceiver(Socket socket, int Id)
        {
            _receiveSocket = socket;
            this.Id = Id;
        }

        public void StartReceiving()
        {
            try
            {
                _buffer = new byte[4];
                _receiveSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if(_receiveSocket.EndReceive(ar)>1)
                {
                    _buffer = new byte[BitConverter.ToInt32(_buffer,0)];
                    _receiveSocket.Receive(_buffer, _buffer.Length, SocketFlags.None);
                    //everything is received, now we convert the data:
                    string data = Encoding.Default.GetString(_buffer);
                    Console.WriteLine(data);
                }
                else
                {
                    Disconnect();
                }
            }
            catch
            {
                if(!_receiveSocket.Connected)
                {
                    Disconnect();
                }
                else
                    StartReceiving();
            }
        }

        private void Disconnect()
        {
            _receiveSocket.Disconnect(true);
            //TODO remove attached client from the client controller
        }
    }
}