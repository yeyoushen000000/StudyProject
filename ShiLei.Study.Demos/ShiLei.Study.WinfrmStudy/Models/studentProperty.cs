using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShiLei.Study.WinfrmStudy.Models
{
    /// <summary>
    /// 对应App.config中的自定义节点属性
    /// </summary>
    public class studentPropertySection
    {
        private string _name;
        private string _sex;
        private string _idCard;

        public string Name { get { return _name; } set { _name = value; } }
        public string Sex { get { return _sex; } set { _sex = value; } }
        public string IdCard { get { return _idCard; } set { _idCard = value; } }


        //利用构造函数来初始化对象
        public studentPropertySection(XmlNode xmlNode)
        {
            XmlElement e = (XmlElement)xmlNode;
            _name = e.GetAttribute("name");
            _sex = e.GetAttribute("sex");
            _idCard = e.GetAttribute("idCard");
        }
    }


    //针对特定节点的处理类，一定要实现IConfigurationSectionHandler中的Create方法
    public class StudentPropertyHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            List<studentPropertySection> lsTemp = new List<studentPropertySection>();

            //选择
            foreach (XmlNode item in section.SelectNodes("//studentProperty"))
            {
                studentPropertySection node = new studentPropertySection(item);
                lsTemp.Add(node);
            }
            return lsTemp;
        }
    }


    public class StudentPropertyOper
    {

        //studentPropertySection 的处理类是 StudentPropertyHandler 实现了接口IConfigurationSectionHandler
        public static List<studentPropertySection> GetStudentSectionList()
        {
            //在GetSection的时候，因为StudentPropertyHandler的处理类继承IConfigurationSectionHandler接口，会触发此接口的Create方法，
            return ConfigurationManager.GetSection("studySection") as List<studentPropertySection>;
        }
    }
        


}
