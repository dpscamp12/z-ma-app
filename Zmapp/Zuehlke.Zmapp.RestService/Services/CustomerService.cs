﻿using System;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Zuehlke.Zmapp.Services.Data;

namespace Zuehlke.Zmapp.RestService.Services
{
    #region Service Implementation

    public class CustomerService : Service
    {
        public IRepository Repository { get; set; }

        public object Get(GetCustomersRequest request)
        {
            var cacheKey = "AllCustomers";
            return base.RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheKey, TimeSpan.FromSeconds(15), () => this.Repository.GetCustomers());
        }

        public object Get(SearchCustomerByIdRequest request)
        {
            return this.Repository.GetCustomer(request.Id);
        }

        public object Get(SearchCustomersByNameRequest request)
        {
            return this.Repository.GetCustomers().Where(p => p.Name.Contains(request.Name));
        }

        public object Post(SaveCustomersRequest request)
        {
            this.Repository.SetCustomerBatch(request.Customers);

            this.RequestContext.RemoveFromCache(this.Cache, new[] { "AllCustomers" });

            return null;
        }

        public void Delete(DeleteCustomerByIdRequest request)
        {
            this.Repository.RemoveCustomer(request.Id);
        }

        public void Delete(DeleteCustomersByIdsRequest request)
        {
            foreach (var id in request.Ids)
            {
                this.Repository.RemoveCustomer(id);
            }
        }
    }

    #endregion

    #region Requests

    [Route("/Customers", "GET")]
    public class GetCustomersRequest : IReturn<List<Zmapp.Services.DomainModel.Customer>>
    {
    }

    [Route("/Customers/{Id}", "GET")]
    public class SearchCustomerByIdRequest : IReturn<Zmapp.Services.DomainModel.Customer>
    {
        public int Id { get; set; }
    }


    [Route("/Customers/search/{Name}", "GET")]
    public class SearchCustomersByNameRequest : IReturn<List<Zmapp.Services.DomainModel.Customer>>
    {
        public string Name { get; set; }
    }

    [Route("/Customers/Delete/{Id}", "DELETE")]
    public class DeleteCustomerByIdRequest : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Route("/Customers/Delete/", "DELETE")]
    public class DeleteCustomersByIdsRequest : IReturnVoid
    {
        public List<int> Ids { get; set; }
    }

    [Route("/Customers", "POST")]
    public class SaveCustomersRequest : IReturnVoid
    {
        public List<Zmapp.Services.DomainModel.Customer> Customers { get; set; }
    }

    #endregion
}