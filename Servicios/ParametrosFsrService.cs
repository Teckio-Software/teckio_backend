using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Presupuesto;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ParametrosFsrService<T> : IParametrosFsrService<T> where T : DbContext
    {
        private readonly IGenericRepository<ParametrosFsr, T> _Repositorio;
        private readonly IMapper _Mapper;
        public ParametrosFsrService(
            IGenericRepository<ParametrosFsr, T> Repositorio,
            IMapper Mapper
            ) {
            _Repositorio = Repositorio;
            _Mapper = Mapper;
        }

        public async Task<RespuestaDTO> Crear(ParametrosFsrDTO registro)
        {
            var respuesta = new RespuestaDTO();
            var objeto = await _Repositorio.Crear(_Mapper.Map<ParametrosFsr>(registro));
            if (objeto.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se crearon los parametros";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se crearon los parametros";
            return respuesta;
        }

        public Task<ParametrosFsrDTO> CrearYObtener(ParametrosFsrDTO registro)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDTO> Editar(ParametrosFsrDTO registro)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se encontro el registro";
                return respuesta;
            }

            objetoEncontrado.RiesgoTrabajo = registro.RiesgoTrabajo;
            objetoEncontrado.CuotaFija = registro.CuotaFija;
            objetoEncontrado.AplicacionExcedente = registro.AplicacionExcedente;
            objetoEncontrado.PrestacionDinero = registro.PrestacionDinero;
            objetoEncontrado.GastoMedico = registro.GastoMedico;
            objetoEncontrado.InvalidezVida = registro.InvalidezVida;
            objetoEncontrado.Retiro = registro.Retiro;
            objetoEncontrado.PrestaconSocial = registro.PrestaconSocial;
            objetoEncontrado.Infonavit = registro.Infonavit;
            objetoEncontrado.UMA = registro.UMA;

            var editar = await _Repositorio.Editar(objetoEncontrado);
            if (!editar) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se edito el registro";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se edito el registro";
            return respuesta;
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se encontro el registro";
                return respuesta;
            }
            var eliminar = await _Repositorio.Eliminar(objetoEncontrado);
            if (!eliminar) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se elimino el registro";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se elimino el registro";
            return respuesta;
        }

        public async Task<ParametrosFsrDTO> ObtenerXIdProyecto(int IdProyecto)
        {
            var objeto = await _Repositorio.Obtener(z => z.IdProyecto == IdProyecto);
            return _Mapper.Map<ParametrosFsrDTO>(objeto);
        }

        public Task<ParametrosFsrDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
