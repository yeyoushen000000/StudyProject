using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;

namespace ShiLei.Study.WinfrmStudy
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 1.0  C#默认启动程序
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMain());
            #endregion


            #region 2.0 指定管理员权限启动
            //获取window系统的当前登录用户
            WindowsIdentity winId = WindowsIdentity.GetCurrent();
            //利用window用户实例化WindowsPrincipal对象
            WindowsPrincipal winPro = new WindowsPrincipal(winId);
            //利用WindowsPrincipal对象判断是否为管理员角色
            if (winPro.IsInRole(WindowsBuiltInRole.Administrator))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
            else
            {
                //非管理员角色
                MessageBox.Show("非管理员权限执行以下程序");
                ProcessStartInfo proStrInfo = new ProcessStartInfo();
                proStrInfo.FileName = Application.ExecutablePath;
                proStrInfo.Arguments = string.Join(" ", "");
                proStrInfo.Verb = "runas";
                Process.Start(proStrInfo);
            
                Application.Exit();
        }



        ///** 
        // * 当前用户是管理员的时候，直接启动应用程序 
        // * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行 
        // */
        ////获得当前登录的Windows用户标示
        ////URL：http://www.bianceng.cn/Programming/csharp/201410/45784.htm
        ////获取当前登录的用户
        //WindowsIdentity identity = WindowsIdentity.GetCurrent();
        ////创建Windows用户主题  
        //Application.EnableVisualStyles();

        //WindowsPrincipal principal = new WindowsPrincipal(identity);
        ////判断当前登录用户是否为管理员  
        //if (principal.IsInRole(WindowsBuiltInRole.Administrator))
        //{
        //    //如果是管理员，则直接运行  

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new frmMain());
        //}
        //else
        //{
        //    //创建启动对象   ProcessStartInfo 启动一组
        //    ProcessStartInfo startInfo = new ProcessStartInfo();
        //    //设置运行文件  
        //    startInfo.FileName = Application.ExecutablePath;
        //    //设置启动参数  
        //    startInfo.Arguments = String.Join(" ", "");
        //    //设置启动动作,确保以管理员身份运行  
        //    startInfo.Verb = "runas";
        //    //如果不是管理员，则启动UAC  
        //    System.Diagnostics.Process.Start(startInfo);
        //    //退出  
        //    System.Windows.Forms.Application.Exit();
        //}
        #endregion





    }







        }
}
