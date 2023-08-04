using API.PaymentTransactions.Data;
using API.PaymentTransactions.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly APIPaymentTransactionsContext context;

        public PaymentController(APIPaymentTransactionsContext context)
        {
            this.context = context;
        }


        [HttpPost]
        public async Task<ActionResult<String>> Post(DataPayment paymentData)
        {
            Console.WriteLine("hOLALLALLALALAL");
            return Ok("ve");
        }
    }
}


/*
{
  "payment": {
    "description": "string"
  },
  "mount": {
    "fromTotal": 100,
    "fromCurrency": 0,
    "factor": 1
  },
  "receipt": {
    "receiptId": 0,
    "franchise": 0,
    "reference": "string",
    "issuerName": 0,
    "authorization": 0,
    "paymentMethod": 0,
    "payerId": 5
  }
}
*/



/*
            Payment payment = paymentData.payment;
            Mount mount = paymentData.mount;
            Receipt receipt = paymentData.receipt;
            
            SqlParameter fromTotalParam = new SqlParameter("@fromTotal", SqlDbType.BigInt);
            fromTotalParam.Value = mount.fromTotal;

            SqlParameter fromCurrencyParam = new SqlParameter("@fromCurrency", SqlDbType.Int);
            fromCurrencyParam.Value = mount.fromCurrency;

            SqlParameter countIdParam = new SqlParameter("@countId", SqlDbType.BigInt);
            countIdParam.Value = mount.countId;

            SqlParameter factorParam = new SqlParameter("@factor", SqlDbType.Int);
            
            
            switch (mount.fromCurrency)
            {
                case currencys.COP:
                    factorParam.Value = 1;
                    break;

                case currencys.USD:
                    factorParam.Value = 3949.19;
                    break;

                case currencys.EUR:
                    factorParam.Value = 4378.87;
                    break;

            }

            SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.NVarChar);
            descriptionParam.Value = payment.description;

            SqlParameter franchiseParam = new SqlParameter("@franchise", SqlDbType.Int);
            franchiseParam.Value = receipt.franchise;

            SqlParameter referenceParam = new SqlParameter("@reference", SqlDbType.NVarChar);
            referenceParam.Value = receipt.reference;

            SqlParameter issuerNameParam = new SqlParameter("@issuerName", SqlDbType.Int);
            issuerNameParam.Value = receipt.issuerName;

            SqlParameter authorizationParam = new SqlParameter("@authorization", SqlDbType.Int);
            authorizationParam.Value = receipt.authorization;

            SqlParameter paymentMethodParam = new SqlParameter("@paymentMethod", SqlDbType.Int);
            paymentMethodParam.Value = receipt.paymentMethod;

            SqlParameter payerIdParam = new SqlParameter("@payerId", SqlDbType.BigInt);
            payerIdParam.Value = receipt.payerId;
            */

/*
String sqlCommand = 
    @"EXEC InsertPayment
    @fromTotal,
    @fromCurrency,
    @countId,
    @factor,
    @description,
    @franchise,
    @reference,
    @issuerName,
    @authorization,
    @paymentMethod,
    @payerId";

await context.Database.ExecuteSqlRawAsync(
    sqlCommand,
    fromTotalParam,
    fromCurrencyParam,
    countIdParam,
    factorParam,
    descriptionParam,
    franchiseParam,
    referenceParam,
    issuerNameParam,
    authorizationParam,
    paymentMethodParam,
    payerIdParam);
*/