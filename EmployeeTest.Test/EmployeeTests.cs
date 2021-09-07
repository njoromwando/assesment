using System;
using System.Linq;
using Employee.Core;
using System.Text;
using Xunit;

namespace EmployeeTest.Test
{
    public class EmployeeTests
    {
        [Theory]
        [InlineData(new[] { "Employee4,Employee1,1000h", "Employee1,,1000" }, 1)]
        public void EmployeeTestCsvValidateInt(string[] csvStrings, int expected)
        {
            var employee = new Employee.Core.Employee(csvStrings).EmployeesList.Count;
            Assert.Equal(employee, expected);
        }

        [Fact]
        public void EmployeeTestCsvMapping()
        {
            var testData = new[] { "Employee1,,1000" };
            var employee = new Employee.Core.Employee(testData);
            Assert.Equal(1000, employee.EmployeesList[0].Salary);
        }

        [Theory]
        [InlineData("Employee4", 500)]
        [InlineData("Employee1", 3300)]
        [InlineData("Employee11", 0)]
        public void EmployeeTestCsvTestSalaryBudget(string managerId, long expected)
        {
            var testData = new[] { "Employee4,Employee2,500", "Employee3,Employee1,800", "Employee1,,1000", "Employee5,Employee1,500", "Employee2,Employee1,500" };
            var employee = new Employee.Core.Employee(testData);
            var salary = employee.SalaryBudget(managerId);
            Assert.Equal(expected, salary);
        }
    }
}