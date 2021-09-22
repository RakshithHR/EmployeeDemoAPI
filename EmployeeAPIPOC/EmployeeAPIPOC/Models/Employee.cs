using System.ComponentModel.DataAnnotations;

namespace EmployeeAPIPOC
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpDesignation { get; set; }
        public int Salary { get; set; }
        public bool Status { get; set; }
    }
}
