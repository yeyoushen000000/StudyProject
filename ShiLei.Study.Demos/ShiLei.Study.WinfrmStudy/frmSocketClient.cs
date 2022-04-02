using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiLei.Study.WinfrmStudy
{
    public partial class frmSocketClient : Form
    {
      

        private void frmSocketClient_Load(object sender, EventArgs e)
        {

        }



     
            public frmSocketClient()
            {
                InitializeComponent();
            }

            Socket socketsend;
            private void button1_Click(object sender, EventArgs e)
            {
                // 创建负责通信的socket
                socketsend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//InterNetwork代表IPV4

                //获得IP地址和端口号
                IPAddress ip = IPAddress.Parse(textBox3.Text);  //获得要连接远程服务器端的IP地址
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(textBox1.Text));
                //获得要连接的远程应用程序的IP地址和端口号
                socketsend.Connect(point);
                showmess("连接成功");
            }
            public void showmess(string str)
            {
               // textBox2.Text = str + "\r\n";
            }

            private void button2_Click(object sender, EventArgs e)
            {
                byte[] bs = new byte[1024];
                bs = Encoding.Default.GetBytes(textBox4.Text);

                int nums = socketsend.Send(bs);
            }

      
    
  

    }
}
