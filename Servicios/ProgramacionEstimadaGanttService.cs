using AutoMapper;
using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERP_TECKIO.Servicios
{
    public class ProgramacionEstimadaGanttService<T> : IProgramacionEstimadaGanttService<T> where T : DbContext
    {
        private readonly IGenericRepository<ProgramacionEstimadaGantt, T> _Repositorio;
        private readonly IMapper _Mapper;

        public ProgramacionEstimadaGanttService(
            IGenericRepository<ProgramacionEstimadaGantt, T> repositorio
            , IMapper mapper
            )
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerXIdProyecto(int IdProyecto, DbContext db)
        {
            var items = db.Database.SqlQueryRaw<string>(""""
                select 
                PEG.Id
                , PEG.IdProyecto
                , P.EsSabado
                , P.EsDomingo
                , PEG.IdPrecioUnitario
                , PU.Cantidad
                , PU.TipoPrecioUnitario
                , CASE WHEN PU.TipoPrecioUnitario = 1 THEN 'task' ELSE 'project' END as Type
                , PEG.IdConcepto
                , C.Codigo
                , C.Descripcion
                , C.Descripcion as Name
                , C.CostoUnitario
                , C.CostoUnitario * PU.Cantidad as Importe 
                , PEG.FechaInicio
                , PEG.FechaTermino
                , PEG.Duracion
                , PEG.Progreso
                , PEG.Comando
                , PEG.DesfaseComando
                , CASE WHEN PEG.IdPadre != 0 THEN PEG.IdPadre ELSE null END as IdPadre
                from ProgramacionEstimadaGantt PEG
                inner join PrecioUnitario PU on PEG.IdPrecioUnitario = PU.id
                inner join Concepto C on PEG.IdConcepto = C.Id
                inner join Proyecto P on PEG.IdProyecto = P.Id
                where PEG.IdProyecto = 
                """" + IdProyecto +
                """"for json path""""
                ).ToList();
            if (items.Count <= 0)
            {
                return new List<ProgramacionEstimadaGanttDTO>();
            }
            string json = string.Join("", items);
            var datosBase = JsonSerializer.Deserialize<List<ProgramacionEstimadaGanttDeserealizadaDTO>>(json);
            var datos = _Mapper.Map<List<ProgramacionEstimadaGanttDTO>>(datosBase);
            return datos;
        }

        public async Task<ProgramacionEstimadaGanttDTO> ObtenerXId(int Id, DbContext db)
        {
            var items = db.Database.SqlQueryRaw<string>(""""
                select 
                PEG.Id
                , PEG.IdProyecto
                , PEG.IdPrecioUnitario
                , PU.Cantidad
                , PU.TipoPrecioUnitario
                , PEG.IdConcepto
                , C.Codigo
                , C.Descripcion
                , C.CostoUnitario
                , C.CostoUnitario * PU.Cantidad as Importe 
                , PEG.FechaInicio
                , PEG.FechaTermino
                , PEG.Duracion
                , PEG.Progreso
                , PEG.Comando
                , PEG.DesfaseComando
                , PEG.IdPadre
                from ProgramacionEstimadaGantt PEG
                inner join PrecioUnitario PU on PEG.IdPrecioUnitario = PU.id
                inner join Concepto C on PEG.IdConcepto = C.Id
                where PEG.Id = 
                """" + Id
                ).ToList();
            if (items.Count <= 0)
            {
                return new ProgramacionEstimadaGanttDTO();
            }
            string json = string.Join("", items);
            var datos = JsonSerializer.Deserialize<List<ProgramacionEstimadaGanttDTO>>(json);
            return datos.FirstOrDefault();
        }

        public async Task<List<ProgramacionEstimadaGantt>> ObtenerProgramacionesEnModelo(int IdProyecto)
        {
            try
            {
                var registros = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return registros;
            }
            catch
            {
                return new List<ProgramacionEstimadaGantt>();
            }
        }

        public async Task<ProgramacionEstimadaGanttDTO> CrearYObtener(ProgramacionEstimadaGanttDTO registro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<ProgramacionEstimadaGantt>(registro));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<ProgramacionEstimadaGanttDTO>(objetoCreado);
            }
            catch
            {
                return new ProgramacionEstimadaGanttDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ProgramacionEstimadaGanttDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<ProgramacionEstimadaGantt>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La programacionEstimada no existe";
                    return respuesta;
                }
                objetoEncontrado.FechaInicio = modelo.FechaInicio;
                objetoEncontrado.FechaTermino = modelo.FechaTermino;
                objetoEncontrado.Duracion = modelo.Duracion;
                objetoEncontrado.Progreso = modelo.Progreso;
                objetoEncontrado.Comando = modelo.Comando;
                objetoEncontrado.DesfaseComando = modelo.DesfaseComando;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se puede editar ProgramacionEstimada";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "ProgramacionEstimada editada";
                return respuesta;
            }
            catch(Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> EditarModelo(ProgramacionEstimadaGantt registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La programacionEstimada no existe";
                    return respuesta;
                }
                objetoEncontrado.FechaInicio = registro.FechaInicio;
                objetoEncontrado.FechaTermino = registro.FechaTermino;
                objetoEncontrado.Duracion = registro.Duracion;
                objetoEncontrado.Progreso = registro.Progreso;
                objetoEncontrado.Comando = registro.Comando;
                objetoEncontrado.DesfaseComando = registro.DesfaseComando;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se puede editar ProgramacionEstimada";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "ProgramacionEstimada editada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == id);
                if(objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La programacionEstimada no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if(respuesta.Estatus = false)
                {
                    respuesta.Descripcion = "No se pudo eliminar ProgramacionEstimada";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Algo salió mal en la eliminación de ProgramacionEstimada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación de ProgramacionEstimada";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> EliminarMultiple(List<ProgramacionEstimadaGanttDTO> registros)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == registros[0].IdProyecto);
                var resultado = await _Repositorio.EliminarMultiple(query);
                if (!resultado)
                {
                    respuesta.Descripcion = "No se pueden eliminar las programaciones estimadas";
                    respuesta.Estatus = resultado;
                }
                respuesta.Descripcion = "Los registros han sido eliminados";
                respuesta.Estatus = true;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Descripcion = ex.Message;
                respuesta.Estatus = false;
                return respuesta;
            }
        }
    }
}
