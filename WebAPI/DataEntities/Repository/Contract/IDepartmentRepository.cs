using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.Repository.Contract
{
    public interface IDepartmentRepository<Department>
    {
        IEnumerable<Department> GetAll();
        Department Get(int id);
        Department Add(Department department);
        Department Update(int id, Department department);
        bool Delete(int id);
        bool IsExists(int id);
    }
}
