using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tools
{
    public static class XmlHelper
    {


        #region 创建Xml文件
        public static  XmlDocument CreateXmlFile(string path,string rootNode,string enCoding)
        {
            //创建一个新的Xml文件
            XmlDocument newFile = new XmlDocument();
            XmlDeclaration xmlDec = newFile.CreateXmlDeclaration("1.0","","");
            xmlDec.Encoding = Encoding.UTF8.EncodingName;
            xmlDec.Standalone = "";
            return null;
        }

        #endregion







        #region 加载Xml文件
        public static XmlDocument LoadXmlFile(string filePath)
        {
            XmlDocument xmlDc = new XmlDocument();
            //加载xml文件
            if (File.Exists(filePath))
                xmlDc.Load(filePath);
            else
                xmlDc = null;

            return xmlDc;
        }
        #endregion

        #region 根据节点名称查询 返回Element或者Node
        
        public static XmlNode GetXmlNodeByName(string nodeName, string filePath)
        {
            XmlDocument xmlDc = LoadXmlFile(filePath);
            XmlNode rootNode = xmlDc.SelectSingleNode("//" + nodeName);
            return rootNode;
        }

        public static XmlNode GetXmlNodeByName(string nodeName, XmlNode rootNode)
        {
            XmlNode childNode = rootNode.SelectSingleNode(nodeName);
            return childNode;
        }


        public static XmlElement GetXmlElementByName(string nodeName, XmlNode rootNode)
        {
            XmlNode childNode = rootNode.SelectSingleNode(nodeName);
            return (XmlElement)childNode;
        }

        #endregion

        #region 节点属性的CRUD
        /// <summary>
        /// 更新指定节点的属性
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="nodePath"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void UpdateSingleNodeAttribute(string filePath, string nodePath, string key, string value,string id)
        {
            XmlDocument xmldc = LoadXmlFile(filePath);
            XmlElement root = xmldc.DocumentElement;
            XmlNode setNode = root.SelectSingleNode(nodePath);

            foreach (XmlNode item in setNode.ChildNodes)
            {
                if (item.Attributes.GetNamedItem("Id").Value == id)
                { 
                    item.Attributes[key].Value = value;
                    xmldc.Save(filePath);
                    break;
                }
            }
        }

        /// <summary>
        /// 增加指定节点的属性
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="nodePath"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddNewNodeAttribute(string filePath, string nodePath, string key, string value)
        {
            XmlDocument xmldc = LoadXmlFile(filePath);
            XmlElement root = xmldc.DocumentElement;
            XmlElement setElement = (XmlElement)root.SelectSingleNode(nodePath);
            XmlAttribute attr = xmldc.CreateAttribute(key);
            attr.Value = value;
            setElement.Attributes.Append(attr);
            xmldc.Save(filePath);
        }


        /// <summary>
        /// 移除指定节点的属性
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="nodePath"></param>
        /// <param name="key"></param>
        public static void RemoveNodeAttribute(string filePath, string nodePath, string key)
        {
            XmlDocument xmldc = LoadXmlFile(filePath);
            XmlElement root = xmldc.DocumentElement;
            XmlElement setElement = (XmlElement)root.SelectSingleNode(nodePath);
            XmlAttribute attr = xmldc.CreateAttribute(key);

            setElement.Attributes.Remove(attr);
            xmldc.Save(filePath);
        }
        #endregion

    }
}
