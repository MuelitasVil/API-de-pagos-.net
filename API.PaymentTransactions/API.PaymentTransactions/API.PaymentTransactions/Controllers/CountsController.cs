using API.PaymentTransactions.Data;
using API.PaymentTransactions.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API.PaymentTransactions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountsController : ControllerBase
    {

        private readonly APIPaymentTransactionsContext context;
        public CountsController(APIPaymentTransactionsContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Count count)
        {
            var result = new SqlParameter("@Resultado", SqlDbType.Int);
            result.Direction = ParameterDirection.Output;

            await context.Database.ExecuteSqlInterpolatedAsync(
                 $@"EXEC InsertCount
	             @currency = {count.currency},
                 @Total = {count.Total},
                 @allowPartial = {count.allowPartial},
                 @suscribe = 0,
                 @PayerId = {count.payerId},
                 @Resultado = 0 ");

            var res = (string)result.Value;
            return Ok(res);

        }
    }
}
