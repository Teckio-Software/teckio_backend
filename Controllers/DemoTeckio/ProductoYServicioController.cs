using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckio
{
    [Route("api/productoyservicio/3")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoYServicioDemoTeckioController : ControllerBase
    {
        public ProductoYServicioDemoTeckioController() { }
    }
}
