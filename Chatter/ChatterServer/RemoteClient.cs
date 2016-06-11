using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatterServer
{
    class RemoteClient
    {
        private string name;
        public string Name{
            get { return name; }
        }

        private TcpClient client;
        public TcpClient Client{
            get { return client; }
        }

        private NetworkStream streamToClient;
        public NetworkStream StreamToClient {
            get { return streamToClient; }
        }

        private int BufferSize;
        public int Buffersize{
            get {return BufferSize;}
        }

        private byte[] buffer;
        public byte[] Buffer {
            get { return buffer; }
        }

        public RemoteClient(TcpClient client)
        {
            BufferSize = 8192;
            this.client = client;
            this.buffer = new byte[BufferSize];           

            //获取客户端接发流
            streamToClient = client.GetStream();

            //获取客户端注册名称
            readName(client);
        }

        public void readName(TcpClient client)
        {
            //获得流
            NetworkStream readStream = client.GetStream();

            //阻塞读注册名
            int bytesRead = readStream.Read(buffer, 0, BufferSize);
            this.name = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            Array.Clear(buffer, 0, buffer.Length);           //清空缓存，避免脏读
           
        }

        private void sendMsg(string msg, TcpClient client)
        {
            byte[] temp = Encoding.Unicode.GetBytes(msg);    //获得缓存数据流
            try
            {
                streamToClient.Write(temp, 0, temp.Length);  //发送数据流称到服务器
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
