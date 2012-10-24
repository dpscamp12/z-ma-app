using System;
using System.Linq;

using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

using Zuehlke.Zmapp.RestService.Services;
using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.Services.DomainModel;

namespace Zuehlke.Zmapp.Services.Client
{
	public class EmployeeReservationServiceProxy : ServiceProxy<IEmployeeReservationService>, IEmployeeReservationService
	{
		public EmployeeReservationServiceProxy()
			: base("EmployeeReservationService")
		{
		}

		#region IEmployeeReservationService members
		public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
		{
			return this.ExecuteRemoteCall((service) => service.FindPotentialEmployeesForCustomer(query));
		}

		public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			this.ExecuteRemoteCall((service) => service.ReserveEmployeeForCustomer(employeeId, customerId, beginOfPeriod, endOfPeriod));
		}

		public ReservationInfo[] GetReservationsOfEmployee(int employeeId)
		{
			return this.ExecuteRemoteCall((service) => service.GetReservationsOfEmployee(employeeId));
		}

		public void SetReservationsOfEmployee(int employeeId, ReservationInfo[] reservations)
		{
			this.ExecuteRemoteCall((service) => service.SetReservationsOfEmployee(employeeId, reservations));
		}
		#endregion
	}

    public class EmployeeReservationServiceProxyRest : IEmployeeReservationService
    {
        #region Implementation of IEmployeeReservationService

        private const string ServiceUrl = "http://localhost:1337";

        public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
        {
            var client = (IRestClient)new JsonServiceClient(ServiceUrl);

            var request = new EmployeeQueryRequest
                {
                    BeginOfWorkPeriod = query.BeginOfWorkPeriod,
                    CustomerId = query.CustomerId,
                    EndOfWorkPeriod = query.EndOfWorkPeriod,
                    RequestedCareerLevels = query.RequestedCareerLevels,
                    RequestedSkills = query.RequestedSkills
                };

            return client.Get(request).ToArray();
        }

        public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
        {
            var client = (IRestClient)new JsonServiceClient(ServiceUrl);

            var request = new ReserveEmployeeForCustomerRequest
                {
                    CustomerId = customerId,
                    EmployeeId = employeeId,
                    BeginOfPeriod = beginOfPeriod,
                    EndOfPeriod = endOfPeriod,
                };

            client.Post(request);
        }

        public ReservationInfo[] GetReservationsOfEmployee(int employeeId)
        {
            var client = (IRestClient)new JsonServiceClient(ServiceUrl);

            var request = new GetReservationsOfEmployeeRequest
                {
                    EmployeeId = employeeId
                };

            return client.Get(request).ToArray();
        }

        public void SetReservationsOfEmployee(int employeeId, ReservationInfo[] reservations)
        {
            var client = (IRestClient)new JsonServiceClient(ServiceUrl);

            var request = new SetReservationsOfEmployeeRequest
                {
                    EmployeeId = employeeId,
                    Reservations = reservations.Select(
                                                       s => new Reservation
                                                           {
                                                               CustomerId = s.CustomerId,
                                                               End = s.End,
                                                               Start = s.Start
                                                           }).ToArray()
                };

            client.Post(request);
        }

        #endregion
    }
}