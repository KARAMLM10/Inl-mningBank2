using DBContextLibrary.BankAppData;
using DBContextLibrary.Infrastructure.Paging;
using DBContextLibrary.ViewModel;

namespace ServiceLibrary
{
    public class CustomersService : ICustomersService
    {
        private readonly BankAppDataContext _dbContext;
        public CustomersService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerViewModel GetCustomer(int customerId)
        {
            var customer = _dbContext.Customers.FirstOrDefault(a => a.CustomerId == customerId);
            if (customer == null)
            {
                // Handle the case when the customer is not found
                return null; // or throw an exception, return a default value, etc.
            }

            var customerViewModel = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,                
                Givenname = customer.Givenname,
                Gender = customer.Gender,
                Country = customer.Country,
                Birthday = customer.Birthday,
                NationalId = customer.NationalId,
                Telephonenumber = customer.Telephonenumber,
                Emailaddress = customer.Emailaddress,
               
            };

            return customerViewModel;
        }


        public PagedResult<Customer> GetCustomers(string sortColumn, string sortOrder, string q, int pageNo)
        {


            var query = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(p => p.Surname.Contains(q) ||
                    p.City.Contains(q) ||
                    p.Country.Contains(q) ||
                    p.CustomerId.Equals(q) ||
                    p.Gender.Contains(q) ||
                    p.Zipcode.Contains(q) ||
                    p.Givenname.Contains(q));
            }

            if (sortColumn == "Surname")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Surname);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Surname);
            if (sortColumn == "City")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.City);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.City);
            if (sortColumn == "CustomerID")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.CustomerId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.CustomerId);
            if (sortColumn == "Gender")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Gender);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Gender);
            if (sortColumn == "Zipcode")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Zipcode);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Zipcode);
            if (sortColumn == "Givenname")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Givenname);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Givenname);

            var Customers = query.Select(c => new CustomersViewModel
            {
                CustomerId = c.CustomerId,
                City = c.City,
                Surname = c.Surname,
                Givenname = c.Givenname,
                Gender = c.Gender,
                Zipcode = c.Zipcode                             

            }).ToList();

            return query.GetPaged(pageNo, 30);

        }

    }
}
