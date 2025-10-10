using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ImagenService<T> : IImagenService<T> where T : DbContext
    {
        private readonly IGenericRepository<Imagen, T> _Repository;
        private readonly IMapper _Mapper;

        public ImagenService(
            IGenericRepository<Imagen, T> repository,
            IMapper mapper
            )
        {
            _Repository = repository;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(ImagenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _Mapper.Map<Imagen>(modelo);
                var resultado = await _Repository.Crear(objeto);
                if(resultado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrió un error al intentar crear la imagen";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Imagen creada exitosamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar crear la imagen";
                return respuesta;
            }
        }

        public async Task<ImagenDTO> CrearYObtener(ImagenDTO modelo)
        {
            try
            {
                var objeto = _Mapper.Map<Imagen>(modelo);
                var resultado = await _Repository.Crear(objeto);
                if (resultado.Id <= 0)
                {
                    return new ImagenDTO();
                }
                return _Mapper.Map<ImagenDTO>(objeto);
            }
            catch
            {
                return new ImagenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ImagenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _Repository.Obtener(i=>i.Id==modelo.Id);
                if(objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró la imagen que se intenta editar";
                    return respuesta;
                }
                objeto.Ruta = modelo.Ruta;
                objeto.Seleccionado = modelo.Seleccionado;
                objeto.Tipo = modelo.Tipo;
                var resultado = await _Repository.Editar(objeto);
                if (!resultado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrió un error al intentar editar la imagen";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Imagen editada exitosamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar editar la imagen";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _Repository.Obtener(i => i.Id == id);
                if (objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró la imagen que se intenta eliminar";
                    return respuesta;
                }
                var resultado = await _Repository.Eliminar(objeto);
                if (!resultado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrió un error al intentar eliminar la imagen";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Imagen eliminada exitosamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar eliminar la imagen";
                return respuesta;
            }
        }

        public async Task<ImagenDTO> Obtener(int id)
        {
            try
            {
                var objeto = await _Repository.Obtener(i => i.Id == id);
                if(objeto.Id <= 0)
                {
                    return new ImagenDTO();
                }
                return _Mapper.Map<ImagenDTO>(objeto);
            }
            catch
            {
                return new ImagenDTO();
            }
        }

        public async Task<List<ImagenDTO>> ObtenerTodos()
        {
            try
            {
                var lista = await _Repository.ObtenerTodos();
                if (lista.Count > 0)
                {
                    return _Mapper.Map<List<ImagenDTO>>(lista);
                }
                else
                {
                    return new List<ImagenDTO>();
                }
            }
            catch
            {
                return new List<ImagenDTO>();
            }
        }
    }
}
