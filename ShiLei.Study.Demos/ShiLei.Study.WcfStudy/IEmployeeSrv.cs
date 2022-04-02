using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ShiLei.Study.WcfStudy
{
 


    [ServiceContract]

    public interface IEmployeeSrv

    {

        [OperationContract]

        void AddEmployees(Employee employee);

        [OperationContract]

        List<Employee> GetAllEmployees();

        [OperationContract]

        void RemoveEmployee(string id);



    }


}
