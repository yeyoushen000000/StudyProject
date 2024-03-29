﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiLei.Study.WinfrmStudy
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region 1.0 委托操作按钮
        private void button1_Click(object sender, EventArgs e)
        {
            Form frmOne = new frmDelegate();
            frmOne.ShowDialog();
        }
        #endregion

        #region 2.0  Config配置文件操作按钮
        //处理配置项窗口
        private void button2_Click(object sender, EventArgs e)
        {
            Form frmOne = new frmConfigFileSetting();
            frmOne.ShowDialog();
        }


        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
           
            //string ss = "AA,55," +
            //    "5C," +
            //    "01," +
            //    "3B," +
            //    "9C,E8,07,00," +
            //    "01,00,00,00," +
            //    "00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00," +
            //    "41,65";



            //var  sdf= ss.Split(',').ToList();

            //Byte[] arrByte = new Byte[92];
            //for (int i = 0; i < arrByte.Length; i++)
            //{
            //    arrByte[i] = Convert.ToByte( sdf[i],16);
            //}



            //if (GetCheckSum(arrByte, 2) == arrByte[91])
            //{

            //    string sssdsfdf = "";
            //}

            //int sss = ss.Length;
        }




        /// <summary>
        /// 获取字节数组的校验和
        /// </summary>
        /// <param name="bts">字节数组</param>
        /// <returns></returns>
        private int GetCheckSum(byte[] bts, int offset)
        {
            int sum = 0;
            for (int i = offset; i < offset + 89; i++)
            {
                sum += bts[i];
            }
            return sum % (byte.MaxValue + 1);
        }

        #region 操作注册表
        private void button3_Click(object sender, EventArgs e)
        {
            frmRegisterKey frmRegKey = new frmRegisterKey();
            frmRegKey.ShowDialog();
        }


        #endregion
        //操作注册表

        #region 操作XML页面
        private void button5_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void button6_Click(object sender, EventArgs e)
        {


            #region  Switch模式匹配 net core 版本
            //int obj = 10;
            //switch (obj)
            //{
            //    case (> 0) and (< 5):
            //        Console.WriteLine($"小于5的数");
            //        break;
            //    case (> 5) and (< 10):
            //        Console.WriteLine("小于10的数");
            //        break;
            //    case (>= 10) and (< 20):
            //        Console.WriteLine("小于20的数");
            //        break;
            //    default:
            //        Console.WriteLine("未知类型");
            //        break;
            //}
            #endregion


            //if < 5 { }
            //else if < 8{ }
            //else if < 14{ }
            //else { }
        }


        #region 多线程编程
        private void button7_Click(object sender, EventArgs e)
        {
            Form frmOne = new frmThreads();
            frmOne.ShowDialog();
        }
        #endregion


        ///net core 版本
        //public int GetStatus(int seconds)
        //{
        //    return seconds switch
        //    {
        //        >= 0 and < 5 => 0,
        //        >= 5 and < 8 => 1,
        //        >= 8 and < 14 => 2,
        //        _ => -1 // 其他情况
        //    };
        //}
    }
}
