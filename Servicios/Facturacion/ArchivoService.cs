using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ArchivoService<TContext> : IArchivoService<TContext> where TContext : DbContext
    {
        private readonly IGenericRepository<Archivo, TContext> _Repositorio;
        private readonly IMapper _Mapper;

        public ArchivoService(
            IGenericRepository<Archivo, TContext> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<bool> Crear(ArchivoDTO parametro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Archivo>(parametro));
            if (objetoCreado.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<ArchivoDTO> CrearYObtener(ArchivoDTO parametro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Archivo>(parametro));
            if (objetoCreado.Id == 0)
                return new ArchivoDTO();
            return _Mapper.Map<ArchivoDTO>(objetoCreado);
        }

        public async Task<bool> Editar(ArchivoDTO parametro)
        {
            var modelo = _Mapper.Map<Archivo>(parametro);
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
            if (objetoEncontrado == null)
            {
                return false;
            }
            objetoEncontrado.Nombre = modelo.Nombre;
            objetoEncontrado.Ruta = modelo.Ruta;
            var respuesta = await _Repositorio.Editar(objetoEncontrado);

            return respuesta;
        }

        public async Task<bool> Eliminar(int Id)
        {
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);
            if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
            {
                return false;
            }
            var respuesta = await _Repositorio.Eliminar(objetoEncontrado);
            return respuesta;
        }

        public async Task<List<ArchivoDTO>> ObtenTodos()
        {
            var lista = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<ArchivoDTO>>(lista);
        }

        public async Task<List<ArchivoDTO>> ObtenXContenido(string contenido)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.Nombre.ToLower().Contains(contenido.ToLower()));
            return _Mapper.Map<List<ArchivoDTO>>(lista);
        }

        public async Task<ArchivoDTO> ObtenXId(int Id)
        {
            var lista = await _Repositorio.Obtener(z => z.Id == Id);
            return _Mapper.Map<ArchivoDTO>(lista);
        }

        public async Task<List<ArchivoDTO>> ObtenXNombre(string Nombre)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.Nombre == Nombre);
            return _Mapper.Map<List<ArchivoDTO>>(lista);
        }
    }
}
