using DBContextLibrary.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ServiceLibrary;

namespace InlämningBank2.Pages.Account
{
    public class TransactionsModel : PageModel
    {
        private readonly IAccountsService _accountsService;
        public TransactionsModel(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        public List<TransactionViewModel> Transactions { get; set; }
        public int AccountId { get; set; }
        public int PageNo { get; set; }
        public void OnGet(int accountId , int pageNo)
        {
            AccountId = accountId;
        }
        public IActionResult OnGetShowMore(int accountId, int pageNo)
        {
            var transactions = _accountsService.GetTransactions(accountId, pageNo);
            Transactions = transactions.Results.Select(a=> new TransactionViewModel
            {
                TransactionId = a.TransactionId,
                Date = a.Date,
                Type = a.Type,
                Amount = a.Amount,
            }).ToList();

             return new JsonResult(new {Transactions});
        }

    }
}
