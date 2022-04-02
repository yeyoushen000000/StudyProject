using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiLei.Study.Tools
{
   public class MessageInfo
    {
        //消息类型
        public enum MessageType
        {
            //无消息
            None=0,
            //通知消息
            Info = 1,
            //错误消息
            Err = 2,
            //警告消息
            Warn = 3,
            //日志信息
            Log = 4
        }



        //结果
        bool _result=true;
        //信息
        string _information;

        MessageType _type;

        //消息结果
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        //消息内容
        public string Information {
            get { return _information; }
            set { _information = value; }
        }

        //消息类型
        public MessageType Type {

            get { return _type; }
            set { _type = value; }

        }


    }
}
