using dolar_api_conversion.resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace dolar_api_conversion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {

        [HttpGet(Name = "ObtenerCotizacion")]

        public async Task<string> ObtenerCotizacion([FromQuery, Required] decimal valor)
        {
            ExampleAPI api = new ExampleAPI();

            decimal result = await api.ObtenerCotizacionEspecifica();

            return (result * valor).ToString();

        }

    }
}
