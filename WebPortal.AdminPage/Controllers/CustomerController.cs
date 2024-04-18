using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.Utilities.Constants;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private readonly IEncryptDecrypt _encryptDecrypt;
        public CustomerController(ICustomerService customerService,
            IMapper mapper,
            IEncryptDecrypt encryptDecrypt,
            IStorageService storageService,
            IWebsiteService websiteService) : base(websiteService)
        {
            _mapper = mapper;
            _customerService = customerService;
            _storageService = storageService;
            _encryptDecrypt = encryptDecrypt;
        }
        public async Task<IActionResult> Index([FromQuery]CustomerSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;

            ViewBag.SearchRequest = request;
            var customers = await _customerService.GetPaging(request);
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                request.WebsiteID = WebsiteID;
                request.DateCreated = DateTime.Now;

                if (request.NewImage != null)
                {
                    request.Image = await _storageService.SaveFileAsync(request.NewImage);
                }
                request.Password = _encryptDecrypt.Encrypt(request.NewPassword, SystemConstant.EncryptKey);

                await _customerService.Create(request);
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetById(id);
            if (customer == null) return RedirectToAction("Index");

            var customerRequest = new CustomerRequest();
            _mapper.Map(customer, customerRequest);

            customerRequest.ImageUrl = _storageService.GetFileUrl(customerRequest.Image);

            return View(customerRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request.NewImage != null)
                {
                    await _storageService.DeleteFileAsync(request.Image);
                    request.Image = await _storageService.SaveFileAsync(request.NewImage);
                }

                if (!string.IsNullOrEmpty(request.NewPassword))
                    request.Password = _encryptDecrypt.Encrypt(request.NewPassword, SystemConstant.EncryptKey);

                await _customerService.Update(id, request);
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.Delete(id);
            await _storageService.DeleteFileAsync(customer.Image);
            return RedirectToAction("Index");
        }
    }
}
