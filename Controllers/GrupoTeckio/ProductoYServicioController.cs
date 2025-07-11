using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.GrupoTeckio
{
    [Route("api/productoyservicio/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoYServicioGrupoTeckioController : ControllerBase
    {
        public ProductoYServicioGrupoTeckioController() { }
    }
}
