using InlämningBank2.BankAppData;
using InlämningBank2.Infrastructure.Paging;
using InlämningBank2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InlämningBank2.Services
{
    public enum errorcodeenum
    {
        Invalidaccount
    }
    public class AccountsService : IAccountsService
    {
        private readonly BankAppDataContext _dbContext;
        public AccountsService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public List<Account> GetAccounts()
        {
            //throw new NotImplementedException();
            return _dbContext.Accounts.ToList();
        }
        public Account GetAccount(int accountId)
        {
            //throw new NotImplementedException();
            return _dbContext.Accounts.First(a => a.AccountId == accountId);
        }
        public void Update(Account account)
        {
            //throw new NotImplementedException();
            _dbContext.SaveChanges();
        }


        public List<AccountViewModel> GetAccounts(int customerId)
        {
            //throw new NotImplementedException();
            var accounts = _dbContext.Dispositions
                .Include(x => x.Customer)
                .Include(a => a.Account)
                .Where(c => c.CustomerId == customerId).Select(a => a.Account).AsQueryable();
            if (accounts == null)
            {
                // Handle the case when the customer is not found
                return null; // or throw an exception, return a default value, etc.
                
            }
            var accountsViewModel = new List<AccountViewModel>();
            foreach (var accouunt in accounts)
            {
                var accountViewModel = new AccountViewModel
                {
                    AccountId = accouunt.AccountId,
                    Frequency = accouunt.Frequency,
                    Created = accouunt.Created,
                    Balans = accouunt.Balance,
                };
                accountsViewModel.Add(accountViewModel);
            }

            return accountsViewModel;
        }

        public PagedResult<Account> GetAccounts(string sortColumn, string sortOrder, string q, int pageNo)
        {


            var query = _dbContext.Accounts.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query
                    .Where(p => p.AccountId.Equals(q) ||
                    p.Frequency.Contains(q) ||
                    p.Created.Equals(q));
            }
            if (sortColumn == "AccountId")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.AccountId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.AccountId);
            if (sortColumn == "Frequency")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Frequency);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Frequency);

            var Accounts = query.Select(c => new AccountViewModel
            {
                AccountId = c.AccountId,
                Frequency = c.Frequency,
                Created = c.Created,
                Balans = c.Balance

            }).ToList();

            return query.GetPaged(pageNo, 20);


        }

        public PagedResult<Transaction> GetTransactions(int accountId, int page) 
        {
            var transactions = _dbContext.Accounts
                .Include(t => t.Transactions
                .Where(t=>t.AccountId == accountId))
                .SelectMany(t => t.Transactions)
                .OrderByDescending(s=>s.TransactionId);
            return transactions.GetPaged(page,10);

        }

        public Transaction GetTransfer(int accountId, int accounttoId, decimal amount)
        {

            var account = _dbContext.Accounts
                .Include(d => d.Dispositions)
                .ThenInclude(a => a.Customer)
                .First(a => a.AccountId == accountId);
            var accountto = _dbContext.Accounts
                .Include(d => d.Dispositions)
                .ThenInclude(a => a.Customer)
                .First(a => a.AccountId == accounttoId);
            account.Balance -= amount;
            accountto.Balance += amount;
            string messege = "transfer";
            _dbContext.Transactions.Add(new Transaction
            {
                AccountId = accountId,
                Date = DateTime.Now,
                Type = "Debit",
                Operation = messege,
                Amount = -amount,
                Balance = account.Balance,
                Account = accountto.AccountId.ToString(),
            });
            _dbContext.Transactions.Add(new Transaction
            {
                AccountId = accounttoId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = messege,
                Amount = amount,
                Balance = accountto.Balance,
                Account = accountto.AccountId.ToString(),
            });
            _dbContext.SaveChanges();
            return _dbContext.Transactions.First();
        }

       
    }
}
