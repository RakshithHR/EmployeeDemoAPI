using Microsoft.AspNetCore.JsonPatch;
using EmployeeAPIPOC.Models;
using System.Collections.Generic;

namespace EmployeeAPIPOC.IServices
{
    public interface IEmployeeService
    {
        List<Employee> Get();
        Employee GetBy(int id);
        List<Employee> Create(Employee employee);
        List<Employee> Update(Employee employee);
        List<Employee> Delete(int id);
        Employee UpdateByPatch(int id, JsonPatchDocument<Employee> document);
        bool IsValidUserInformation(LoginModel model);
        List<Employee> GetActiveOrDeactiveemployees(bool status);
    }
}
