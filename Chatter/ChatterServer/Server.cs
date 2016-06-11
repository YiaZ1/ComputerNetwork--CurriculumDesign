using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatterServer
{
    class Server
    {
        private Dictionary<String, RemoteClient> clientList;

        private TcpListener listener;

        public delegate void AysnRnSDelegate(RemoteClient wapper);        //建立新委托

        public Server()
        {
            clientList = new Dictionary<String, RemoteClient>();

            IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
            listener = new TcpListener(ip, 6666);

            listener.Start();           // 开始侦听
            Console.WriteLine("Start Listening ...");
        }

        public void start()
        {
            while (true)
            {
                // 获取一个连接，同步方法，在此处中断
                TcpClient client = listener.AcceptTcpClient();
                RemoteClient wapper = new RemoteClient(client);

                //添加到客户端列表中,如果未注册即注册并开始异步接收数据
                if (addList(wapper))
                {
                    //异步读取并转发数据
                    BeginRnS(wapper);            
                }
            }
        }

        private void RnS(RemoteClient wapper)
        {
            int count;
            while (IsOnline(wapper.Client))
            {
                count = 0;

                //获得流
                NetworkStream clientStream = wapper.StreamToClient;

                string msg = "";

                //阻塞读数据
                if (clientStream.DataAvailable)
                {
                    count = clientStream.Read(wapper.Buffer, 0, wapper.Buffersize);
                    Console.WriteLine("已接收到{0}字节", count);         //debug
                    msg = Encoding.Unicode.GetString(wapper.Buffer, 0, count);
                    if (IsDisconnectMsg(msg))
                        continue;
                    Array.Clear(wapper.Buffer, 0, wapper.Buffer.Length);           //清空缓存，避免脏读
                }

                //阻塞发送数据
                if (msg != "")
                {
                    foreach (RemoteClient rc in clientList.Values)
                    {
                        if (rc.Name != wapper.Name)
                        {
                            if (IsOnline(rc.Client))
                            {
                                sendMsg(rc, wapper.Name + "：" + msg + "\r\n");
                            }
                            else                                         //如果连接已经不存在，则删除列表中的客户端
                            {
                                //Console.WriteLine("{0} 已下线", rc.Name);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("{0} RnS已停止！", wapper.Name);   //debug
        }
      
        private void RnSed(IAsyncResult result)
        {
            AysnRnSDelegate aysnDelegate = result.AsyncState as AysnRnSDelegate;
        }
       
        private IAsyncResult BeginRnS(RemoteClient wapper)
        {
            string fileStr = string.Empty;
            AysnRnSDelegate RnSDelegate = new AysnRnSDelegate(RnS);
            return RnSDelegate.BeginInvoke(wapper, RnSed, RnSDelegate);
        }

        private bool addList(RemoteClient rc)
        {
            if (clientList.ContainsKey(rc.Name) == false)   //名字未被注册，添加到客户端列表
            {
                sendMsg(rc, "true");
                clientList.Add(rc.Name, rc);
                Console.WriteLine("注册成功！ {0} 已上线", rc.Name);
                System.Threading.Thread.Sleep(1000);
                refreshList();
                return true;
            }
            else
            {
                sendMsg(rc, "false");                       //已被注册，返回错误信息
                Console.WriteLine(" {0} 名称已被注册，注册失败！", rc.Name);
                return false;
            }   
        }

        private void sendMsg(RemoteClient rc, string msg)
        {
            byte[] temp = Encoding.Unicode.GetBytes(msg);               //获得缓存数据流
            NetworkStream streamToClient = rc.Client.GetStream();
            lock (streamToClient)
            {
                try
                {
                    Console.WriteLine("{0} 字节已发送给{1}！", temp.Length, rc.Name); //debug
                    streamToClient.Write(temp, 0, temp.Length);         //发送数据流称到客户端                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //发送新的客户端列表
        private void refreshList()
        {
            string msg = "$$$";                                     //开头三个$表示刷新列表的消息，可优化
            foreach (string name in clientList.Keys)
            {
                msg = msg + name + "$";
            }
            foreach (RemoteClient rc in clientList.Values)
            {
                sendMsg(rc, msg);
            }
        }

        //判断是否是断开连接的消息
        private bool IsDisconnectMsg(string msg)
        {
            if (msg.Length < 4)
                return false;
            if (msg.Substring(0, 3) == "^^^")
            {
                string name = msg.Substring(3, msg.Length - 3);
                clientList.Remove(name);
                Console.WriteLine("{0} 已下线", name);
                refreshList();
                return true;
            }
            else
                return false;
        }

        //检测TCP连接是否断开
        public static bool IsOnline(TcpClient c)
        {
            return !((c.Client.Poll(1000, SelectMode.SelectRead) && (c.Client.Available == 0)) || !c.Client.Connected);
        }
    }
}
