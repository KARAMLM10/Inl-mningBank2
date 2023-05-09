using InlämningBank2.BankAppData;
using InlämningBank2.Infrastructure.Paging;
using InlämningBank2.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace InlämningBank2.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly BankAppDataContext _dbContext;
        public AccountsService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
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
                //github fel
                //github fel
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



        //    public List<AccountsViewModel> GetAccounts(string sortColumn, string sortOrder)
        //{
        //    //throw new NotImplementedException();
        //    var query = _dbContext.Accounts.AsQueryable();
        //    if (sortColumn == "AccountId")
        //        if (sortOrder == "asc")
        //            query = query.OrderBy(c => c.AccountId);
        //        else if (sortOrder == "desc")
        //            query = query.OrderByDescending(c => c.AccountId);
        //    if (sortColumn == "Frequency")
        //        if (sortOrder == "asc")
        //            query = query.OrderBy(c => c.Frequency);
        //        else if (sortOrder == "desc")
        //            query = query.OrderByDescending(c => c.Frequency);

        //    var Accounts = query.Select(c => new AccountsViewModel
        //    {
        //        AccountId = c.AccountId,
        //        Frequency = c.Frequency,
        //        Created = c.Created,
        //        Balans = c.Balance

        //    }).Take(8).ToList();
        //    return Accounts;
        //}


    }
}
