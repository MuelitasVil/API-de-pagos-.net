using API.PaymentTransactions.API.Controllers.ModelsOfControllers;
using API.PaymentTransactions.Data;
using API.PaymentTransactions.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.API.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<String>> Post(PostPayment paymentData)
        {

            SqlParameter fromTotalParam = new SqlParameter("@fromTotal", SqlDbType.BigInt);
            fromTotalParam.Value = paymentData.mountFromTotal;

            SqlParameter fromCurrencyParam = new SqlParameter("@fromCurrency", SqlDbType.Int);
            fromCurrencyParam.Value = paymentData.mountfromCurrency;

            SqlParameter countIdParam = new SqlParameter("@countId", SqlDbType.BigInt);
            countIdParam.Value = paymentData.mountCountId;

            SqlParameter factorParam = new SqlParameter("@factor", SqlDbType.Int);


            switch (paymentData.mountfromCurrency)
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
            descriptionParam.Size = 20;
            descriptionParam.Value = paymentData.paymentDescription;

            SqlParameter franchiseParam = new SqlParameter("@franchise", SqlDbType.Int);
            franchiseParam.Size = 20;
            franchiseParam.Value = paymentData.receiptFranchise;

            SqlParameter referenceParam = new SqlParameter("@reference", SqlDbType.NVarChar, paymentData.receiptReference.Length);
            referenceParam.Value = paymentData.receiptReference;

            SqlParameter issuerNameParam = new SqlParameter("@issuerName", SqlDbType.Int);
            issuerNameParam.Value = paymentData.mountfromCurrency;

            SqlParameter authorizationParam = new SqlParameter("@authorization", SqlDbType.Int);
            authorizationParam.Value = paymentData.receiptAuthorization;

            SqlParameter paymentMethodParam = new SqlParameter("@paymentMethod", SqlDbType.Int);
            paymentMethodParam.Value = paymentData.receiptPaymentMethod;

            SqlParameter payerIdParam = new SqlParameter("@payerId", SqlDbType.BigInt);
            payerIdParam.Value = paymentData.receiptPayerId;

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

            return Ok("ve");
        }
    }
}


/*
{
  "paymentDescription": "Generar Pago de algo",
  "mountFromTotal": 100,
  "mountfromCurrency": 0,
  "mountCountId": 1,
  "mountFactor": 0,
  "receiptReference": "ASD",
  "receiptFranchise": 0,
  "receiptAuthorization": 123,
  "receiptIssuername": 0,
  "receiptPaymentMethod": 0,
  "receiptPayerId": 1
}

*/

