using AutoMapper;
using ERP_TECKIO.DTO.Usuario;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class EmpleadoService<T> : IEmpleadoService<T> where T : DbContext
    {
        private readonly IGenericRepository<Empleado, T> _Repository;
        private readonly IMapper _Mapper;
        public EmpleadoService(
            IGenericRepository<Empleado, T> Repository,
            IMapper Mapper 
            )
        {
            _Repository = Repository;
            _Mapper = Mapper;
        }
        public async Task<RespuestaDTO> Crear(EmpleadoDTO objeto)
        {
            var respuesta = new RespuestaDTO();
            var resultado = await _Repository.Crear(_Mapper.Map<Empleado>(objeto));
            if (resultado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creo el empleado";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creo el empleado";
            return respuesta;
        }

        public async Task<RespuestaDTO> Editar(EmpleadoDTO objeto)
        {
            var respuesta = new RespuestaDTO();

            var objetoEncontrado = await _Repository.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El empleado no existe";
                return respuesta;
            }
            objetoEncontrado.IdUser = objeto.IdUser;
            objetoEncontrado.Nombre = objeto.Nombre;
            objetoEncontrado.ApellidoPaterno = objeto.ApellidoPaterno;
            objetoEncontrado.ApellidoMaterno = objeto.ApellidoMaterno;
            objetoEncontrado.Curp = objeto.Curp;
            objetoEncontrado.Rfc = objeto.Rfc;
            objetoEncontrado.SeguroSocial = objeto.SeguroSocial;
            objetoEncontrado.SalarioDiario = objeto.SalarioDiario;
            objetoEncontrado.Estatus = objeto.Estatus;
            objetoEncontrado.FechaRelacionLaboral = objeto.FechaRelacionLaboral;
            objetoEncontrado.FechaTerminoRelacionLaboral = objeto.FechaTerminoRelacionLaboral;

            var editar = await _Repository.Editar(objetoEncontrado);
            if (!editar) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se edito el empleado";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se edito el empleado";
            return respuesta;
        }

        public async Task<RespuestaDTO> Eliminar(int IdEmpleado)
        {
            var respuesta = new RespuestaDTO();

            var objetoEncontrado = await _Repository.Obtener(z => z.Id == IdEmpleado);
            if (objetoEncontrado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El empleado no existe";
                return respuesta;
            }
            var eliminar = await _Repository.Eliminar(objetoEncontrado);
            if (!eliminar)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se elimino el empleado";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se elimino el empleado";
            return respuesta;
        }

        public async Task<List<EmpleadoDTO>> ObtenerTodos()
        {
            var lista = await _Repository.ObtenerTodos();
            return _Mapper.Map<List<EmpleadoDTO>>(lista);
        }

        public async Task<EmpleadoDTO> ObtenerXId(int IdEmpleado)
        {
            var objeto = await _Repository.ObtenerTodos();
            return _Mapper.Map<EmpleadoDTO>(objeto);
        }

        public async Task<EmpleadoDTO> ObtenerXIdUser(int IdUser)
        {
            var objeto = await _Repository.Obtener(z => z.IdUser == IdUser);
            return _Mapper.Map<EmpleadoDTO>(objeto);
        }
    }
}
