using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class EmployeeWithDepartmentSpecifiaction : BaseSpecifications<Employee>
    {
        public EmployeeWithDepartmentSpecifiaction() 
        {
            Includes.Add(E => E.Department);
        }
        public EmployeeWithDepartmentSpecifiaction(int id) : base(E => E.Id == id)
        {
            Includes.Add(E => E.Department);
        }
    }
}
