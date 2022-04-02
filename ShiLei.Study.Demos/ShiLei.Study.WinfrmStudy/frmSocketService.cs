using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiLei.Study.WinfrmStudy
{
    public partial class frmSocketService : Form
    {
        public frmSocketService()
        {
            InitializeComponent();
        }

        private void frmSocketService_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// 服务器端的监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //当点击开始监听的时候，在服务器端创建一个监听IP地址和端口号的socket（负责监听）
                // 绑定监听端口
                Socket socketwatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                // IPAddress ip= IPAddress.Parse(Server.Text);

                // 创建端口号对象
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(textport.Text));

                //监听
                socketwatch.Bind(point);  // 监听的目的是等待其他的连接；连接后，负责监听的socket调用accept，创建了负责与客户端连接的socket
                Showmes("监听成功");

                //一个时间点最大的连接数量   监听
                socketwatch.Listen(10);

                //Accept  等待客户端的连接
                // 要处理的问题：1、Accept() 需要一个线程来处理，不能放在主线程中。2、可以等待多个，而不是一个

                Thread th = new Thread(listen);
                th.IsBackground = true;
                th.Start(socketwatch);  // 负责监听的socket
            }
            catch
            {
                // 即使出现了异常，也不会抛出
            }
        }
        /// <summary>
        /// 等待客户端的连接，并且创建与之通信的socket  ，写成循环是因为每一个连接都会创建了一个通信socket
        /// </summary>
        /// <returns></returns>
        public void listen(object o)
        {
            try
            {
                Socket socketwatch = o as Socket; // 这个socket是负责监听的 as:类型转换，如果转换成功，返回转换结果，否则null.
                while (true)
                {
                    Socket socketSend = socketwatch.Accept();// 这个方法可以接受客户端的连接，监听的socket为新连接创建一个负责通信的socket
                    Showmes(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功");// 通过这个属性（RemoteEndPoint）拿到远程客户端的ip地址和端口号

                    // 客户端连接成功后，服务器应该接受客户端发过来的消息：这些都是由负责通信的socket来处理，监听的socket不再用
                    Thread thr = new Thread(receive);
                    thr.IsBackground = true;
                    thr.Start(socketSend);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 服务器端不停的接收发过来的数据
        /// </summary>
        public void receive(object o)
        {
            Socket socketSend = (Socket)o;
            while (true)
            {  // 在网络通信中，经常会发生各种异常，所以注意异常处理
                try
                {
                    byte[] bb = new byte[1024 * 1024];
                    int realnum = socketSend.Receive(bb);   //将接受的数据放在byte[]中，实际接受了realnum个
                    if (realnum == 0)
                    {
                        break;   // 如果客户端下线后，整个程序没有要结束的条件，一直在发空；所以，当接受到空时，结束
                    }
                    string realstr = Encoding.Default.GetString(bb, 0, realnum);
                    // txtlog.Text = realstr;
                    Showmes(socketSend.RemoteEndPoint + ":" + realstr);  // 内容是远程地址和端口号：内容
                }
                catch
                {

                }
            }
        }

        public void Showmes(string str)
        {
            label2.Text = str + "\r\n";
        }
        private void textport_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;// 取消跨线程的操作
        }

    }
}
