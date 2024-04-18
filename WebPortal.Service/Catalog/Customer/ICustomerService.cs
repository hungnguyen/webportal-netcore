using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface ICustomerService : IService<Customer, CustomerRequest>
    {
        Task<PagedResult<CustomerView>> GetPaging(CustomerSearchRequest request);
        Task<Customer> GetByUsername(string username);
        Task<Customer> GetByEmail(string email);
        Task<int> Register(CustomerRegisterRequest request);
        Task<bool> Signin(CustomerSigninRequest request);
        Task<string> ResetPassword(int id);
        Task<int> ChangePassword(int id, CustomerChangePasswordRequest request);
        Task<int> Count(CustomerSearchRequest request);
    }
}
