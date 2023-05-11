using InlämningBank2.BankAppData;
using InlämningBank2.Services;
using InlämningBank2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace InlämningBank2.Pages.Account
{
    [BindProperties]
    public class DepositeModel : PageModel
    {
        private readonly IAccountsService _accountsService;
        public DepositeModel(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        //public List<DepositViewModel> Deposit { get; set; }
        [Range(100, 10000)]
        public decimal Amount { get; set; }
        public DateTime DepositDate { get; set; }
        [Required(ErrorMessage =
             "You forgot to write a comment!")]
        [MinLength(5, ErrorMessage =
            "Comments must be at least 5 characters long")]
        [MaxLength(250, ErrorMessage =
            "OK, thats just too many words")]
        public string Comment { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public void OnGet(int accountId, int customerId)
        {
            DepositDate = DateTime.Now.AddHours(1);
            CustomerId = customerId;
            AccountId = accountId;
        }
        public IActionResult OnPost(int accountId, int customerId)
        {
            if (DepositDate < DateTime.Now)
            {
                ModelState.AddModelError(
                    "DepositDate", "Cannot Deposit money in the past!");
            }

            if (ModelState.IsValid)
            {
                var accountDb = _accountsService.GetAccount(accountId);
                accountDb.Balance += Amount;
                _accountsService.Update(accountDb);
                return RedirectToPage("/CustomerImage", new { customerId, accountId });             
            }
            return Page();
        }

    }
}
