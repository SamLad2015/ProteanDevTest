using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;

namespace DeveloperTest.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public CustomerModel[] GetCustomers()
        {
            return context.Customers
                .Join(context.CustomerTypes, 
                    customer => customer.CustomerTypeId,
                    type => type.CustomerTypeId,
                    (c, t) => new CustomerModel
                    {
                        CustomerId = c.CustomerId,
                        Name = c.Name,
                        CustomerType = t.Description
                    }
                ).ToArray();
        }

        public CustomerTypeModel[] GetCustomerTypes()
        {
            return context.CustomerTypes.Select(x => new CustomerTypeModel
            {
                CustomerTypeId = x.CustomerTypeId,
                Description = x.Description
            }).ToArray();
        }

        public CustomerModel GetCustomer(int customerId)
        {
            return context.Customers
                .Join(context.CustomerTypes, 
                    customer => customer.CustomerTypeId,
                    type => type.CustomerTypeId,
                    (c, t) => new CustomerModel
                    {
                        CustomerId = c.CustomerId,
                        Name = c.Name,
                        CustomerType = t.Description
                    }
                ).SingleOrDefault(c => c.CustomerId == customerId);
        }

        public CustomerModel CreateCustomer(CustomerRequestModel model)
        {
            var addedCustomer = context.Customers.Add(new Customer
            {
                Name = model.Name,
                CustomerTypeId = model.CustomerTypeId
            });

            context.SaveChanges();

            return new CustomerModel
            {
                CustomerId = addedCustomer.Entity.CustomerId,
                Name = addedCustomer.Entity.Name,
                CustomerType = context.CustomerTypes
                    .FirstOrDefault(t => t.CustomerTypeId == addedCustomer.Entity.CustomerTypeId)
                    .Description
            };
        }
    }
}
