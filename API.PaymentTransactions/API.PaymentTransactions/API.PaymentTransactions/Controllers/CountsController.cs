using API.PaymentTransactions.Data;
using API.PaymentTransactions.Shared;
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
            SqlParameter currencyParam = new SqlParameter("@currency", SqlDbType.Int);
            currencyParam.Value = count.currency;

            SqlParameter totalParam = new SqlParameter("@Total", SqlDbType.BigInt);
            totalParam.Value = count.Total;

            SqlParameter allowPartialParam = new SqlParameter("@allowPartial", SqlDbType.Bit);
            allowPartialParam.Value = count.allowPartial;

            SqlParameter suscribeParam = new SqlParameter("@suscribe", SqlDbType.Bit);
            suscribeParam.Value = false; 

            SqlParameter payerIdParam = new SqlParameter("@PayerId", SqlDbType.BigInt);
            payerIdParam.Value = count.payerId;

            SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.Int);
            resultadoParam.Direction = ParameterDirection.Output;

            String sqlCommand = 
                @"EXEC InsertCount
                @currency,
                @Total,
                @allowPartial,
                @suscribe,
                @PayerId,
                @Resultado OUTPUT";

            await context.Database.ExecuteSqlRawAsync(
                sqlCommand,
                currencyParam,
                totalParam,
                allowPartialParam,
                suscribeParam,
                payerIdParam,
                resultadoParam
            );

            return Ok($"Se ha creado una nueva cuenta al usuario -> : {count.payerId}");

        }

        [HttpGet("{payerId:int}")]
        public async Task<ActionResult<Payer>> Get(int payerId)
        {

            SqlParameter payerIdParam = new SqlParameter("@payerId", SqlDbType.BigInt);
            payerIdParam.Value = payerId;

            String sqlCommand = "Exec dbo.GetCountsByPayers @payerId";

            IAsyncEnumerable<Count> ConsultCounts = context.Counts.FromSqlRaw(sqlCommand, payerIdParam).AsAsyncEnumerable();

            return Ok(ConsultCounts);
            return NotFound();
        }
    }
}
