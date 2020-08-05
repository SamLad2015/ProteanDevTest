using DeveloperTest.Models;

namespace DeveloperTest.Business.Interfaces
{
    public interface ICustomerService
    {
        CustomerModel[] GetCustomers();
        
        CustomerTypeModel[] GetCustomerTypes();

        CustomerModel GetCustomer(int jobId);

        CustomerModel CreateCustomer(CustomerRequestModel model);
    }
}
