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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
            #endregion


            #region 2.0 指定管理员权限启动
            ////获取window系统的当前登录用户
            //WindowsIdentity winId = WindowsIdentity.GetCurrent();
            ////利用window用户实例化WindowsPrincipal对象
            //WindowsPrincipal winPro = new WindowsPrincipal(winId);
            ////利用WindowsPrincipal对象判断是否为管理员角色
            //if (winPro.IsInRole(WindowsBuiltInRole.Administrator))
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new frmMain());
            //}
            //else
            //{
            //    //非管理员角色
            //    MessageBox.Show("非管理员权限执行以下程序");
            //    ProcessStartInfo proStrInfo = new ProcessStartInfo();
            //    proStrInfo.FileName = Application.ExecutablePath;
            //    proStrInfo.Arguments = string.Join(" ", "");
            //    proStrInfo.Verb = "runas";
            //    Process.Start(proStrInfo);

            //    Application.Exit();
            //}
            #endregion
        }
    }
}
