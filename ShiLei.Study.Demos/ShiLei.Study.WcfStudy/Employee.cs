using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShiLei.Study.WcfStudy
{
 
        [DataContract]
        public class Employee
        {
            [DataMember]

            public string EmployeeId;

            [DataMember]

            public string EmployeeName;

            [DataMember]

            public int EmployeeAge;

    }
}
