using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Services.Data;
using Zuehlke.Zmapp.Services.DomainModel;

namespace Zuehlke.Zmapp.Services.Employees
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class EmployeeReservationService : IEmployeeReservationService
	{
		private readonly IRepository repository;

		public EmployeeReservationService()
			: this(Repository.Instance)
		{
		}

		internal EmployeeReservationService(IRepository repository)
		{
			if (repository == null) throw new ArgumentNullException("repository");

			this.repository = repository;
		}

		#region IEmployeeReservationService members

		public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
		{
			IEnumerable<Employee> foundEmployees = this.FindEmployees(query);
			return foundEmployees
				.Select(e => new EmployeeSearchResult
								 {
									 Id = e.Id,
									 Distance = 10.1F,
									 EmployeeName = String.Format("{0} {1}", e.FirstName, e.LastName),
									 Skills = e.Skills.ToArray(),
									 Level = e.CareerLevel
								 })
				.ToArray();
		}

		public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			var employee = this.repository.GetEmployee(employeeId);

			if (employee == null)
			{
				var error = String.Format("Employee with Id '{0}' not found.", employeeId);
				throw new ArgumentException(error, "employeeId");
			}

			employee.Reservations.Add(new Reservation() { CustomerId = customerId, Start = beginOfPeriod, End = endOfPeriod });

			this.repository.SetEmployee(employee);
		}

		public ReservationInfo[] GetReservationsOfEmployee(int employeeId)
		{
			Employee employee = this.repository.GetEmployee(employeeId);

			return employee.Reservations
				.Select(CreateReservationInfo)
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

		private IEnumerable<Employee> FindEmployees(EmployeeQuery query)
		{
			return EmployeeReservationServiceHelper.FindEmployees(this.repository.GetEmployees(), query);
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