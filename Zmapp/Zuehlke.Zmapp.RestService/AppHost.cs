using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Configuration;
using ServiceStack.WebHost.Endpoints;
using Zuehlke.Zmapp.RestService.Customer;
using Zuehlke.Zmapp.Services.Data;

namespace Zuehlke.Zmapp.RestService
{
    public class AppHost : AppHostHttpListenerBase
    {
        static readonly ConfigurationResourceManager AppSettings = new ConfigurationResourceManager();

        public AppHost()
            : base("Zmapp REST Service", typeof(CustomerService).Assembly) 
        {
        }

        public override void Configure(Funq.Container container)
        {
            container.Register<IRepository>(Repository.Instance);
        }
    }
}
