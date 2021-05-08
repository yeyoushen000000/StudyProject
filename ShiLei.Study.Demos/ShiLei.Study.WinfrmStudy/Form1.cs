using System;
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
    public partial class Form1 : Form
    {
        public Form1()
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

      

    }
}
