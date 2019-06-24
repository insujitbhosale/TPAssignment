using DataEntities.Models;
using DataEntities.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEntities.Repository.Services
{
    public class DepartmentRepository : IDepartmentRepository<Department>
    {
        readonly ApplicationContext applicationContext;

        public DepartmentRepository(ApplicationContext context)
        {
            this.applicationContext = context;
        }

        public Department Add(Department department)
        {
            this.applicationContext.Add(department);
            this.applicationContext.SaveChanges();
            return department;
        }

        public bool Delete(int id)
        {
            var department = this.applicationContext.Departments.FirstOrDefault(x => x.DeptID == id);
            if (department != null)
            {
                this.applicationContext.Remove<Department>(department);
                this.applicationContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Department Get(int id)
        {
            return this.applicationContext.Departments.Where(e => e.DeptID == id).FirstOrDefault();
        }

        public IEnumerable<Department> GetAll()
        {
            return this.applicationContext.Departments.ToList();
        }

        public bool IsExists(int id)
        {
            return this.applicationContext.Departments.Any(e => e.DeptID == id);
        }

        public Department Update(int id, Department department)
        {
            department.DeptID = id;
            this.applicationContext.Update(department);
            this.applicationContext.SaveChanges();
            return department;
        }
    }
}
