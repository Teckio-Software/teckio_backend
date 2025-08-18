
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ERP_TECKIO
{
    [Route("api/preciounitario/15")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPrecioUnitario-Empresa2")]
    public class PrecioUnitarioAlumno15Controller : ControllerBase
    {
        private readonly PrecioUnitarioProceso<Alumno15Context> _precioUnitarioProceso;
        private readonly IProgramacionEstimadaService<Alumno15Context> _programacionestimadaService;
        private readonly DbContextOptionsBuilder<Alumno15Context> _Options;
        private readonly ExplocionInsumosProceso<Alumno15Context> _explocionInsumosProceso;
        public PrecioUnitarioAlumno15Controller(
            PrecioUnitarioProceso<Alumno15Context> precioUnitarioProceso
            , IProgramacionEstimadaService<Alumno15Context> programacionEstimadaService
            , DbContextOptionsBuilder<Alumno15Context> options,
            ExplocionInsumosProceso<Alumno15Context> explocionInsumosProceso
            )
        {
            _precioUnitarioProceso = precioUnitarioProceso;
            _programacionestimadaService = programacionEstimadaService;
            _Options = options;
            _explocionInsumosProceso = explocionInsumosProceso;
        }

        [HttpGet("todos/{IdProyecto:int}")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> Obtener(int IdProyecto)
        {
            return await _precioUnitarioProceso.ObtenerPrecioUnitario(IdProyecto);
        }

        [HttpGet("obtenerConceptos/{IdProyecto:int}")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> obtenerConceptos(int IdProyecto)
        {
            var PUs = await _precioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(IdProyecto);
            var conceptos = PUs.Where(z => z.TipoPrecioUnitario == 1).ToList();
            return conceptos;
        }

        [HttpGet("sinestructurar/{IdProyecto:int}")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> ObtenerSinEstructura(int IdProyecto)
        {
            return await _precioUnitarioProceso.ObtenerSinEstructura(IdProyecto);
        }

        [HttpPost("crear")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CrearYObtener([FromBody] PrecioUnitarioDTO registro)
        {
            using (var db = new Alumno15Context(_Options.Options))
            {
                return await _precioUnitarioProceso.CrearYObtener(registro, db);
            }
        }

        [HttpPost("editar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> Editar([FromBody] PrecioUnitarioDTO registro)
        {
            return await _precioUnitarioProceso.Editar(registro);
        }

        [HttpGet("AutorizarPresupuesto/{IdProyecto:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task AutorizarPresupuesto(int IdProyecto)
        {
            await _precioUnitarioProceso.AutorizarPresupuesto(IdProyecto);
        }

        [HttpPost("AutorizarXPrecioUnitario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task AutorizarXPrecioUnitario(PrecioUnitarioDTO partida)
        {
            await _precioUnitarioProceso.AutorizarXPartida(partida);
        }

        [HttpPost("partirConcepto")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> partirConcepto([FromBody] PrecioUnitarioDTO registro)
        {
            return await _precioUnitarioProceso.PartirConcepto(registro);
        }

        [HttpPost("EditarIndirectoPrecioUnitario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<bool>> EditarIndirectoPrecioUnitario([FromBody] PrecioUnitarioDTO registro)
        {
            return await _precioUnitarioProceso.EditarIndirectoPrecioUnitario(registro);
        }

        [HttpPost("eliminar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarPrecioUnitario-Empresa2")]
        public async Task<ActionResult<RespuestaDTO>> Eliminar([FromBody] int idPrecioUnitario)
        {
            return await _precioUnitarioProceso.EliminarPU(idPrecioUnitario);
        }

        [HttpPost("copiaregistros")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPrecioUnitario-Empresa2")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CopiarRegistros([FromBody] DatosParaCopiarDTO datos)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno15Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.CrearRegistrosCopias(datos.Registros, datos.IdPrecioUnitarioBase, datos.IdProyecto, db);
            }
            return registros;
        }

        [HttpGet("todoscopia/{IdProyecto:int}")]
        public async Task<ActionResult<List<PrecioUnitarioCopiaDTO>>> ObtenerCopia(int IdProyecto)
        {
            return await _precioUnitarioProceso.ObtenerPrecioUnitarioCopia(IdProyecto);
        }

        [HttpPost("copiararmado")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CopiarArmado([FromBody] DatosParaCopiarArmadoDTO datos)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno15Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.CrearRegistrosDetallesCopia(datos.Registros, datos.IdPrecioUnitarioBase, datos.IdProyecto, db);
            }
            return registros;
        }

        [HttpPost("copiararmadocomoconcepto")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CopiarArmadoComoConcepto([FromBody] DatosParaCopiarArmadoDTO datos)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno15Context(_Options.Options))
            {
                registros = await _precioUnitarioProceso.CrearRegistrosDetallesCopiaConcepto(datos.Registros, datos.IdPrecioUnitarioBase, datos.IdProyecto, db);
            }
            return registros;
        }

        [HttpPost("excel")]
        public async Task<ActionResult> Excel(int i)
        {
            await _precioUnitarioProceso.CreardeExcel(i);
            return NoContent();
        }

        [HttpPost("importarPresupuestoExcel")]
        public async Task<ActionResult> Excel([FromForm] List<IFormFile> files, [FromForm] int idProyecto)
        {
            await _precioUnitarioProceso.CrearPresupuestoConExel(files, idProyecto);
            return NoContent();
        }

        [HttpPost("excelInsumos")]
        public async Task<ActionResult> ExcelInsumos(int i)
        {
            await _precioUnitarioProceso.CrearRegistrosDeInsumosDeExcel(i);
            return NoContent();
        }

        [HttpGet("obtenerExplosionDeInsumos/{IdProyecto}")]
        public async Task<ActionResult<List<InsumoParaExplosionDTO>>> ObtenerExplosion(int IdProyecto)
        {
            return await _precioUnitarioProceso.obtenerExplosion(IdProyecto);
        }

        [HttpPost("obtenerExplosionDeInsumosXPrecioUnitario")]
        public async Task<ActionResult<List<InsumoParaExplosionDTO>>> ObtenerExplosionXPrecioUnitario(PrecioUnitarioDTO precioUnitario)
        {
            return await _precioUnitarioProceso.ObtenerExplosionXPrecioUnitario(precioUnitario);
        }

        [HttpPost("editarCostoDesdeExplosion")]
        public async Task<ActionResult<List<InsumoParaExplosionDTO>>> EditarCostoDesdeExplosion(InsumoParaExplosionDTO registro)
        {
            return await _precioUnitarioProceso.EditarInsumoDesdeExplosion(registro);
        }

        [HttpGet("obtenerExplosionDeInsumosXEmpleado/{IdProyecto}/{IdEmpleado}")]
        public async Task<ActionResult<List<InsumoParaExplosionDTO>>> obtenerExplosionXEmpleado(int IdProyecto, int IdEmpleado)
        {
            return await _explocionInsumosProceso.obtenerExplosionXEmpleado(IdProyecto, IdEmpleado);
        }

        [HttpPost("recalcularPresupuesto")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> RecalcularPresupuest([FromBody] int IdProyecto)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno15Context(_Options.Options))
            {
                await _precioUnitarioProceso.RecalcularPresupuesto(IdProyecto, db);
                registros = await _precioUnitarioProceso.ObtenerPrecioUnitario(IdProyecto);
            }
            return registros;
        }

        [HttpPost("moverRegistro")]
        public async Task<ActionResult<List<PrecioUnitarioDTO>>> MoverRegistros([FromBody] PreciosParaEditarPosicionDTO registro)
        {
            var registros = new List<PrecioUnitarioDTO>();
            using (var db = new Alumno15Context(_Options.Options))
            {
                await _precioUnitarioProceso.modificarPosicion(registro);
                registros = await _precioUnitarioProceso.ObtenerPrecioUnitario(registro.Seleccionado.IdProyecto);
            }
            return registros;
        }

        [HttpPost("importarOpus")]
        public async Task<IActionResult> ImportarOpus([FromForm] List<IFormFile> files, [FromForm] int IdProyecto)
        {
            var ImportacionInsumoDBF = files.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "P.DBF").FirstOrDefault();
            if (ImportacionInsumoDBF == null || ImportacionInsumoDBF.Length == 0)
                return BadRequest("Archivo DBF no válido.");
            var ImportacionInsumoFPT = files.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "P.FPT").FirstOrDefault();
            if (ImportacionInsumoFPT == null || ImportacionInsumoFPT.Length == 0)
                return BadRequest("Archivo FPT no válido.");

            await _precioUnitarioProceso.ImportarInsumos(ImportacionInsumoDBF, ImportacionInsumoFPT, IdProyecto);

            var ImportacionConceptosDBF = files.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "A.DBF").FirstOrDefault();
            if (ImportacionConceptosDBF == null || ImportacionConceptosDBF.Length == 0)
                return BadRequest("Archivo DBF no válido.");
            var ImportacionConceptosFPT = files.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "A.FPT").FirstOrDefault();
            if (ImportacionConceptosFPT == null || ImportacionConceptosFPT.Length == 0)
                return BadRequest("Archivo FPT no válido.");

            await _precioUnitarioProceso.ImportarConceptos(ImportacionConceptosDBF, ImportacionConceptosFPT, IdProyecto);

            var ArmadoCatalogoConcepto = files.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "1.DBF").FirstOrDefault();
            if (ArmadoCatalogoConcepto == null || ArmadoCatalogoConcepto.Length == 0)
                return BadRequest("Archivo DBF no válido.");
            var ArmadoCatalogoConceptoFPT = files.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "1.FPT").FirstOrDefault();
            if (ArmadoCatalogoConceptoFPT == null || ArmadoCatalogoConceptoFPT.Length == 0)
                return BadRequest("Archivo FPT no válido.");

            await _precioUnitarioProceso.ArmarCatalogoConceptos(ArmadoCatalogoConcepto, ArmadoCatalogoConceptoFPT, IdProyecto);

            var ArmadoPU = files.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "F.DBF").FirstOrDefault();
            if (ArmadoPU == null || ArmadoPU.Length == 0)
                return BadRequest("Archivo DBF no válido.");
            var ArmadoPUFPT = files.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "F.FPT").FirstOrDefault();
            if (ArmadoPUFPT == null || ArmadoPUFPT.Length == 0)
                return BadRequest("Archivo FPT no válido.");

            await _precioUnitarioProceso.ArmarPreciosUnitarios(ArmadoPU, ArmadoPUFPT, IdProyecto);
            return NoContent();
        }
        //public async Task ImportarOpus(List<IFormFile> archivosDBF)
        //{
        //    var ImportacionInsumo = archivosDBF.Where(z => z.FileName.Substring(z.FileName.Length - 5, 5) == "P.DBF").FirstOrDefault();
        //    var archivoInsumo = ImportacionInsumo.OpenReadStream();
        //    var insumos = new DBFReader(archivoInsumo);
        //    object[] rowObjects;
        //    while ((rowObjects = insumos.NextRecord()) != null)
        //    {
        //        // Procesar cada fila del archivo DBF
        //        for (int i = 0; i < rowObjects.Length; i++)
        //        {
        //            Console.WriteLine($"Columna {i}: {rowObjects[i]}");
        //        }
        //    }
        //    //DbfDataReader file = new DbfDataReader(ImportacionInsumo.FirstOrDefault(), FileMode.Open, FileAccess.Read);
        //}
    }
}
