using API.PaymentTransactions.Data;
using API.PaymentTransactions.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Payer>> Get(int id)
        {

            SqlParameter payerIdParam = new SqlParameter("@payerId", SqlDbType.BigInt);
            payerIdParam.Value = id;

            String sqlCommand = "Exec dbo.GetPayers @payerId";

            IAsyncEnumerable<Payer> ConsultPayers = context.Payers.FromSqlRaw(sqlCommand, payerIdParam).AsAsyncEnumerable();

            
            await foreach(Payer payer in ConsultPayers)
            {
                return Ok(payer);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<Payer>> Get()
        {

            String sqlCommand = "Exec dbo.GetPayers";
            IAsyncEnumerable<Payer> ConsultPayers = context.Payers.FromSqlRaw(sqlCommand).AsAsyncEnumerable();
            return Ok(ConsultPayers);
           
        }

        [HttpPut]
        public async Task<ActionResult<String>> Put(Payer payer)
        {

            SqlParameter payerIdParam = new SqlParameter("@payerId", SqlDbType.BigInt);
            payerIdParam.Value = payer.PayerId;

            SqlParameter documentTypeParam = new SqlParameter("@documentType", SqlDbType.Int);
            documentTypeParam.Value = payer.documentType;

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 100);
            nameParam.Value = payer.name;

            SqlParameter emailParam = new SqlParameter("@email", SqlDbType.VarChar);
            emailParam.Value = payer.email;

            SqlParameter numberParam = new SqlParameter("@number", SqlDbType.VarChar);
            numberParam.Value = payer.number;

            SqlParameter responseParam = new SqlParameter("@response", SqlDbType.Int);
            responseParam.Value = 0;
            responseParam.Direction = ParameterDirection.Output;

            string sqlCommand = "Exec dbo.EditPayerById @payerId,@email,@number,@name, @response OUTPUT";

            await context.Database.ExecuteSqlRawAsync(
                sqlCommand,
                payerIdParam,
                emailParam,
                numberParam,
                nameParam,
                responseParam
            );

            int response = (int)responseParam.Value;
            if(response == 0) return Ok(payer);
            return BadRequest("El payer que desea ser modificado no existe ....");

        }

    }
}
