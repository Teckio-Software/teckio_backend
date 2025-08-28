using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ParametrosImpresionPuService<T> : IParametrosImpresionPuService<T> where T : DbContext
    {
        private readonly IGenericRepository<ParametrosImpresionPu, T> _Repository;
        private readonly IMapper _Mapper;
        public ParametrosImpresionPuService(
            IGenericRepository<ParametrosImpresionPu, T> repository,
            IMapper mapper
            )
        {
            _Repository = repository;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(ParametrosImpresionPuDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _Mapper.Map<ParametrosImpresionPu>(modelo);
                var resultado = await _Repository.Crear(objeto);
                if (resultado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrió un error al intentar crear los parámetros de impresión";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Parámetros de impresión creados exitosamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar crear los parámetros de impresión";
                return respuesta;
            }
        }

        public async Task<ParametrosImpresionPuDTO> CrearYObtener(ParametrosImpresionPuDTO modelo)
        {
            try
            {
                var objeto = _Mapper.Map<ParametrosImpresionPu>(modelo);
                var resultado = await _Repository.Crear(objeto);
                if (resultado.Id <= 0)
                {
                    return new ParametrosImpresionPuDTO();
                }
                return _Mapper.Map<ParametrosImpresionPuDTO>(objeto);
            }
            catch
            {
                return new ParametrosImpresionPuDTO();

            }
        }

        public async Task<RespuestaDTO> Editar(ParametrosImpresionPuDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _Repository.Obtener(p=>p.Id==modelo.Id);
                if(objeto.Id<=0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontraron los parámetros de impresión";
                    return respuesta;
                }
                objeto.MargenDerecho = modelo.MargenDerecho;
                objeto.MargenIzquierdo = modelo.MargenIzquierdo;
                objeto.MargenSuperior = modelo.MargenSuperior;
                objeto.MargenInferior = modelo.MargenInferior;
                objeto.EncabezadoCentro = modelo.EncabezadoCentro;
                objeto.EncabezadoDerecho = modelo.EncabezadoDerecho;
                objeto.EncabezadoIzquierdo = modelo.EncabezadoIzquierdo;
                objeto.PieCentro = modelo.PieCentro;
                objeto.PieDerecho = modelo.PieDerecho;
                objeto.PieIzquierdo = modelo.PieIzquierdo;
                objeto.Nombre = modelo.Nombre;
                var resultado = await _Repository.Editar(objeto);
                if (!resultado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrió un error al intentar editar los parámetros de impresión";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Parámetros de impresión editados exitosamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar editar los parámetros de impresión";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _Repository.Obtener(p => p.Id == id);
                if (objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontraron los parámetros de impresión";
                    return respuesta;
                }
                
                var resultado = await _Repository.Eliminar(objeto);
                if (!resultado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrió un error al intentar eliminar los parámetros de impresión";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Parámetros de impresión eliminados exitosamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar eliminar los parámetros de impresión";
                return respuesta;
            }
        }

        public async Task<ParametrosImpresionPuDTO> Obtener(int id)
        {
            try
            {
                var objeto = await _Repository.Obtener(p => p.Id == id);
                if (objeto.Id<0)
                {
                    return new ParametrosImpresionPuDTO();
                }
                else
                {
                    return _Mapper.Map<ParametrosImpresionPuDTO>(objeto);
                }

            }
            catch
            {
                return new ParametrosImpresionPuDTO();
            }
        }

        //public async Task<List<ParametrosImpresionPuDTO>> ObtenerPorCliente(int idCliente)
        //{
        //    try
        //    {
        //        var lista = await _Repository.ObtenerTodos(p => p.IdCliente == idCliente);
        //        if (lista.Count > 0)
        //        {
        //            return _Mapper.Map<List<ParametrosImpresionPuDTO>>(lista);
        //        }
        //        else
        //        {
        //            return new List<ParametrosImpresionPuDTO>();
        //        }
        //    }
        //    catch
        //    {
        //        return new List<ParametrosImpresionPuDTO>();
        //    }
        //}

        public async Task<List<ParametrosImpresionPuDTO>> ObtenerTodos()
        {
            try
            {
                var lista = await _Repository.ObtenerTodos();
                if (lista.Count > 0)
                {
                    return _Mapper.Map<List<ParametrosImpresionPuDTO>>(lista);
                }
                else
                {
                    return new List<ParametrosImpresionPuDTO>();
                }
            }
            catch
            {
                return new List<ParametrosImpresionPuDTO>();
            }
        }
    }
}
