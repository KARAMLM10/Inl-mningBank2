using InlämningBank2.Services;
using InlämningBank2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace InlämningBank2.Pages
{
    [Authorize(Roles = "Admin")]
    public class AccountsModel : PageModel
    {
        public AccountsModel(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        //private readonly IAccountsService accountsService;
        private readonly IAccountsService _accountsService;

        public List<AccountsViewModel> Accounts { get; set; }
        public string Q { get; set; }
        public int PageCount { get; set; }
        public int AccountId { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int CurrentPage { get; set; }

        public void OnGet(string sortColumn, string sortOrder, string q, int pageNo)
        {
            if (pageNo < 1)
            {
                pageNo = 1;
            }
            Q = q;
            SortColumn = sortColumn;
            SortOrder = sortOrder;            
            PageCount = pageNo;
            CurrentPage = pageNo;
            var result = _accountsService.GetAccounts(sortColumn, sortOrder, q, pageNo);
            PageCount = result.PageCount;

            Accounts = result.Results.Select(c => new AccountsViewModel
            {
                AccountId = c.AccountId,
                Frequency = c.Frequency,
                Created = c.Created,
                Balans = c.Balance
            }).ToList();
            //Accounts = _accountsService.GetAccounts(SortColumn, sortOrder);

            //Accounts = _dbContext.Accounts.Select(x => new AccountsViewModel
            //{
            //    AccountId = x.AccountId,
            //    Frequency = x.Frequency,
            //    Created = DateTime.Now,
            //    Balans = x.Balance

            //}).Take(6).ToList();
        }
    }
}
