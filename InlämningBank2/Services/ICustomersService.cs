using InlämningBank2.BankAppData;
using InlämningBank2.Infrastructure.Paging;
using InlämningBank2.ViewModel;

namespace InlämningBank2.Services
{
    public interface ICustomersService
    {
        PagedResult<Customer> GetCustomers(string sortColumn, string sortOrder, string q, int pageNo);

        CustomerViewModel GetCustomer(int customerId);
    }
}
