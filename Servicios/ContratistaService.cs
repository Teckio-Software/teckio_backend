using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public class ContratistaService<TContext> : IContratistaService<TContext> where TContext : DbContext
    {
        private readonly IGenericRepository<Contratista, TContext> _Repositorio;

        private readonly IMapper _Mapper;

        public ContratistaService(
            IMapper mapper,
            IGenericRepository<Contratista, TContext> _repositorio)
        {
            _Mapper = mapper;
            _Repositorio = _repositorio;
        }

        public async Task<ContratistaDTO> CrearYObtener(ContratistaDTO modelo)
        {
            try
            {
                modelo.IdCuentaContable = null;
                modelo.IdIvaAcreditableContable = null;
                modelo.IdIvaPorAcreditar = null;
                modelo.IdCuentaAnticipos = null;
                modelo.IdCuentaRetencionISR = null;
                modelo.IdCuentaRetencionIVA = null;
                modelo.IdEgresosIvaExento = null;
                modelo.IdEgresosIvaGravable = null;
                modelo.IdIvaAcreditableFiscal = null;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Contratista>(modelo));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<ContratistaDTO>(objetoCreado);
            }
            catch
            {
                return new ContratistaDTO();
            }
        }
        
        public async Task<RespuestaDTO> Editar(ContratistaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Contratista>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El contratista no existe";
                    return respuesta;
                }
                objetoEncontrado.RazonSocial = modelo.RazonSocial;
                objetoEncontrado.Rfc = modelo.Rfc;
                objetoEncontrado.EsProveedorMaterial = modelo.EsProveedorMaterial;
                objetoEncontrado.EsProveedorServicio = modelo.EsProveedorServicio;
                objetoEncontrado.Telefono = modelo.Telefono;
                objetoEncontrado.Email = modelo.Email;
                objetoEncontrado.Domicilio = modelo.Domicilio;
                objetoEncontrado.NExterior = modelo.NExterior;
                objetoEncontrado.Colonia = modelo.Colonia;
                objetoEncontrado.Municipio = modelo.Municipio;
                objetoEncontrado.CodigoPostal = modelo.CodigoPostal;
                objetoEncontrado.RepresentanteLegal = modelo.RepresentanteLegal;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Registro editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);

                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El contratista no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Contratista eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del contratista";
                return respuesta;
            }
        }

        public async Task<List<ContratistaDTO>> ObtenTodos()
        {
            var query = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<ContratistaDTO>>(query);
        }

        public async Task<ContratistaDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<ContratistaDTO>(query);
            }
            catch
            {
                return new ContratistaDTO();
            }
        }

        public async Task<ContratistaDTO> ObtenXRfc(string Rfc)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Rfc == Rfc);
                return _Mapper.Map<ContratistaDTO>(query);
            }
            catch
            {
                return new ContratistaDTO();
            }
        }
    }
}
