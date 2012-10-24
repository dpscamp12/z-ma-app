using System;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Services.Data;
using Zuehlke.Zmapp.Services.DomainModel;

namespace Zuehlke.Zmapp.RestService.Services
{
    #region Service Implementation

    public class EmployeeService : Service
    {
        public class CustomerService : Service
        {
            public IRepository Repository { get; set; }

            #region HTTP Verbs

            public object Get(GetEmployeesRequest request)
            {
                const string cacheKey = "AllEmployees";
                return this.RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheKey, TimeSpan.FromSeconds(15), () => this.Repository.GetEmployees());
            }

            public object Get(SearchEmployeeByIdRequest request)
            {
                return this.Repository.GetEmployees();
            }

            public object Get(SearchEmployeesByFirstNameRequest request)
            {
                return this.Repository.GetEmployees().Where(p => p.FirstName.Contains(request.FirstName));
            }

            public object Post(SaveEmployeesRequest request)
            {
                this.Repository.SetEmployeeBatch(request.Employees);

                this.RequestContext.RemoveFromCache(this.Cache, new[] { "AllEmployees" });

                return null;
            }

            public void Delete(DeleteEmployeeByIdRequest request)
            {
                this.Repository.RemoveEmployee(request.Id);
            }

            public void Delete(DeleteEmployeesByIdsRequest request)
            {
                foreach (var id in request.Ids)
                {
                    this.Repository.RemoveEmployee(id);
                }
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

            private static EmployeeInfo CreateEmployeeInfo(Employee employee)
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

            private Reservation CreateReservationEntity(ReservationInfo reservation)
            {
                return new Reservation
                {
                    CustomerId = reservation.CustomerId,
                    Start = reservation.Start,
                    End = reservation.End
                };
            }

            #endregion
        }
    }

    #endregion

    #region Requests

    [Route("/Employees", "GET")]
    public class GetEmployeesRequest : IReturn<List<Employee>>
    {
    }

    [Route("/Employees/{Id}", "GET")]
    public class SearchEmployeeByIdRequest : IReturn<Employee>
    {
        public int Id { get; set; }
    }


    [Route("/Employees/search/{Name}", "GET")]
    public class SearchEmployeesByFirstNameRequest : IReturn<List<Employee>>
    {
        public string FirstName { get; set; }
    }

    [Route("/Employees/Delete/{Id}", "DELETE")]
    public class DeleteEmployeeByIdRequest : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Route("/Employees/Delete/", "DELETE")]
    public class DeleteEmployeesByIdsRequest : IReturnVoid
    {
        public List<int> Ids { get; set; }
    }

    [Route("/Employees", "POST")]
    public class SaveEmployeesRequest : IReturnVoid
    {
        public List<Zuehlke.Zmapp.Services.DomainModel.Employee> Employees { get; set; }
    }

    #endregion
}
