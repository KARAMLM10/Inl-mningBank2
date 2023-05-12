using DBContextLibrary.BankAppData;
using DBContextLibrary.Infrastructure.Paging;
using DBContextLibrary.ViewModel;

namespace ServiceLibrary
{
    public interface ICustomersService
    {
        PagedResult<Customer> GetCustomers(string sortColumn, string sortOrder, string q, int pageNo);

        CustomerViewModel GetCustomer(int customerId);
        

    }
}
