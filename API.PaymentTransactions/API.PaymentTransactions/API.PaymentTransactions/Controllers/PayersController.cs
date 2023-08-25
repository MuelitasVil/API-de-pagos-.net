using API.PaymentTransactions.API.Cryptography;
using API.PaymentTransactions.Data;
using API.PaymentTransactions.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace API.PaymentTransactions.API.Controllers
{
    [Authorize] 
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

            Encrypt encrypt = new Encrypt();

            SqlParameter documentTypeParam = new SqlParameter("@documentType", SqlDbType.Int);
            documentTypeParam.Value = payer.documentType;

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 100);
            nameParam.Value = payer.name;

            SqlParameter emailParam = new SqlParameter("@email", SqlDbType.VarChar);
            emailParam.Value = payer.email;

            SqlParameter numberParam = new SqlParameter("@number", SqlDbType.VarChar);
            numberParam.Value = payer.number;

            SqlParameter saltParam = new SqlParameter("@salt", SqlDbType.VarChar);
            String salt = encrypt.getSalt(10);
            payer.salt = salt;
            saltParam.Value = salt;

            SqlParameter passwordParam = new SqlParameter("@password", SqlDbType.VarChar);
            String password = encrypt.GetSHA256(payer.password + salt);
            payer.password = password;
            passwordParam.Value = password;

            SqlParameter resultParam = new SqlParameter("@result", SqlDbType.Int);
            resultParam.Value = 0;
            resultParam.Direction = ParameterDirection.Output;

            String sqlCommand =
                @"EXEC InsertPayer
                @documentType,
                @name,
                @email,
                @number,
                @password,
                @salt,
                @result OUTPUT";

            await context.Database.ExecuteSqlRawAsync(
                sqlCommand,
                documentTypeParam,
                nameParam,
                emailParam,
                numberParam,
                saltParam,
                passwordParam,
                resultParam
            );


            Console.Out.WriteLine(".............");
            Console.Out.WriteLine(payer.salt);
            Console.Out.WriteLine(payer.password);

            int result = (int)resultParam.Value;
            if (result == 0) return Ok(payer);
            if (result == 1) return BadRequest("El correo ingresado ya existe");
            if (result == 2) return BadRequest("El numero ingreado ya existe");
            return BadRequest("nose xd");
      
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
