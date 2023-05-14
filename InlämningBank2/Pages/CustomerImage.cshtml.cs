using InlämningBank2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Reflection.Emit;
using DBContextLibrary.ViewModel;
using ServiceLibrary;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace InlämningBank2.Pages
{
    [Authorize(Roles = "Admin")]
    public class CustomerImageModel : PageModel
    {
        private readonly ICustomersService _customersService;
        private readonly IAccountsService _accountsService;
        public CustomerImageModel(ICustomersService customersService, IAccountsService accountsService)
        {
            //_dbContext = dbContext;
            _customersService = customersService;
            _accountsService = accountsService;

        }
        public CustomerViewModel Customer { get; set; }
        public List<AccountViewModel> Account { get; set; }

        public void OnGet(int customerId)
        {
            Customer = _customersService.GetCustomer(customerId);
            Account = _accountsService.GetAccounts(customerId);
            if (Customer == null)
            {
                // Handle the case when the customer is not found
                // For example, you can return a not found page or redirect to another page
                // return NotFound();
                // or
                // RedirectToPage("/Error");
            }

        }
    }
}
