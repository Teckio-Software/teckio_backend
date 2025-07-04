using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace ERP_TECKIO
{
    public class ProyectoProceso<TContext> where TContext : DbContext
    {
        private readonly IProyectoService<TContext> _proyectoService;
        private readonly IFactorSalarioRealService<TContext> _fsrService;
        private readonly IFactorSalarioIntegradoService<TContext> _fsiService;
        private readonly ProgramacionEstimadaGanttProceso<TContext> _ProgramacionEstimadaGantt;
        private readonly TContext _context;

        public ProyectoProceso(
            IProyectoService<TContext> proyectoService
            , IFactorSalarioRealService<TContext> fsrService
            , IFactorSalarioIntegradoService<TContext> fsiService
            , ProgramacionEstimadaGanttProceso<TContext> programacionEstimadaProceso
            , TContext context)
        {
            _proyectoService = proyectoService;
            _fsrService = fsrService;
            _fsiService = fsiService;
            _context = context;
            _ProgramacionEstimadaGantt = programacionEstimadaProceso;
        }

        public async Task<ProyectoDTO> Post([FromBody] ProyectoDTO parametroCreacionDTO)
        {
            var respuesta = new ProyectoDTO();
            try
            {
                var ExisteNombreProyecto = await _proyectoService.ObtenXNombreProyecto(parametroCreacionDTO.Nombre);
                if (ExisteNombreProyecto.Id > 0) {
                    return respuesta;
                }
                respuesta = await _proyectoService.CrearYObtener(parametroCreacionDTO);
                FactorSalarioIntegradoDTO FSI = new FactorSalarioIntegradoDTO();
                FSI.IdProyecto = respuesta.Id;
                FSI.Fsi = 1;
                var FSICreado = await _fsiService.CrearYObtener(FSI);
                FactorSalarioRealDTO FSR = new FactorSalarioRealDTO();
                FSR.IdProyecto = respuesta.Id;
                FSR.PorcentajeFsr = 1;
                var FSRCreado = await _fsrService.CrearYObtener(FSR);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return respuesta;
        }

        public async Task Put([FromBody] ProyectoDTO parametros)
        {
            var programacionesGantt = await _ProgramacionEstimadaGantt.ObtenerProgramacionEstimadaXIdProyecto(parametros.Id, _context);
            var inicio = parametros.FechaInicio.ToShortDateString();
            var termino = parametros.FechaInicio.AddDays(1).ToShortDateString();
            foreach(var gantt in programacionesGantt)
            {
                gantt.Start = Convert.ToDateTime(inicio);
                gantt.End = Convert.ToDateTime(termino);
                await _ProgramacionEstimadaGantt.editarFechaProyectoGantt(gantt);
            }
            try
            {
                var resultado = await _proyectoService.Editar(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task<ActionResult<ProyectoDTO>> PostCrearYObtener([FromBody] ProyectoDTO creacionDTO)
        {
            try
            {
                if (creacionDTO.IdPadre > 0)
                {
                    var padre = await _proyectoService.ObtenXId(creacionDTO.IdPadre);
                    creacionDTO.Nivel = padre.Nivel + 1;
                }
                var resultado = await _proyectoService.CrearYObtener(creacionDTO);
                FactorSalarioIntegradoDTO FSI = new FactorSalarioIntegradoDTO();
                FSI.IdProyecto = resultado.Id;
                FSI.Fsi = 1;
                var FSICreado = await _fsiService.CrearYObtener(FSI);
                FactorSalarioRealDTO FSR = new FactorSalarioRealDTO();
                FSR.IdProyecto = resultado.Id;
                FSR.PorcentajeFsr = 1;
                var FSRCreado = await _fsrService.CrearYObtener(FSR);
                return resultado;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new ProyectoDTO();
            }
        }

        public async Task delete(int Id)
        {
            try
            {
                var existeFsr = await _fsrService.ObtenerTodosXProyecto(Id);
                if(existeFsr.Count > 0)
                {
                    var fsr = existeFsr.FirstOrDefault();
                    await _fsrService.Eliminar(fsr.Id);
                }
                var existeFSI = await _fsiService.ObtenerTodosXProyecto(Id);
                if(existeFSI.Count > 0)
                {
                    var fsi = existeFSI.FirstOrDefault();
                    await _fsiService.Eliminar(fsi.Id);
                }
                var resultado = await _proyectoService.Eliminar(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task<ActionResult<List<ProyectoDTO>>> Get()
        {
            var lista = await _proyectoService.Lista();
            var listaEstructurada = await _proyectoService.Estructurar(lista);
            var listaResult = listaEstructurada.OrderBy(z => z.Id).ToList();
            return listaResult;
        }

        public async Task<ActionResult<List<ProyectoDTO>>> GetSinEstructura()
        {
            var lista = await _proyectoService.Lista();
            var query = lista.AsQueryable();
            var listaResult = query.OrderBy(z => z.Id).ToList();
            return listaResult;
        }
public async Task<ActionResult<ProyectoDTO>> GetXId(int Id)
        {
            var lista = await _proyectoService.ObtenXId(Id);
            return lista;
        }
        
    }
}
