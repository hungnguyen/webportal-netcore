using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface ILanguageService : IService<Language, LanguageRequest>
    {
        Task<PagedResult<LanguageView>> GetPaging(LanguageSearchRequest request);
    }
}
