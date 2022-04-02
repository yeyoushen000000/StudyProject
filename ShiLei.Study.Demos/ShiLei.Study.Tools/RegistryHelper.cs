using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ShiLei.Study.Tools
{
    public class RegistryHelper
    {

        /// <summary>
        /// 注册表-保存与当前登录的用户有关的环境设置的数据，包含桌面设置、网络连接等
        /// </summary>
        public const string Hkey_CurrentUser = "CurrentUser";
        /// <summary>
        /// 注册表-用于保存本机系统的信息，包含硬件与操作系统的数据，如驱动程序、系统配置信息
        /// </summary>
        public const string Hkey_LocalMachine = "LocalMachine";
        /// <summary>
        /// 注册表-保存当用户登录时，所有必须载入的用户配置文件数据，包含缺省的配置文件和登录者的环境配置文件。
        /// </summary>
        public const string Hkey_Users = "Users";
        /// <summary>
        /// 注册表-保存与当前的硬件配置文件有关的数据
        /// </summary>
        public const string Hkey_CurrentConfig = "CurrentConfig";
        /// <summary>
        /// 注册表-用于保存文件扩展名与文件关联有关的信息
        /// </summary>
        public const string Hkey_ClassesRoot = "ClassesRoot";






        #region 增删改查注册表

        #endregion
        public static MessageInfo AddRegistryKey(string rootName, string subKeyName,
            RegistryKeyPermissionCheck permissionCheck = RegistryKeyPermissionCheck.Default, RegistryOptions registryOptions = RegistryOptions.Volatile)
        {
            RegistryKey key = GetRegistryRootKey(rootName);
            try
            {
                if (CheckSubkeyIsExist(key, subKeyName))
                    return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = "添加的注册表项已存在", Result = false };
                //创建注册表新项
                RegistryKey tempAdd = key.CreateSubKey(subKeyName);
              
            }
            catch (Exception e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = e.Message, Result = false };
            }
            finally
            {
                //释放注册表资源
                key.Close();
            }

            return new MessageInfo() { Type = MessageInfo.MessageType.None, Information = "", Result = true };
        }


        public static MessageInfo DeleteRegistryKey(string rootName, string subKeyName)
        {
            RegistryKey key = GetRegistryRootKey(rootName);
            try
            {
                //删除注册表项,true=找不到该项抛出异常
                key.DeleteSubKey(subKeyName, true);
                //释放注册表资源
                key.Close();
            }
            catch (InvalidOperationException e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = "要删除的注册表项有子项", Result = false };
            }
            catch (ArgumentException e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = "未找到要删除的注册表项", Result = false };
            }
            catch (Exception e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = e.Message, Result = false };
            }

            return new MessageInfo() { Type = MessageInfo.MessageType.None, Information = "", Result = true };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootName"></param>
        /// <param name="subKeyName">子项名称</param>
        /// <param name="name">键名</param>
        /// <param name="value">键值</param>
        /// <param name="valKind">键值类型</param>
        /// <returns></returns>
        public static MessageInfo ModifyRegistryKey(string rootName, string subKeyName, string name, string value, RegistryValueKind valKind)
        {

            RegistryKey key = GetRegistryRootKey(rootName);
            try
            {
                if (!CheckSubkeyIsExist(key, subKeyName))
                    return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = "需要修改的注册表项不存在", Result = false };

                //打开注册表项  true写访问权限
                using (RegistryKey tempkey = key.OpenSubKey(subKeyName, true))
                {
                    tempkey.SetValue(name, value, valKind);
                }
                //释放注册表资源
                key.Close();
            }
            catch (ArgumentNullException e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = "要修改的注册表项值为空", Result = false };
            }
            catch (ArgumentException e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = "要修改的注册表项指定的数据类型不匹配", Result = false };
            }
            catch (Exception e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = e.Message, Result = false };
            }

            return new MessageInfo() { Type = MessageInfo.MessageType.None, Information = "", Result = true };
        }


        public static MessageInfo QueryRegistryKeyValue(string rootName, string subKeyName, string name,ref string outQueryValue)
        {
            outQueryValue = string.Empty;
            RegistryKey key = GetRegistryRootKey(rootName);
            try
            {
                if (!CheckSubkeyIsExist(key, subKeyName))
                    return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = "查询的注册表项不存在", Result = false };

                //打开注册表项  true写访问权限
                using (RegistryKey tempkey = key.OpenSubKey(subKeyName, true))
                {
                    outQueryValue =tempkey.GetValue(name,"").ToString();
                }
                //释放注册表资源
                key.Close();
            }
            catch (Exception e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = e.Message, Result = false };
            }

            return new MessageInfo() { Type = MessageInfo.MessageType.None, Information = "", Result = true };

        }


        public static MessageInfo QueryRegistryKeyValue(string rootName, string subKeyName,List<string> lsName,ref Dictionary<string,string> reDic)
        {
           
            RegistryKey key = GetRegistryRootKey(rootName);
            try
            {
                if (!CheckSubkeyIsExist(key, subKeyName))
                    return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = "查询的注册表项不存在", Result = false };

                //打开注册表项  true写访问权限
                using (RegistryKey tempkey = key.OpenSubKey(subKeyName, true))
                {
                    foreach (string name in lsName)
                    {
                        string queryValue = tempkey.GetValue(name,"").ToString();
                        reDic.Add(name, queryValue);
                    }
                }
                //释放注册表资源
                key.Close();
            }
            catch (Exception e)
            {
                return new MessageInfo() { Type = MessageInfo.MessageType.Err, Information = e.Message, Result = false };
            }

            return new MessageInfo() { Type = MessageInfo.MessageType.None, Information = "", Result = true };

        }

        public static List<string> GetAllChildSubkeyName(string rootName, string subKeyName)
        {
            List<string> lsChildSubkey = new List<string>();
            RegistryKey key = GetRegistryRootKey(rootName);
            try
            {
                if (!CheckSubkeyIsExist(key, subKeyName))
                    return null;

                //打开注册表项  true写访问权限
                using (RegistryKey tempkey = key.OpenSubKey(subKeyName, true))
                {
                    lsChildSubkey = tempkey.GetSubKeyNames().ToList();
                }
                //释放注册表资源
                key.Close();
            }
            catch (Exception e)
            {
                return null;
            }

            return lsChildSubkey;

        }







        private static RegistryKey GetRegistryRootKey(string rootName)
        {
            RegistryKey temp = Registry.LocalMachine;
            switch (rootName)
            {
                case "CurrentUser":
                    temp = Registry.CurrentUser;
                    break;
                case "LocalMachine":
                    temp = Registry.LocalMachine;
                    break;
                case "Users":
                    temp = Registry.Users;
                    break;
                case "CurrentConfig":
                    temp = Registry.CurrentConfig;
                    break;
                case "ClassesRoot":
                    temp = Registry.ClassesRoot;
                    break;
                default:
                    temp = Registry.LocalMachine;
                    break;
            }
            return temp;
        }


        private static bool CheckSubkeyIsExist(RegistryKey key, string subkey)
        {
            if (key.OpenSubKey(subkey) != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 从指定注册表的路径中获取指定的可执行文件信息集合
        /// </summary>
        /// <param name="regKeyPath"></param>
        /// <param name="type"> "16"=.exe程序</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetInfoFromRegistryKey(string regKeyPath, string type)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            using (RegistryKey keys = Registry.LocalMachine.OpenSubKey(regKeyPath, false))
            {
                if (keys != null)
                {
                    foreach (string keyName in keys.GetSubKeyNames())
                    {
                        //todo： 要有这样子一种意识，凡是占用到系统的资源的对象，都应该是用完就要释放资源
                        //以防止占用过多导致系统资源不足
                        using (RegistryKey childKey = keys.OpenSubKey(keyName, false))
                        {
                            if (childKey == null) continue;

                            string keyType = childKey.GetValue("Type", "").ToString();
                            if (keyType == string.Empty) continue;
                            //type=16表示可执行文件.exe
                            if (keyType == type)
                            {
                                //文件路径
                                string installPath = childKey.GetValue("ImagePath", "").ToString();
                                //文件名称
                                string softwareName = childKey.GetValue("DisplayName", "").ToString();
                                if (softwareName != string.Empty && installPath != string.Empty)
                                    result.Add(softwareName, installPath);
                                else
                                    continue;
                            }
                        }
                    }
                }
            }
            return result;
        }


        public static List<string> GetUsbRecordFromRegistry(string regKeyPath, string type="disk")
        {

            List<string> diskInfo = new List<string>();
            RegistryKey rootKey = Registry.LocalMachine.OpenSubKey(regKeyPath);
            List<string> childKey = rootKey.GetSubKeyNames().ToList();
            if (childKey != null && childKey.Count > 0)
            {
                foreach (string name in childKey)
                {
                    using (RegistryKey rgk = rootKey.OpenSubKey(name))
                    {
                        string uid = rgk.GetSubKeyNames().ToList().First();
                        using (RegistryKey temp = rgk.OpenSubKey(uid))
                        {
                            //service有两种值  disk=磁盘  cdrom=光盘
                            if (temp.GetValue("Service", "").ToString() == type)
                            {
                                StringBuilder sb = new StringBuilder();
                                string diskName=temp.GetValue("FriendlyName", "").ToString();
                                sb.Append("磁盘全称："+diskName+"\r\n");
                                sb.Append("磁盘UID：" + uid + "\r\n");

                                string fullPath = "Hkey_Local_Machine\\" + regKeyPath + "\\" + name;
                                sb.Append("磁盘全路径：" + fullPath + "\r\n");
                                diskInfo.Add(sb.ToString());
                            }
                        }
                    }
                }
            }
            return diskInfo;
        }



    }
}
