using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chatter
{
    public partial class client_log : Form
    {
        private TcpClient client;                //TCP客户端
        private string name;                     //注册名称
        private NetworkStream streamToServer;    //用于发送接收的流
        private const int BufferSize = 8192;
        private byte[] buffer;
        private ManualResetEvent connectDone;    //用于进程同步

        public client_log()
        {
            InitializeComponent();
            connectDone = new ManualResetEvent(false);
            buffer = new byte[BufferSize];
        }

        private void logbut_login_Click(object sender, EventArgs e)
        {
            if (logCB_server.SelectedItem == null)
            {
                MessageBox.Show("请选择端口号！");
                return;
            }
            if (logTB_Name.Text == "")
            {
                MessageBox.Show("请填写注册名称！");
                return;
            }

            name = logTB_Name.Text;
            string port = logCB_server.SelectedItem.ToString();

            connectDone.Reset();
            client = new TcpClient();
            try
            {
                client.BeginConnect("localhost", Int32.Parse(port), new AsyncCallback(ConnectCallback), client);   //异步连接
                this.Enabled = false;
                connectDone.WaitOne();                //等待连接进程结束

                if(client != null && client.Connected)
                {
                    streamToServer = client.GetStream();
                    sendName();                               //向服务器发送注册名字
                    if (allowed())                            //如果返回true，则注册成功
                    {
                        this.Visible = false;
                        Chat_Form fm = new Chat_Form(client, name);
                        fm.ShowDialog(this);                  //显示客户端窗口，并制定其所有者为自己，方便释放资源                     
                    }
                    else
                        MessageBox.Show("名字已被注册！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            this.Enabled = true;
        }

        //异步连接回调函数
        private void ConnectCallback(IAsyncResult ar)
        {
            connectDone.Set();
            TcpClient t = (TcpClient)ar.AsyncState;
            try
            {
                if (!t.Connected)
                {
                    MessageBox.Show("连接失败");
                    t.EndConnect(ar);
                }
                t.EndConnect(ar);
            }
            catch (SocketException se)
            {
                MessageBox.Show("连接发生错误ConnCallBack.......:" + se.Message);
            }
        }

        //同步发送注册名称
        private void sendName()
        {
            byte[] temp = Encoding.Unicode.GetBytes(name);    //获得缓存数据流
            try
            {
                streamToServer.Write(temp, 0, temp.Length);  //发送数据流称到服务器
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //同步接收权限
        private bool allowed()
        {
            int bytesRead = streamToServer.Read(buffer, 0, BufferSize);
            string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            Array.Clear(buffer, 0, buffer.Length);         //清空缓存，避免脏读

            if (msg == "true")
                return true;
            else
                return false;
        }
    }
}
