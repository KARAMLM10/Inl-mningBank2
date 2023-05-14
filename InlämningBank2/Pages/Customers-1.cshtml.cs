using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using DBContextLibrary.ViewModel;
using ServiceLibrary;

namespace InlämningBank2.Pages
{
    [Authorize(Roles = "Admin, Cashier")]
    public class Customers_1Model : PageModel
    {
        private readonly ICustomersService _customersService;

        public Customers_1Model(ICustomersService customersService)
        {
            _customersService = customersService;

        }

        public List<CustomersViewModel> Customers { get; set; }

        public string Q { get; set; }
        public int PageCount { get; set; }
        public int CustomerId { get; set; }
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
            var result = _customersService.GetCustomers(sortColumn, sortOrder, q, pageNo);
            PageCount = result.PageCount;

            Customers = result.Results.Select(c => new CustomersViewModel
            {
                CustomerId = c.CustomerId,
                Gender = c.Gender,
                City = c.City,
                Surname = c.Surname,
                Zipcode = c.Zipcode,
                Givenname = c.Givenname,

            }).ToList();

        }
    }
}
