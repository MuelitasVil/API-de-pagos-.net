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
    public class PayersController : ControllerBase
    {
        private readonly APIPaymentTransactionsContext context;

        public PayersController(APIPaymentTransactionsContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<String>> Post(Payer payer) 
        { 
            var name = new SqlParameter("@name", SqlDbType.VarChar);
            name.Direction = ParameterDirection.Output;

            await context.Database.ExecuteSqlInterpolatedAsync(
                           $@"EXEC InsertPayer
                           @documentType = {payer.documentType},
                           @name = {payer.name} OUTPUT,
                           @email = {payer.email},
                           @number = {payer.number} ");

            var id = (string)name.Value;
            return Ok(id);  

        }
        



    }
}
