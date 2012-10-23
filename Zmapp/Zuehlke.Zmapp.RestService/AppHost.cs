using ServiceStack.WebHost.Endpoints;
using Zuehlke.Zmapp.RestService.Customer;
using Zuehlke.Zmapp.Services.Data;

namespace Zuehlke.Zmapp.RestService
{
    public class AppHost : AppHostHttpListenerBase
    {
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
