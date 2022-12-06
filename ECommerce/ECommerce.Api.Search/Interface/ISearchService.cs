using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interface
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic searchResults)> SearchAsync(int customerId);
    }
}
