using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Services.Common;
using WebPortal.Utilities.Exeptions;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;
using System.Linq;
using WebPortal.Utilities.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace WebPortal.Services
{
    public class CustomerService : Service<Customer, CustomerRequest>, ICustomerService
    {
        private readonly IEncryptDecrypt _encryptDecrypt;
        public CustomerService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper, IEncryptDecrypt encryptDecrypt) : base(serviceScopeFactory, mapper)
        {
            _encryptDecrypt = encryptDecrypt;
        }

        public async Task<int> ChangePassword(int id, CustomerChangePasswordRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var customer = await dbContext.Customers.FindAsync(id);
                if (customer == null) throw new WPExeption($"Can't find a customer with id {id}");

                customer.Password = _encryptDecrypt.Encrypt(request.NewPassword, SystemConstant.EncryptKey);
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> Count(CustomerSearchRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from b in dbContext.Customers
                            where b.WebsiteID == request.WebsiteID
                            select b;

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(b => b.FullName.Contains(request.Keyword) ||
                                        b.Email.Contains(request.Keyword) ||
                                        b.PhoneNumber.Contains(request.Keyword));
                }

                return await query.CountAsync();
            }
        }

        public async Task<Customer> GetByEmail(string email)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Email.Equals(email));
                return customer;
            }
        }

        public async Task<Customer> GetByUsername(string username)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.UserName.Equals(username));
                return customer;
            }
        }

        public async Task<PagedResult<CustomerView>> GetPaging(CustomerSearchRequest request)
            => await Find<CustomerView>(
                    b => (b.WebsiteID == request.WebsiteID) &&
                        (string.IsNullOrEmpty(request.Keyword) || b.FullName.Contains(request.Keyword) ||
                                                                b.Email.Contains(request.Keyword) ||
                                                                b.PhoneNumber.Contains(request.Keyword)),
                    q => q.OrderBy(b => b.FullName),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        

        public async Task<int> Register(CustomerRegisterRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var customer = new Customer()
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    UserName = request.Username,
                    Password = _encryptDecrypt.Encrypt(request.Password, SystemConstant.EncryptKey)
                };

                dbContext.Customers.Add(customer);
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<string> ResetPassword(int id)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var customer = await dbContext.Customers.FindAsync(id);
                if (customer == null) throw new WPExeption($"Can't find a customer with id {id}");

                var newPassword = Guid.NewGuid().ToString().Substring(0, 8);
                customer.Password = _encryptDecrypt.Encrypt(newPassword, SystemConstant.EncryptKey);

                await dbContext.SaveChangesAsync();
                return newPassword;
            }
        }

        public async Task<bool> Signin(CustomerSigninRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var customer = await (from c in dbContext.Customers
                                      where c.UserName == request.Username && c.Password == _encryptDecrypt.Decrypt(request.Password, SystemConstant.EncryptKey)
                                      select c)
                                .SingleOrDefaultAsync();

                return customer != null;
            }
        }
    }
}
