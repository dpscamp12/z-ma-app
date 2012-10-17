using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml.Serialization;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Services.Test
{
	[TestClass]
	public class EmployeeServiceTest
	{
		[TestMethod]
		public void SkillTest()
		{
			var e = new Employee();
			e.Skills.AddRange(new[] { Skill.SqlServer, Skill.CloudComputing, });


			Assert.IsTrue(e.HasSkill(Skill.SqlServer));
			Assert.IsTrue(e.HasSkill(Skill.CloudComputing));
			Assert.IsFalse(e.HasSkill(Skill.WCF));
		}

		[TestMethod]
		public void NoSkillTest()
		{
			var e = new Employee();

			Assert.IsFalse(e.HasSkill(Skill.WCF));
		}

		[TestMethod]
		public void IsAvailableTest()
		{
			var reservations = new[]
								   {
									   new Reservation { Start = new DateTime(2014,1,1), End = new DateTime(2014,1,10)},
									   new Reservation { Start = new DateTime(2013,12,21), End = new DateTime(2014,1,4)},
									   new Reservation { Start = new DateTime(2014,2,1), End = new DateTime(2014,5,30)},
								   };
			var e = new Employee();
			e.Reservations.AddRange(reservations);

			Assert.IsTrue(e.IsAvailable(new DateTime(2012, 1, 1)));
			Assert.IsTrue(e.IsAvailable(new DateTime(2013, 12, 20)));
			Assert.IsFalse(e.IsAvailable(new DateTime(2013, 12, 21)));
			Assert.IsFalse(e.IsAvailable(new DateTime(2014, 1, 1)));
			Assert.IsFalse(e.IsAvailable(new DateTime(2014, 1, 10)));
			Assert.IsTrue(e.IsAvailable(new DateTime(2014, 1, 11)));
			Assert.IsTrue(e.IsAvailable(new DateTime(2014, 1, 31)));
			Assert.IsFalse(e.IsAvailable(new DateTime(2014, 3, 5)));
		}

		[TestMethod]
		public void HasAnyAvailableTimeTest()
		{
			var reservations = new[]
								   {
									   new Reservation { Start = new DateTime(2014,1,1), End = new DateTime(2014,1,10)},
									   new Reservation { Start = new DateTime(2013,12,21), End = new DateTime(2014,1,4)},
									   new Reservation { Start = new DateTime(2014,2,1), End = new DateTime(2014,5,30)},
								   };
			var e = new Employee();
			e.Reservations.AddRange(reservations);

			Assert.IsTrue(e.HasAnyAvailableTime(new DateTime(2010, 1, 1), new DateTime(2011, 1, 1)));
			Assert.IsTrue(e.HasAnyAvailableTime(new DateTime(2013, 1, 1), new DateTime(2014, 1, 1)));
			Assert.IsFalse(e.HasAnyAvailableTime(new DateTime(2013, 12, 21), new DateTime(2013, 12, 21)));
			Assert.IsFalse(e.HasAnyAvailableTime(new DateTime(2013, 12, 21), new DateTime(2014, 1, 1)));
			Assert.IsFalse(e.HasAnyAvailableTime(new DateTime(2013, 12, 25), new DateTime(2014, 1, 9)));
			Assert.IsTrue(e.HasAnyAvailableTime(new DateTime(2013, 12, 25), new DateTime(2014, 1, 12)));
			Assert.IsTrue(e.HasAnyAvailableTime(new DateTime(2013, 12, 25), new DateTime(2015, 1, 12)));
			Assert.IsTrue(e.HasAnyAvailableTime(new DateTime(2014, 1, 8), new DateTime(2014, 1, 12)));
			Assert.IsTrue(e.HasAnyAvailableTime(new DateTime(2014, 1, 31), new DateTime(2014, 2, 5)));
			Assert.IsTrue(e.HasAnyAvailableTime(new DateTime(2014, 1, 31), new DateTime(2014, 7, 5)));
			Assert.IsFalse(e.HasAnyAvailableTime(new DateTime(2014, 3, 15), new DateTime(2014, 4, 15)));
			Assert.IsTrue(e.HasAnyAvailableTime(new DateTime(2014, 3, 15), new DateTime(2015, 7, 5)));
		}

		[TestMethod]
		public void FindEmployees_FromDateAndEndDateAreEqual_FilterCorrectlyAppliedToOneDay()
		{
			//var service = new EmployeeEvaluationService();
			//service.FindEmployees()

			Employee employee = new Employee();
			employee.Reservations.Add(new Reservation() { CustomerId = 1, End = DateTime.Now, Start = DateTime.Now });

			var serializer = new XmlSerializer(typeof(Employee));
			using (TextWriter writer = new StreamWriter(@"C:\Temp\employee.xml"))
			{
				serializer.Serialize(writer, employee);
			}
		}
	}
}