using InlämningBank2.BankAppData;
using InlämningBank2.Infrastructure.Paging;
using InlämningBank2.ViewModel;

namespace InlämningBank2.Services
{
    
    public interface IAccountsService
    {
        PagedResult<Account> GetAccounts(string sortColumn, string sortOrder, string q, int pageNo);
        List<AccountViewModel> GetAccounts(int AccountId);
        List<Account> GetAccounts();
        void Update(Account account);
        Account GetAccount(int accountId);
        PagedResult<Transaction> GetTransactions(int accountId, int page);
        Transaction GetTransfer(int accountId, int accounttoId, decimal amount);
       
    }
}
