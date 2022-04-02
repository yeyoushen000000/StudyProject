using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ShiLei.Study.WcfStudy
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]



    public class EmployeeSrv : IEmployeeSrv

    {

        List<Employee> _Employee = new List<Employee>();

        public void AddEmployees(Employee employee)
        {

            employee.EmployeeId = Guid.NewGuid().ToString();

            _Employee.Add(employee);

        }



        public void RemoveEmployee(string id)
        {

            Employee employee = _Employee.Find(p => p.EmployeeId == id);

            _Employee.Remove(employee);
        }



        public List<Employee> GetAllEmployees()
        {

            return _Employee;

        }

    }

}
