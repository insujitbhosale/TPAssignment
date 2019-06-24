using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.Repository.Contract
{
    public interface IEmployeeRepository<Employee>
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
        Employee Add(Employee employee);
        Employee Update(int id, Employee employee);
        bool Delete(int id);
        bool IsExists(int id);
    }
}
