using InlämningBank2.BankAppData;
using InlämningBank2.Infrastructure.Paging;
using InlämningBank2.ViewModel;

namespace InlämningBank2.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly BankAppDataContext _dbContext;
        public CustomersService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
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

            var Customers = query.Select(c => new CustomersViewModel
            {
                CustomerId = c.CustomerId,
                City = c.City,
                Surname = c.Surname,
                Givenname = c.Givenname,
                Gender = c.Gender,
                Zipcode = c.Zipcode
                //CustomerId = c.CustomerId,
                //Givenname = c.Givenname,
                //Surname = c.Surname                

            }).ToList();

            return query.GetPaged(pageNo, 30);

        }

    }
}
