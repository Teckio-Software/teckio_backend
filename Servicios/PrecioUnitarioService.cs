using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class PrecioUnitarioService<T> : IPrecioUnitarioService<T> where T : DbContext
    {
        private readonly IGenericRepository<PrecioUnitario, T> _Repositorio;
        private readonly IMapper _Mapper;

        public PrecioUnitarioService(IGenericRepository<PrecioUnitario, T> repositorio, IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<PrecioUnitarioDTO>> ObtenerTodos(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<PrecioUnitarioDTO>>(query);
            }
            catch
            {
                return new List<PrecioUnitarioDTO>();
            }
        }

        public async Task<List<PrecioUnitarioCopiaDTO>> ObtenerTodosParaCopia(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<PrecioUnitarioCopiaDTO>>(query);
            }
            catch
            {
                return new List<PrecioUnitarioCopiaDTO>();
            }
        }

        public async Task<List<PrecioUnitarioDTO>> Estructurar(List<PrecioUnitarioDTO> registros, decimal Indirecto)
        {
            var padres = registros.Where(z => z.IdPrecioUnitarioBase == 0).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                var hijos = await BuscaHijos(registros, padres[i], Indirecto);
                padres[i].Hijos = hijos;
                if (padres[i].TipoPrecioUnitario == 1)
                {
                    if (hijos.Count() > 0)
                    {
                        var PUsinI = hijos.Sum(z => (z.Importe));
                        var PUConIG = PUsinI * Indirecto;
                        decimal PUConIP = 0;
                        if (padres[i].PorcentajeIndirecto > 0)
                        {
                            PUConIP = (PUsinI) * padres[i].PorcentajeIndirecto - (PUsinI);
                        }
                        padres[i].PrecioUnitario = (PUConIG) + (PUConIP);
                        padres[i].Importe = padres[i].PrecioUnitario * padres[i].Cantidad;
                        padres[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", padres[i].PrecioUnitario);
                        padres[i].ImporteConFormato = String.Format("{0:#,##0.00}", padres[i].Importe);
                    }
                    else
                    {
                        var PUConIG = padres[i].CostoUnitario * Indirecto;
                        decimal PUConIP = 0;
                        if (padres[i].PorcentajeIndirecto > 0)
                        {
                            PUConIP = (padres[i].CostoUnitario) * padres[i].PorcentajeIndirecto - (padres[i].CostoUnitario);
                        }
                        padres[i].PrecioUnitario = PUConIG + PUConIP;
                        padres[i].Importe = padres[i].PrecioUnitario * padres[i].Cantidad;
                        padres[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", padres[i].PrecioUnitario);
                        padres[i].ImporteConFormato = String.Format("{0:#,##0.00}", padres[i].Importe);
                    }
                }
                else
                {
                    padres[i].PrecioUnitario = hijos.Sum(z => (z.Importe));
                    padres[i].Importe = padres[i].PrecioUnitario * padres[i].Cantidad;
                    padres[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", padres[i].PrecioUnitario);
                    padres[i].ImporteConFormato = String.Format("{0:#,##0.00}", padres[i].Importe);
                }
                var PU = registros.FindIndex(z => z.Id == padres[i].Id);
                registros[PU].PrecioUnitario = padres[i].PrecioUnitario;
            }
            return padres;
        }

        private async Task<List<PrecioUnitarioDTO>> BuscaHijos(List<PrecioUnitarioDTO> registros, PrecioUnitarioDTO padre, decimal Indirecto)
        {
            var padres = registros.Where(z => z.IdPrecioUnitarioBase == padre.Id).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                var hijos = await BuscaHijos(registros, padres[i], Indirecto);
                padres[i].Hijos = hijos;
                if (padres[i].TipoPrecioUnitario == 1) {
                    if (hijos.Count() > 0)
                    {
                        var PUsinI = hijos.Sum(z => (z.Importe));
                        var PUConIG = PUsinI * Indirecto;
                        decimal PUConIP = 0;
                        if (padres[i].PorcentajeIndirecto > 0)
                        {
                            PUConIP = (PUsinI) * padres[i].PorcentajeIndirecto - (PUsinI);
                        }
                        padres[i].PrecioUnitario = (PUConIG) + (PUConIP);
                        padres[i].Importe = padres[i].PrecioUnitario * padres[i].Cantidad;
                        padres[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", padres[i].PrecioUnitario);
                        padres[i].ImporteConFormato = String.Format("{0:#,##0.00}", padres[i].Importe);
                    }
                    else
                    {
                        var PUConIG = padres[i].CostoUnitario * Indirecto;
                        decimal PUConIP = 0;
                        if (padres[i].PorcentajeIndirecto > 0)
                        {
                            PUConIP = (padres[i].CostoUnitario) * padres[i].PorcentajeIndirecto - (padres[i].CostoUnitario);
                        }
                        padres[i].PrecioUnitario = PUConIG + PUConIP;
                        padres[i].Importe = padres[i].PrecioUnitario * padres[i].Cantidad;
                        padres[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", padres[i].PrecioUnitario);
                        padres[i].ImporteConFormato = String.Format("{0:#,##0.00}", padres[i].Importe);
                    }
                }
                else
                {
                    padres[i].PrecioUnitario = hijos.Sum(z => (z.Importe));
                    padres[i].Importe = padres[i].PrecioUnitario * padres[i].Cantidad;
                    padres[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", padres[i].PrecioUnitario);
                    padres[i].ImporteConFormato = String.Format("{0:#,##0.00}", padres[i].Importe);
                }
                
                var PU = registros.FindIndex(z => z.Id == padres[i].Id);
                registros[PU].PrecioUnitario = padres[i].PrecioUnitario;
            }
            return padres;
        }

        public async Task<List<PrecioUnitarioCopiaDTO>> EstructurarCopia(List<PrecioUnitarioCopiaDTO> registros)
        {
            var padres = registros.Where(z => z.IdPrecioUnitarioBase == 0).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijosCopia(registros, padres[i]);
            }
            return padres;
        }

        private async Task<List<PrecioUnitarioCopiaDTO>> BuscaHijosCopia(List<PrecioUnitarioCopiaDTO> registros, PrecioUnitarioCopiaDTO padre)
        {
            var padres = registros.Where(z => z.IdPrecioUnitarioBase == padre.Id).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijosCopia(registros, padres[i]);
            }
            return padres;
        }

        public async Task<PrecioUnitarioDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<PrecioUnitarioDTO>(query);
            }
            catch
            {
                return new PrecioUnitarioDTO();
            }
        }

        public async Task<PrecioUnitarioDTO> ObtenXIdConcepto(int IdConcepto)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.IdConcepto == IdConcepto);
                return _Mapper.Map<PrecioUnitarioDTO>(query);
            }
            catch
            {
                return new PrecioUnitarioDTO();
            }
        }

        public async Task<RespuestaDTO> Crear(PrecioUnitarioDTO padre)
        {
            padre.EsDetalle = false;
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<PrecioUnitario>(padre));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Precio unitario creado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creacion del precio unitario";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> CrearCopia(PrecioUnitarioCopiaDTO padre)
        {
            padre.EsDetalle = false;
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                padre.EsDetalle = false;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<PrecioUnitario>(padre));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Precio unitario creado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creacion del precio unitario";
                return respuesta;
            }
        }

        public async Task<PrecioUnitarioDTO> CrearYObtener(PrecioUnitarioDTO hijo)
        {
            try
            {
                hijo.EsDetalle = false;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<PrecioUnitario>(hijo));
                if (objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<PrecioUnitarioDTO>(objetoCreado);
            }
            catch
            {
                return new PrecioUnitarioDTO();
            }
        }

        public async Task<PrecioUnitarioCopiaDTO> CrearYObtenerCopia(PrecioUnitarioCopiaDTO hijo)
        {
            try
            {
                hijo.EsDetalle = false;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<PrecioUnitario>(hijo));
                if (objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<PrecioUnitarioCopiaDTO>(objetoCreado);
            }
            catch
            {
                return new PrecioUnitarioCopiaDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(PrecioUnitarioDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<PrecioUnitario>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El precioUnitario no existe";
                    return respuesta;
                }
                objetoEncontrado.Cantidad = registro.Cantidad;
                objetoEncontrado.CantidadExcedente = registro.CantidadExcedente;
                objetoEncontrado.TipoPrecioUnitario = registro.TipoPrecioUnitario;
                objetoEncontrado.IdProyecto = registro.IdProyecto;
                objetoEncontrado.Nivel = registro.Nivel;
                objetoEncontrado.IdPrecioUnitarioBase = registro.IdPrecioUnitarioBase;
                objetoEncontrado.IdConcepto = registro.IdConcepto;
                objetoEncontrado.EsDetalle = false;

                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "Precio unitario editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del precio Unitario";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El precio unitario no existe";
                    return respuesta;
                }
                bool eliminado = await _Repositorio.Eliminar(objetoEncontrado);
                if (!eliminado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Precio unitario eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del precio unitario";
                return respuesta;
            }
        }
    }
}