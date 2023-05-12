using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ServiceLibrary;

namespace InlämningBank2.Pages.Account
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly IAccountsService _accountsService;
        public WithdrawModel(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        [Range(100, 10000)]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public void OnGet(int accountId, int customerId)
        {
            var accountDb = _accountsService.GetAccount(accountId);
            Balance = accountDb.Balance;
            CustomerId = customerId;
            AccountId = accountId;
        }
        public IActionResult OnPost(int accountId, int customerId)
        {
            var accountDb = _accountsService.GetAccount(accountId);

            if (accountDb.Balance < Amount)
            {
                ModelState.AddModelError(
                        "Amount", "You don't have that much money!");
            }

            if (ModelState.IsValid)
            {
                accountDb.Balance -= Amount;
                _accountsService.Update(accountDb);
                return RedirectToPage("/CustomerImage", new { customerId, accountId });
            }
            return Page();
        }


    }
}
