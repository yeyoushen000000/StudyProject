using ShiLei.Study.WinfrmStudy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiLei.Study.WinfrmStudy
{
    public partial class frmConfigFileSetting : Form
    {
        //****************************************************************************************************
        //1:对于App.config文件中，除了appSetting节点，自定义节点的操作
        //2:自定义节点必须在<ConfigSections>标签下定义个Section标签，包含属性name和属性type
        //3:属性name和需要自定义的节点名称保持一致，属性type处理为操作自定义节点的类和该类的命名空间
        //4:在调用ConfigurationManager.GetSection时，配置在<ConfigSections>的处理类，因为继承IConfigurationSectionHandler接口，会触发此接口的Create方法，
        //****************************************************************************************************
        public frmConfigFileSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigurationManager.GetSection("");
            StringBuilder sb = new StringBuilder();
            //从配置中获取节点
            List< studentPropertySection> studentList=  StudentPropertyOper.GetStudentSectionList();


            foreach (var item in studentList)
            {
                string strTemp = "学生姓名：" + item.Name + "  性别：" + item.Sex + "  编号：" + item.IdCard + "\r\n";
                sb.Append(strTemp);
            }

            label1.Text = sb.ToString();
        }
    }
}
