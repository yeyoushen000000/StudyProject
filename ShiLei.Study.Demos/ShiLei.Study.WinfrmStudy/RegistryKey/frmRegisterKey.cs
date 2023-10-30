using Microsoft.Win32;
using ShiLei.Study.Tools;
using ShiLei.Study.Tools.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiLei.Study.WinfrmStudy
{
    public partial class frmRegisterKey : Form
    {
        //1：注册表子key中的Type表示类型  16=可执行文件.exe
        //2：注册表子key中的ImagePath表示文件安装路径，DisplayName表示文件名称
        //3：subkey的含义是子秘钥
        //4：查找已经安装好的服务service，注册表路径为"SYSTEM\CurrentControlSet\Services\"
        //
        //
        //
        //
        //
        //什么时候会用到注册注册表？  注册表的增删查改如何写呀，是否有带保护的增加删改？


        //运用注册表的场景有哪些？
        //1：知道上一级目录，直接查找该目录下的所有的可执行的文件，用于收集已安装文件的
        //2：利用注册表写入，让程序自启动



        //安装服务的注册表路径
        public const string   RegKey_Service= @"SYSTEM\CurrentControlSet\Services\";
        //usb设备安装记录注册表路径
        public const string RegKey_UsbInfo = @"SYSTEM\\CurrentControlSet\\Enum\\USBSTOR";
        //设备自启动注册表路径
        public const string RegKey_RunExe = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        // CurrentVersion下的Run可用于设置程序开机自启，Uninstall可查看到自己打包安装的程序的 ProductCode
        public const string RegKey_UninstallProcess = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
       

        public frmRegisterKey()
        {
            InitializeComponent();
        }




        private void frmRegisterKey_Load(object sender, EventArgs e)
        {
        
            
        }

        #region 操作注册表关联程序和文件扩展名
        private void button1_Click(object sender, EventArgs e)
        {


            //利用notepad软件打开自定义.ttt文件
            string exeFilePath = @"D:\Program Files\npp.6.5.RC\notepad++.exe";
            richTextBox1.Text = "提示信息：" + "\r\n" + "自定义的后缀文件名：" + ".ttt" + "\r\n" + "打开自定义关联文件的软件：" + exeFilePath;
            //在Hkey_ClassesRoot增加一个.ttt的子项
            MessageInfo info = RegistryHelper.AddRegistryKey(RegistryHelper.Hkey_ClassesRoot, ".ttt");
            if (info.Result)
            {
                RegistryHelper.ModifyRegistryKey(RegistryHelper.Hkey_ClassesRoot, ".ttt", "Content Type", "text/plain", RegistryValueKind.String);
                RegistryHelper.ModifyRegistryKey(RegistryHelper.Hkey_ClassesRoot, ".ttt", "PerceivedType", "text", RegistryValueKind.String);
                MessageInfo info2 = RegistryHelper.AddRegistryKey(RegistryHelper.Hkey_ClassesRoot, ".ttt\\shell\\open\\command");
                //层级使用反斜杠，一层一层一层的处理，
                RegistryHelper.ModifyRegistryKey(RegistryHelper.Hkey_ClassesRoot, ".ttt\\shell\\open\\command", "", exeFilePath + " %1", RegistryValueKind.String);

            }
        }

        #endregion

        #region 操作CLSID将文件夹伪装成系统组件
        private void button2_Click(object sender, EventArgs e)
        {
            //CLSID就是一个类似GUID，能够唯一标识系统文件的
            //获取当前可执行程序的路径
            string direcPath  =Path.GetDirectoryName(Application.StartupPath);
            string newDrPath = direcPath + "\\新建文件夹";


            //todo 这里回头要换成文件处理类进行处理
            //先删除之前的文件夹
            if (Directory.Exists(newDrPath))
                Directory.Delete(newDrPath);


            DirectoryInfo newDr = Directory.CreateDirectory(newDrPath);
            string movePath = direcPath + "\\网上邻居.{208D2C60-3AEA-1069-A2D7-08002B30309D}";

            //先删除之前的文件夹
            if (Directory.Exists(movePath))
                Directory.Delete(movePath);

            //将文件夹重新命名，系统只提供了MoveTo
            newDr.MoveTo(movePath);
         


            //提示信息
            richTextBox1.Text = "提示信息：" + "\r\n" + "指定路径下的新建文件夹将被模拟成网上邻居" + "\r\n" + "网上邻居CLSID：{208D2C60-3AEA-1069-A2D7-08002B30309D}";
            //richTextBox1.Text = "提示信息：" + "\r\n" + "新建文件夹将被模拟成回收站" + "\r\n" + "回收站CLSID：{645FF040-5081-101B-9F08-00AA002F954E}";
        }
        #endregion

        #region 操作注册表获取usb操作记录
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            List<string> lsUsbInfo = RegistryHelper.GetUsbRecordFromRegistry(RegKey_UsbInfo, "disk");
           foreach (string item in lsUsbInfo)
            {
                richTextBox1.Text = richTextBox1.Text + item + "\r\n";

            }
        }
        #endregion

        #region 操作注册表获取所有已经安装window服务的地址和名称
        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;

            //获取已经注册的服务
            Dictionary<string, string> dicResult = RegistryHelper.GetInfoFromRegistryKey(RegKey_Service, "16");

            foreach (KeyValuePair<string, string> item in dicResult)
            {
                richTextBox1.Text = richTextBox1.Text + "服务名称：" + item.Key + "\r\n" + "安装路径：" + item.Value + "\r\n";
            }

        }
        #endregion


        #region 操作注册表设置软件自启动
        private void button6_Click(object sender, EventArgs e)
        {

            //取到当前可执行文件的执行路径
            string fullPath = Application.ExecutablePath;
            //获取可执行文件名
            string processName = Path.GetFileName(fullPath);

            MessageInfo result = RegistryHelper.ModifyRegistryKey(RegistryHelper.Hkey_LocalMachine, RegKey_RunExe, processName, fullPath, RegistryValueKind.String);

        }
        #endregion

    }
}

