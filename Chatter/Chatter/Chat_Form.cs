using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatter
{
    public partial class Chat_Form : Form
    {
        private TcpClient Client;                //TCP客户端
        private string Name;                     //注册名称
        private const int BufferSize = 8192;
        private byte[] buffer;
        private NetworkStream streamToServer;    //用于发送接收的流

        public Chat_Form(TcpClient client, string name)
        {
            InitializeComponent();
            this.Client = client;
            this.Name = name;
            this.buffer = new byte[BufferSize];   
            this.streamToServer = Client.GetStream();

            this.Text = this.Name + " 的聊天窗口";

            lock (streamToServer)
            {
                AsyncCallback callBack = new AsyncCallback(ReadComplete);
                streamToServer.BeginRead(buffer, 0, BufferSize, callBack, null);
            }
        }

        private void ReadComplete(IAsyncResult ar)
        {
            int bytesRead;

            try
            {
                lock (streamToServer)
                {
                    bytesRead = streamToServer.EndRead(ar);
                }
                if (bytesRead == 0) throw new Exception("读取到0字节");

                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);

                if (msg.Substring(0, 3) == "$$$")
                {
                    readLists(msg);
                }
                else
                    ClientRTB_msg.AppendText(msg);

                Array.Clear(buffer, 0, buffer.Length);      // 清空缓存，避免脏读

                lock (streamToServer)
                {
                    AsyncCallback callBack = new AsyncCallback(ReadComplete);
                    streamToServer.BeginRead(buffer, 0, BufferSize, callBack, null);
                }
            }
            catch (Exception ex)
            {
                if (streamToServer != null)
                    streamToServer.Dispose();
                Client.Close();

                Console.WriteLine(ex.Message);
            }
        }

        ~Chat_Form()
        {
            Client.Close();
        }

        private void ChatBT_Send_Click(object sender, EventArgs e)
        {
            string msg = ClintRTB_snd.Text;
            if (msg == "")
            {
                MessageBox.Show("消息不能为空！");
                return;
            }
            ClintRTB_snd.Text = "";
            ClientRTB_msg.AppendText("你 ： " + msg + "\r\n");
            sendMessage(msg);
        }

        private void sendMessage(string msg)
        {
            if (Client.Connected)
            {
                lock (streamToServer)
                {
                    byte[] temp = Encoding.Unicode.GetBytes(msg);    //获得缓存数据流
                    try
                    {
                        streamToServer.Write(temp, 0, temp.Length);  //发送数据流称到服务器
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("客户端连接服务器出错！请重新打开客户端");
        }

        private void readLists(string msg)
        {
            ChatLB_list.Items.Clear();
            string name = "";
            for (int i = 3; i < msg.Length; i++)
            {               
                string tmp = msg.Substring(i, 1);
                if (tmp != "$")
                {
                    name += tmp;
                }
                else {
                    ChatLB_list.Items.Add(name);
                    name = "";
                }
            }
        }

        private void Chat_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            string msg = "^^^" + Name;                         //关闭时发送关闭消息
            sendMessage(msg);
            Client.Close();
            client_log father_form = new client_log();
            father_form = (client_log)this.Owner;
            father_form.Dispose();
            father_form.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg = "^^^" + Name;                         //关闭时发送关闭消息
            sendMessage(msg);
            Client.Close();
            client_log father_form = new client_log();
            father_form = (client_log)this.Owner;
            father_form.Dispose();
            father_form.Close();

            this.Dispose();
            this.Close();
        }
    }
}
