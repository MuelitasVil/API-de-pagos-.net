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

            SqlParameter documentTypeParam = new SqlParameter("@documentType", SqlDbType.Int);
            documentTypeParam.Value = payer.documentType;

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 100); 
            nameParam.Value = payer.name;

            SqlParameter emailParam = new SqlParameter("@email", SqlDbType.VarChar);
            emailParam.Value = payer.email;

            SqlParameter numberParam = new SqlParameter("@number", SqlDbType.VarChar);
            numberParam.Value = payer.number;

            String sqlCommand = 
                @"EXEC InsertPayer
                @documentType,
                @name OUTPUT,
                @email,
                @number"; 

            await context.Database.ExecuteSqlRawAsync(
                sqlCommand,
                documentTypeParam,
                nameParam,
                emailParam,
                numberParam
            );

            return Ok($"Se ha creado un nuevo usuario -> {payer.name}");

        }
        



    }
}
