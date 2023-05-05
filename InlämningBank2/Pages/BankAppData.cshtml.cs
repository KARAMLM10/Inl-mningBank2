//using InlämningBank2.BankAppData;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Data;

//namespace InlämningBank2.Pages
//{
//    [Authorize(Roles = "Admin")]
//    public class BankAppDataModel : PageModel
//    {
//        private readonly BankAppDataContext _dbContext;

//        public BankAppDataModel(BankAppDataContext dbContext)
//        {
//            _dbContext = dbContext;
//        }
//        public class CustomersViewModel
//        {
//            public int CustomerId { get; set; }
//            public string Givenname { get; set; }
//            public string Surname { get; set; }
//        }
//        public List<CustomersViewModel> Customers { get; set; } = new List<CustomersViewModel>();

//        public void OnGet()
//        {
//            Customers = _dbContext.Customers.Select(s => new CustomersViewModel
//            {
//                //Id = s.CustomerId,
//                //CompanyName = s.FirstName,
//                //Region = s.Region
//                CustomerId = s.CustomerId,
//                Givenname = s.Givenname,
//                Surname = s.Surname,
                

//            }).ToList();
//        }
//    }
//}
