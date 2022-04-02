using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiLei.Study.Tools.Models
{
  public  class RegisterKeyModel
    {
        /// <summary>
        /// 需要查询主项名称
        /// </summary>
        public string SubKeyName { get; set; }
        /// <summary>
        /// 所有子Subkey的Name
        /// </summary>
        public List<string> ChildSubKeyNames { get; set; }

        /// <summary>
        /// 所有属性键值对
        /// </summary>
        public Dictionary<string, string> AllPropKeyValue { get; set; }

    }
}
