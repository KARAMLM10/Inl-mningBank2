using InlämningBank2.BankAppData;
using InlämningBank2.Infrastructure.Paging;

namespace InlämningBank2.Services
{
    public interface IAccountsService
    {
        PagedResult<Account> GetAccounts(string sortColumn, string sortOrder, string q, int pageNo);
    }
}
