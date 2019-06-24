using DataEntities.Models;
using DataEntities.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEntities.Repository.Services
{
    public class EmployeeRepository : IEmployeeRepository<Employee>
    {
        readonly ApplicationContext applicationContext;

        public EmployeeRepository(ApplicationContext context)
        {
            this.applicationContext = context;
        }

        public Employee Add(Employee employee)
        {
            this.applicationContext.Add(employee);
            this.applicationContext.SaveChanges();
            return employee;
        }

        public bool Delete(int id)
        {
            var employee = this.applicationContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                this.applicationContext.Remove<Employee>(employee);
                this.applicationContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Employee Get(int id)
        {
            return this.applicationContext.Employees.Include(p=>p.Department).Where(e => e.Id == id).FirstOrDefault();
        }

        public IEnumerable<Employee> GetAll()
        {
            return this.applicationContext.Employees.Include(p => p.Department).ToList();
        }

        public Employee Update(int id, Employee employee)
        {
            employee.Id = id;
            this.applicationContext.Update(employee);
            this.applicationContext.SaveChanges();
            return employee;
        }

        public bool IsExists(int id)
        {
            return this.applicationContext.Employees.Any(e => e.Id == id);
        }
    }
}
