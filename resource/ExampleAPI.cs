using Nancy.Json;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
namespace dolar_api_conversion.resource
{
    public class ExampleAPI
    {
        public async Task<decimal> ObtenerCotizacionEspecifica() //devuelve una lista de tipo decimal
        {
            HttpResponseMessage response; //almacenar rta HTTP

            using(var client = new HttpClient())
            {
                RequestCurrency currency = new RequestCurrency(); //instanciamos la clase request currency
                currency.Code = "Bolsa";
                var jsonObject = new JavaScriptSerializer().Serialize(currency);

                var content = new StringContent(jsonObject.ToString(),Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); //configura el tipo de contenido en los encabezados de la solicitud

                response = await client.PostAsync("https://localhost:7009/api/Cotizacion", content);

                if (!response.IsSuccessStatusCode)
                {
                    var httpResponse = new HttpResponseMessage(response.StatusCode);
                    httpResponse.ReasonPhrase = response.ReasonPhrase = "error para conectar a Dolar API";

                    httpResponse.Content = new StringContent("");

                    throw new HttpResponseException(httpResponse);

                }

                string str = response.Content.ReadAsStringAsync().Result; //lee el contenido de la rta en cadena JSON

                QuoteCurrencyResponse? result = JsonConvert.DeserializeObject<QuoteCurrencyResponse?>(str);

                return result.venta; //venta es propiedad de quotecurrencyresponse

            }

           
        }
    }
}
