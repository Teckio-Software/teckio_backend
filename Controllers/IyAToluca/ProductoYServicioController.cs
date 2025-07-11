using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.IyAToluca
{
    [Route("api/productoyservicio/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoYServicioIyATolucaController : ControllerBase
    {
        public ProductoYServicioIyATolucaController() { }
    }
}
