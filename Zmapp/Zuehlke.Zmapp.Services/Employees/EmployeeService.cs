using System;
using System.Collections.Generic;
using System.Linq;
using Zuehlke.Zmapp.Services.Contracts.Employee;

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

		public ReservationInfo[] GetReservationsOfEmployee(int employeeId)
		{
			Employee employee = this.repository.GetEmployee(employeeId);

			return Enumerable.Select<Reservation, ReservationInfo>(employee.Reservations, CreateReservationInfo)
				.ToArray();
		}

		public void SetReservationsOfEmployee(int employeeId, ReservationInfo[] reservations)
		{
			Employee employeeEntity = this.repository.GetEmployee(employeeId);

			IEnumerable<Reservation> reservationEntities = reservations.Select(this.CreateReservationEntity);
			employeeEntity.Reservations.AddRange(reservationEntities);

			this.repository.SetEmployee(employeeEntity);
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

		private Reservation CreateReservationEntity(ReservationInfo reservation)
		{
			return new Reservation
			{
				CustomerId = reservation.CustomerId,
				Start = reservation.Start,
				End = reservation.End
			};
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

		private static ReservationInfo CreateReservationInfo(Reservation reservation)
		{
			return new ReservationInfo
			{
				CustomerId = reservation.CustomerId,
				Start = reservation.Start,
				End = reservation.End
			};
		}
	}
}