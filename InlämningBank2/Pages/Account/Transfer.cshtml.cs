using InlämningBank2.BankAppData;
using InlämningBank2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InlämningBank2.Pages.Account
{
    [BindProperties]
    public class TransferModel : PageModel
    {
        private readonly IAccountsService _accountsService;
        public TransferModel(IAccountsService accountsService) 
        {
            _accountsService = accountsService;
        }
        public int AccountId { get; set; }
        public int Account { get; set; }
        public decimal Amount { get; set; }
        
        public DateTime DateTransfer { get; set; }
        public int CustomerId { get; set; }
        public int accounttoId { get; set; }
        public int Balance { get; set; }
        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            Account = Account;
            CustomerId = customerId;
            Balance = Balance;
            DateTransfer = DateTime.Now;
        }
        public IActionResult OnPost(int accountId, int customerId)
        {
            if (DateTransfer < DateTime.Now.AddMinutes(-5))
            {
                ModelState.AddModelError(
                    "DateTransfer", "Cannot Deposit money in the past!");
            }
            if (ModelState.IsValid)
            {
                var status = _accountsService.GetTransfer(accountId, accounttoId, Amount);
                return RedirectToPage("/CustomerImage", new { customerId, accountId });
            }
            return Page();           
        }
    }
}
