using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.Common
{
    public class Helper
    {
        public async Task<IActionResult> PaginatedListAll<T>(
            int Offset, int Take, 
            Func<IQueryable<T>> selector, 
            Func<IActionResult> badRequest,
            Func<object?, IActionResult> ok)
        {
            if (Offset < 0 || Take <= 0)
            {
                return badRequest.Invoke();
            }

            var query = selector.Invoke();

            int total = await query.CountAsync<T>();


            var items = await query
                .Skip(Offset)
                .Take(Take)
                .ToListAsync();

            var result = new
            {
                Offset,
                Take,
                Total = total,
                Items = items.Count,
                Data = items
            };

            return ok.Invoke(result);
        }
    }
}
