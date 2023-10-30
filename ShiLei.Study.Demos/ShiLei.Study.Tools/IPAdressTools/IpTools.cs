using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShiLei.Study.Tools
{
    public class IpTools
    {

        #region 是否为合法IPV4地址，正则表达式
        public static bool IsLegalIP(string strIPadd)
        {
            if (Regex.IsMatch(strIPadd, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                string[] ips = strIPadd.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

        #region 获取本机IP地址 GetLocalIP()
        private string GetLocalIP()
        {
            string strLocalIP = "";
            string strPcName = Dns.GetHostName();   //获取主机名称

            IPHostEntry ipEntry = Dns.GetHostEntry(strPcName);

            foreach (var IPadd in ipEntry.AddressList)
            {
                if (IsLegalIP(IPadd.ToString()))
                {
                    strLocalIP = IPadd.ToString();
                    break;
                }
            }
            return strLocalIP;
        }
        #endregion

        #region  转换IP地址为无符号数 TurnIpAdressToUint32 
        /// <summary>
        /// Convert IP address which is string type into IP (long type)
        /// </summary>
        /// <param name="IpStr">Ip address string</param>
        /// <returns>UInt32</returns>
        public UInt32 TurnIpAdressToUint32(String IpStr)
        {
            IPAddress ipaddress;
            if (IPAddress.TryParse(IpStr, out ipaddress))
            {
                byte[] ipBytes = ipaddress.GetAddressBytes();
                return BitConverter.ToUInt32(ipBytes, 0);
            }
            else
                return 0;
        }
        #endregion







    }
}
