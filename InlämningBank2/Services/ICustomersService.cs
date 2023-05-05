using InlämningBank2.BankAppData;
using InlämningBank2.Infrastructure.Paging;

namespace InlämningBank2.Services
{
    public interface ICustomersService
    {
        PagedResult<Customer> GetCustomers(string sortColumn, string sortOrder, string q, int pageNo);
    }
}
