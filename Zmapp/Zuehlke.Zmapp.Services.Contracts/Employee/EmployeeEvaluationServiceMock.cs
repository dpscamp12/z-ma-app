using System;

namespace Zuehlke.Zmapp.Services.Contracts.Employee
{
	public class EmployeeEvaluationServiceMock : IEmployeeEvaluationService
	{
		public EmployeeSearchResult[] FindPotentialEmployeesForCustomer(EmployeeQuery query)
		{
			var e = new EmployeeSearchResult()
						{
							Distance = 10.3f,
							EmployeeName = "Hans Wurst",
							Id = 1,
							Level = CareerLevel.SoftwareEngineer,
							Skills = new[] { Skill.WCF, Skill.AspDotNet, }
						};

			var f = new EmployeeSearchResult()
			{
				Distance = 5.1f,
				EmployeeName = "Super Dev",
				Id = 1,
				Level = CareerLevel.JuniorSoftwareEngineer,
				Skills = new[] { Skill.WCF, Skill.CSharp, }
			};

			return new[] { e, f };
		}

		public void ReserveEmployeeForCustomer(int employeeId, int customerId, DateTime beginOfPeriod, DateTime endOfPeriod)
		{
			throw new NotImplementedException();
		}
	}
}