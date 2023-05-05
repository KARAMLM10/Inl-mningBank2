using InlämningBank2.BankAppData;
using InlämningBank2.Infrastructure.Paging;
using InlämningBank2.ViewModel;

namespace InlämningBank2.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly BankAppDataContext _dbContext;
        public AccountsService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
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

            var Accounts = query.Select(c => new AccountsViewModel
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
