using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Zuehlke.Zmapp.RestService.Helpers;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Services.Data;
using Zuehlke.Zmapp.Services.DomainModel;

namespace Zuehlke.Zmapp.RestService.Services
{
    public class EmployeeReservationService : Service
    {
        #region Service Implementation

        public IRepository Repository { get; set; }

        #region HTTP Verbs

        public object Get(EmployeeQueryRequest request)
        {
            var query = new EmployeeQuery
                {
                    BeginOfWorkPeriod = request.BeginOfWorkPeriod,
                    CustomerId = request.CustomerId,
                    EndOfWorkPeriod = request.EndOfWorkPeriod,
                    RequestedCareerLevels = request.RequestedCareerLevels,
                    RequestedSkills = request.RequestedSkills
                };

            IEnumerable<Employee> foundEmployees = this.FindEmployees(query);

            return foundEmployees
                .Select(
                        e => new EmployeeSearchResult
                            {
                                Id = e.Id,
                                Distance = 10.1F,
                                EmployeeName = String.Format("{0} {1}", e.FirstName, e.LastName),
                                Skills = e.Skills.ToArray(),
                                Level = e.CareerLevel
                            })
                .ToArray();
        }

        public void Post(ReserveEmployeeForCustomerRequest request)
        {
            var employee = this.Repository.GetEmployee(request.EmployeeId);
            if (employee == null)
            {
                var error = String.Format("Employee with Id '{0}' not found.", request.EmployeeId);
                throw new ArgumentException(error, "employeeId");
            }

            employee.Reservations.Add(new Reservation() { CustomerId = request.CustomerId, Start = request.BeginOfPeriod, End = request.EndOfPeriod });

            this.Repository.SetEmployee(employee);
        }

        public object Get(GetReservationsOfEmployeeRequest request)
        {
            Employee employee = this.Repository.GetEmployee(request.EmployeeId);
            if (employee == null)
            {
                var error = String.Format("Employee with Id '{0}' not found.", request.EmployeeId);
                throw new ArgumentException(error, "employeeId");
            }

            return employee.Reservations
                .Select(CreateReservationInfo)
                .ToArray();
        }

        public void Post(SetReservationsOfEmployeeRequest request)
        {
            if (request.Reservations == null) throw new ArgumentNullException("reservations");

            Employee employeeEntity = this.Repository.GetEmployee(request.EmployeeId);

            if (employeeEntity == null)
            {
                var error = String.Format("Employee with Id '{0}' not found.", request.EmployeeId);
                throw new ArgumentException(error, "employeeId");
            }

            IEnumerable<Reservation> reservationEntities = request.Reservations;
            employeeEntity.Reservations.Clear();
            employeeEntity.Reservations.AddRange(reservationEntities);

            this.Repository.SetEmployee(employeeEntity);
        }

        #endregion

        #region Helpers

        private IEnumerable<Employee> FindEmployees(EmployeeQuery query)
        {
            return EmployeeReservationServiceHelper.FindEmployees(this.Repository.GetEmployees(), query);
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

        #endregion

        #endregion
    }

    #region Requests

    [Route("/Reservation/Query", "GET")]
    public class EmployeeQueryRequest : IReturn<List<EmployeeSearchResult>>
    {
        public EmployeeQueryRequest()
        {
            this.RequestedSkills = new Skill[0];
            this.RequestedCareerLevels = new CareerLevel[0];
        }
        public int CustomerId { get; set; }

        public Skill[] RequestedSkills { get; set; }

        public CareerLevel[] RequestedCareerLevels { get; set; }

        public DateTime BeginOfWorkPeriod { get; set; }

        public DateTime EndOfWorkPeriod { get; set; }
    }

    [Route("/Reservation", "POST")]
    public class ReserveEmployeeForCustomerRequest : IReturnVoid
    {
        public int EmployeeId { get; set; }

        public int CustomerId { get; set; }

        public DateTime BeginOfPeriod { get; set; }

        public DateTime EndOfPeriod { get; set; }
    }

    [Description("Get reservations by employee")]
    [Route("/Reservation/ByEmployee/{EmployeeId}", "GET")]
    public class GetReservationsOfEmployeeRequest : IReturn<List<ReservationInfo>>
    {
        public int EmployeeId { get; set; }
    }

    [Route("/Reservation", "POST")]
    public class SetReservationsOfEmployeeRequest : IReturnVoid
    {
        public int EmployeeId { get; set; }
        public Reservation[] Reservations { get; set; }
    }

    #endregion
}
