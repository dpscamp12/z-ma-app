using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

using Zuehlke.Zmapp.RestService.Customer;

namespace Zuehlke.Zmapp.RestService.Test
{
    [TestClass]
    public class UnitTest1
    {
        private const string ServiceUrl = "http://localhost:2222/";

        private static AppHost apphost;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            apphost = new AppHost();
            apphost.Init();
            apphost.Start(ServiceUrl);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            apphost.Stop();
        }

        [TestMethod]
        public void ChuckNorrisTest()
        {
            var client = (IRestClient)new JsonServiceClient(ServiceUrl);
            var customers = client.Get(new GetCustomersRequest());
            
            if (customers.Any(p => p.Id == 4000))
            {
                client.Delete(
                              new DeleteCustomerByIdRequest
                                  {
                                      Id = 4000
                                  });
            }

            customers = client.Get(new GetCustomersRequest());

            Assert.IsFalse(customers.Any(p => p.Id == 4000));

            client.Post(
                        new SaveCustomersRequest
                            {
                                Customers = new List<Services.DomainModel.Customer>
                                    {
                                        new Services.DomainModel.Customer
                                            {
                                                Id = 4000,
                                                City = "TestCity",
                                                Name = "TestName",
                                                Street = "TestStreet",
                                                ZipCode = "TestZipCode"
                                            }
                                    }
                            });

            customers = client.Get(new GetCustomersRequest());

            Assert.IsTrue(customers.Any(p => p.Id == 4000));

            var customer = customers.First(p => p.Id == 4000);
            
            Assert.AreEqual("TestCity", customer.City);
            Assert.AreEqual("TestName", customer.Name);
            Assert.AreEqual("TestStreet", customer.Street);
            Assert.AreEqual("TestZipCode", customer.ZipCode);
        }
    }
}
