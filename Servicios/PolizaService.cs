using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class PolizaService<T> : IPolizaService<T> where T : DbContext
    {
        private readonly IGenericRepository<Poliza, T> _Repositorio;
        private readonly IMapper _Mapper;
        public PolizaService(
            IGenericRepository<Poliza, T> repositorio
            , IMapper mapper) {
                _Repositorio = repositorio;
                _Mapper = mapper;
            }

        public async Task<List<PolizaDTO>> ObtenTodosXEmpresa()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<PolizaDTO>>(lista);
            }
            catch
            {
                return new List<PolizaDTO>();
            }
        }

        public async Task<PolizaDTO> ObtenXId(int IdEmpresa)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<PolizaDTO>(lista);
            }
            catch
            {
                return new PolizaDTO();
            }
        }

        public async Task<RespuestaDTO> Crear(PolizaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Poliza>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Poliza creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación de la poliza";
                return respuesta;
            }
        }

        public async Task<PolizaDTO> CrearYObtener(PolizaDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Poliza>(modelo));
                if (objetoCreado.Id == 0)
                {
                    return new PolizaDTO();
                }
                return _Mapper.Map<PolizaDTO>(objetoCreado);
            }
            catch
            {
                return new PolizaDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(PolizaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Poliza>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La poliza no existe";
                    return respuesta;
                }
                objetoEncontrado.Estatus = parametro.Estatus;
                objetoEncontrado.Concepto = parametro.Concepto;
                objetoEncontrado.Folio = parametro.Folio;
                objetoEncontrado.NumeroPoliza = parametro.NumeroPoliza;
                objetoEncontrado.FechaAlta = parametro.FechaAlta;
                objetoEncontrado.FechaPoliza = parametro.FechaPoliza;
                objetoEncontrado.Observaciones = parametro.Observaciones;
                objetoEncontrado.OrigenDePoliza = parametro.OrigenDePoliza;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Poliza editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la poliza";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Contabilizar(PolizaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Poliza>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if(objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La poliza no existe";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 1;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Poliza editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la poliza";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Auditar(PolizaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Poliza>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La poliza no existe";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 2;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Poliza editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la poliza";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Cancelar(PolizaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Poliza>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La poliza no existe";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 3;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Poliza editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la poliza";
                return respuesta;
            }
        }
    }
}
