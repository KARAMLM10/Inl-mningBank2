using InlämningBank2.BankAppData;
using InlämningBank2.Services;
using InlämningBank2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Reflection.Emit;

namespace InlämningBank2.Pages
{
    public class Customer_imageModel : PageModel
    {
        private readonly ICustomersService _customersService;

        public Customer_imageModel(ICustomersService customersService)
        {
            //_dbContext = dbContext;
            _customersService = customersService;

        }
        public CustomerViewModel Customer { get; set; }
        //private readonly BankAppDataContext _dbContext;
        //private ICustomerService _customerService;
        //private IAccountsService _accountsService;
        //public Customer_imageModel(ICustomerService customerService, IAccountsService accountsService)
        //{
        //    _customerService = customerService;
        //    _accountsService = accountsService;
        //}

        //public List<CustomerViewModel> Customer { get; set; }
        //public List<CustomerViewModel> Account { get; set; }

        //public string Gender { get; set; }
        //public string Givenname { get; set; }
        //public string Surname { get; set; }
        //public string City { get; set; }
        //public string Zipcode { get; set; }
        //public int AccountId { get; set; }
        //public string Frequency { get; set; } = null!;
        //public DateTime Created { get; set; }
        //public decimal Balance { get; set; }
        public void OnGet(int customerId)
        {
            
                Customer = _customersService.GetCustomer(customerId);

                if (Customer == null)
                {
                    // Handle the case when the customer is not found
                    // For example, you can return a not found page or redirect to another page
                    // return NotFound();
                    // or
                    // RedirectToPage("/Error");
                }
            

            //Givenname = _dbContext.Customers.First(c => c.CustomerId == id).Givenname;
            //Gender = _dbContext.Customers.First(c => c.CustomerId == id).Gender;
            //Surname = _dbContext.Customers.First(c => c.CustomerId == id).Surname;
            //City = _dbContext.Customers.First(c => c.CustomerId == id).City;
            //Zipcode = _dbContext.Customers.First(c => c.CustomerId == id).Zipcode;
            //AccountId = _dbContext.Accounts.First(c => c.AccountId == id).AccountId;
            //Frequency = _dbContext.Accounts.First(c => c.AccountId == id).Frequency;
            //Created = _dbContext.Accounts.First(c => c.AccountId == id).Created;
            //Balance = _dbContext.Accounts.First(c => c.AccountId == id).Balance;

        }
    }
}
