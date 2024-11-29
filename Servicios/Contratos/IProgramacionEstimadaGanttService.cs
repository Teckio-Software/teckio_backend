using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


using SistemaERP.DTO.Presupuesto.Gantt;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaERP.BLL.Contrato.Gantt
{
    public interface IProgramacionEstimadaGanttService<T> where T : DbContext
    {
        Task<List<ProgramacionEstimadaGanttDTO>> ObtenerXIdProyecto(int IdProyecto, DbContext db);
        Task<ProgramacionEstimadaGanttDTO> ObtenerXId(int Id, DbContext db);
        Task<ProgramacionEstimadaGanttDTO> CrearYObtener(ProgramacionEstimadaGanttDTO registro);
        Task<RespuestaDTO> Editar(ProgramacionEstimadaGanttDTO registro);
        Task<RespuestaDTO> Eliminar(int IdRegistro);
        Task<RespuestaDTO> EliminarMultiple(List<ProgramacionEstimadaGanttDTO> registros);
    }
}
