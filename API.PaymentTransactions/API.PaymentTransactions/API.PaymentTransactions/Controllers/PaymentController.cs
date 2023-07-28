using API.PaymentTransactions.Data;
using API.PaymentTransactions.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

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

        /*
        [HttpPost]
        public IActionResult InsertPayment(Payment paymentData)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("InsertPayment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@toTotal", paymentData.ToTotal);
                    command.Parameters.AddWithValue("@toCurrency", paymentData.ToCurrency);
                    command.Parameters.AddWithValue("@fromTotal", paymentData.FromTotal);
                    command.Parameters.AddWithValue("@fromCurrency", paymentData.FromCurrency);
                    command.Parameters.AddWithValue("@factor", paymentData.Factor);
                    command.Parameters.AddWithValue("@countId", paymentData.CountId);

                    SqlParameter existCountParameter = new SqlParameter("@ExistCount", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(existCountParameter);
                    connection.Open();
                    command.ExecuteNonQuery();

                    int existCount = (int)existCountParameter.Value;

                    if (existCount <= 0)
                    {
                        return BadRequest("No se pudo insertar el pago debido a que ExistCount es menor o igual a 0.");
                    }

                    return Ok("El pago se insertó correctamente.");
                }
            }
        }
    }
    */




}
}
