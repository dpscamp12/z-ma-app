using System;
using System.Linq;

using Zuehlke.Zmapp.Services.Contracts.Employees;

namespace Zuehlke.Zmapp.Services.Employees
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IRepository repository;

		public EmployeeService()
			: this(Repository.Instance)
		{
		}

		public EmployeeService(IRepository repository)
		{
			if (repository == null) throw new ArgumentNullException("repository");

			this.repository = repository;
		}

		#region Implementation of IEmployeeService
		public EmployeeInfo[] GetEmployees()
		{
			return this.repository.GetEmployees()
				.Select(CreateEmployeeInfo)
				.ToArray();
		}

		public EmployeeInfo GetEmployee(int employeeId)
		{
			Employee employee = this.repository.GetEmployee(employeeId);

			return CreateEmployeeInfo(employee);
		}

		public void SetEmployee(EmployeeInfo employee)
		{
			Employee employeeEntity = this.CreateEmployeeEntity(employee);
			this.repository.SetEmployee(employeeEntity);
		}

		public bool RemoveEmployee(int employeeId)
		{
			return this.repository.RemoveEmployee(employeeId);
		}

		#endregion

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
				Skills = employee.Skills.ToArray()
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
	}
}