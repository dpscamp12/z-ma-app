
using System.Collections.Generic;
using System.Linq;

using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

using Zuehlke.Zmapp.RestService.Services;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Services.DomainModel;

namespace Zuehlke.Zmapp.Services.Client
{
    public class EmployeeServiceProxy : ServiceProxy<IEmployeeService>, IEmployeeService
    {
        public EmployeeServiceProxy()
            : base("EmployeeService")
        {
        }

        #region Implementation of IEmployeeService

        public EmployeeInfo[] GetEmployees()
        {
            return this.ExecuteRemoteCall((service) => service.GetEmployees());
        }

        public EmployeeInfo GetEmployee(int employeeId)
        {
            return this.ExecuteRemoteCall((service) => service.GetEmployee(employeeId));
        }

        public void SetEmployee(EmployeeInfo employee)
        {
            this.ExecuteRemoteCall((service) => service.SetEmployee(employee));
        }

        public void SetEmployees(EmployeeInfo[] employees)
        {
            this.ExecuteRemoteCall((service) => service.SetEmployees(employees));
        }

        public bool RemoveEmployee(int employeeId)
        {
            return this.ExecuteRemoteCall((service) => service.RemoveEmployee(employeeId));
        }

        #endregion
    }

    public class EmployeeServiceProxyRest : IEmployeeService
    {
        private const string ServiceUrl = "http://localhost:1337";

        #region Implementation of IEmployeeService

        public EmployeeInfo[] GetEmployees()
        {
            var client = (IRestClient)new JsonServiceClient(ServiceUrl);
            var result = client.Get(new GetEmployeesRequest());

            return result.Select(this.CreateEmployeeInfo).ToArray();
        }

        public EmployeeInfo GetEmployee(int employeeId)
        {
            return this.GetEmployees().FirstOrDefault(p => p.Id == employeeId);
        }

        public void SetEmployee(EmployeeInfo employee)
        {
            var employeeConverted = this.CreateEmployeeEntity(employee);

            var client = (IRestClient)new JsonServiceClient();
            client.Post(
                        new SaveEmployeesRequest
                            {
                                Employees = new List<Employee>
                                    {
                                        employeeConverted
                                    }
                            });
        }

        public void SetEmployees(EmployeeInfo[] customers)
        {
            var customersConverted = customers.Select(s => s != null ? this.CreateEmployeeEntity(s) : null).Where(p => p != null).ToList();

            var client = (IRestClient)new JsonServiceClient(ServiceUrl);
            client.Post(
                        new SaveEmployeesRequest
                            {
                                Employees = customersConverted
                            });
        }

        public bool RemoveEmployee(int employeeId)
        {
            var client = (IRestClient)new JsonServiceClient();
            client.Delete(
                          new DeleteCustomerByIdRequest
                              {
                                  Id = employeeId
                              });

            return true;
        }

        #endregion

        #region Helpers

        private Employee CreateEmployeeEntity(EmployeeInfo employeeInfo)
        {
            var employee = new Employee
                {
                    Id = employeeInfo.Id,
                    FirstName = employeeInfo.FirstName,
                    LastName = employeeInfo.LastName,
                    Street = employeeInfo.Street,
                    ZipCode = employeeInfo.ZipCode,
                    City = employeeInfo.City,
                    Phone = employeeInfo.Phone,
                    EMail = employeeInfo.EMail,
                    CareerLevel = employeeInfo.CareerLevel
                };

            if (employeeInfo.Skills != null)
            {
                employee.Skills.AddRange(employeeInfo.Skills);
            }

            return employee;
        }

        private EmployeeInfo CreateEmployeeInfo(Employee employee)
        {
            return new EmployeeInfo
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Street = employee.Street,
                ZipCode = employee.ZipCode,
                City = employee.City,
                Phone = employee.Phone,
                EMail = employee.EMail,
                CareerLevel = employee.CareerLevel,
                Skills = (employee.Skills ?? new List<Skill>()).ToArray()
            };
        }

        #endregion
    }
}