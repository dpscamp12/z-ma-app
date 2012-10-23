using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Redis;
using ServiceStack.WebHost.Endpoints;

using Zuehlke.Zmapp.RestService.Services;
using Zuehlke.Zmapp.Services.Data;

namespace Zuehlke.Zmapp.RestService
{
    public class RestServiceHost : AppHostHttpListenerBase
    {
        public RestServiceHost()
            : base("Zmapp REST Service", typeof(CustomerService).Assembly) 
        {
        }

        public override void Configure(Funq.Container container)
        {
            container.Register<IRepository>(Repository.Instance);
            //container.Register<IRedisClientsManager>(c => new PooledRedisClientManager("localhost:6379"));
            //container.Register<ICacheClient>(c => (ICacheClient)c.Resolve<IRedisClientsManager>().GetCacheClient());
            container.Register<ICacheClient>(new MemoryCacheClient());
        }
    }
}
