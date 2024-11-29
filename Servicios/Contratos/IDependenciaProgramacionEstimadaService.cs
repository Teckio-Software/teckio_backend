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
    public interface IDependenciaProgramacionEstimadaService<T> where T : DbContext
    {
        Task<List<DependenciaProgramacionEstimadaDTO>> ObtenerXIdProgramacionEstimadaGantt(int IdProgramacionEstimadaGantt);
        Task<DependenciaProgramacionEstimadaDTO> ObtenerXId(int Id);
        Task<DependenciaProgramacionEstimadaDTO> CrearYObtener(DependenciaProgramacionEstimadaDTO registro);
        Task<RespuestaDTO> Editar(DependenciaProgramacionEstimadaDTO registro);
        Task<RespuestaDTO> Eliminar(int IdRegistro);
        Task<RespuestaDTO> EliminarMultiple(List<DependenciaProgramacionEstimadaDTO> registros);
    }
}
