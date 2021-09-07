using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;

namespace Employee.Core
{
    public class Employee
    {
        public static List<Employee> Employees = new List<Employee>();
        public string EmployeeId { get; set; }
        public string ManagerId { get; set; }
        public decimal Salary { get; set; }
        public List<Employee> EmployeesList { get; set; }

        public Employee(string[] csvstring)
        {
            EmployeesList = ReadCsv(csvstring);
            CircularReferenceValidate();
        }

        public Employee()
        {
        }

        public static List<Employee> ReadCsv(string[] csvString)
        {
            try
            {
                /// string[] data = System.IO.File.ReadAllLines(@"D:\Projects\Others\TecnoBrain\Employee.Core\Data\employee.csv");

                foreach (var dataRow in csvString)
                {
                    string[] rowData = dataRow.Split(',');
                    if (Validate(rowData) && rowData.Length >= 1)
                    {
                        var emp = new Employee
                        {
                            EmployeeId = rowData[0],
                            ManagerId = rowData[1],
                            Salary = Convert.ToDecimal(rowData[2])
                        };
                        Employees.Add(emp);
                    }
                }

                return Employees;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public static bool Validate(string[] csvRow)
        {
            bool resp = false;
            //check duplicate
            var duplicate = Employees.Where(e => e.EmployeeId == csvRow[0]).ToList();
            //check salary is a valid int
            if (int.TryParse(csvRow[2], out int n) && duplicate.Count <= 0)
            {
                resp = true;
            }
            return resp;
        }

        public decimal SalaryBudget(string managerId)
        {
            decimal budget = 0;
            var emp = Employees.FirstOrDefault(i => i.EmployeeId.Trim() == managerId.Trim());
            if (emp == null) return 0;
            budget += emp.Salary;
            budget += Employees.Where(i => i.ManagerId == managerId).Sum(item => SalaryBudget(item.EmployeeId));
            return budget;
        }

        private void CircularReferenceValidate()
        {
            for (int i = Employees.Count - 1; i >= 0; i--)
            {
                var item = Employees[i];
                Employees.RemoveAll(m => m.ManagerId == item.EmployeeId && m.EmployeeId == item.ManagerId);
                if (!string.IsNullOrEmpty(item.ManagerId))
                {
                    ValidateManager(item.ManagerId);
                }
            }
        }

        private void ValidateManager(string manager_Id)
        {
            if (Employees.FirstOrDefault(i => i.EmployeeId.Trim() == manager_Id.Trim()) == null)
            {
                Employees.RemoveAll(i => i.ManagerId.Trim() == manager_Id.Trim());
            }
        }
    }
}