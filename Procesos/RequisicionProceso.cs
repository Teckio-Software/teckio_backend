using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class RequisicionProceso<T> where T : DbContext
    {
        private readonly IRequisicionService<T> _requisicionService;

        private readonly IInsumoXRequisicionService<T> _insumoXRequisicionService;

        private readonly IInsumoService<T> _insumoService;
        private readonly IProyectoService<T> _proyectoService;
        private readonly ActualizaEstatusSubProceso<T> _actualizaEstatusSubProceso;
        public RequisicionProceso(IInsumoXRequisicionService<T> insumoXRequisicionService, IRequisicionService<T> requisicionService, IInsumoService<T> insumoService
            , IProyectoService<T> proyectoService, ActualizaEstatusSubProceso<T> actualizaEstatusSubProceso)
        {
            _insumoXRequisicionService = insumoXRequisicionService;
            _requisicionService = requisicionService;
            _insumoService = insumoService;
            _proyectoService = proyectoService;
            _actualizaEstatusSubProceso = actualizaEstatusSubProceso;
        }

        public async Task<RespuestaDTO> CrearRequisicion(RequisicionCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();

            RequisicionDTO requisicion = new RequisicionDTO();

            requisicion.IdProyecto = parametros.IdProyecto;
            requisicion.Observaciones = "";
            requisicion.Residente = "";
            requisicion.EstatusRequisicion = 1;
            requisicion.EstatusInsumosComprados = 0;
            requisicion.EstatusInsumosSurtIdos = 0;

            var requisiciones = await _requisicionService.ObtenXIdProyecto(parametros.IdProyecto);
            var proyecto = await _proyectoService.ObtenXId(parametros.IdProyecto);
            var codigoProyecto = proyecto.CodigoProyecto.Substring(0, 3);
            string numero = "";
            if (requisiciones.Count <= 0)
            {
                numero = codigoProyecto + "_R_1";
            }
            else
            {
                numero = codigoProyecto + "_R_" + (requisiciones.Count() + 1);
            }
            requisicion.NoRequisicion = numero.ToString();
            requisicion.PersonaSolicitante = usuarioNombre[0].Value;
            var crearobjetoR = await _requisicionService.CrearYObtener(requisicion);
            if (crearobjetoR.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la requisición";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creo la requisicion";
            return respuesta;
        }

        public async Task<RespuestaDTO> EditarRequisicion(RequisicionDTO parametros) {
            RespuestaDTO respuesta = new RespuestaDTO();
            var editar = await _requisicionService.Editar(parametros);
            if (!editar.Estatus) { 
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se edito";
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "registro editado";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearInsumoXRequisicion(InsumoXRequisicionCreacionDTO parametro) {
            var respuesta = new RespuestaDTO();
            if (parametro.Unidad == "" ||
                   parametro.Cantidad == 0 ||
                   parametro.Descripcion == "" ||
                   parametro.Observaciones == ""
                   )
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Capture todos los campos";
                return respuesta;
            }
            var requisicion = await _requisicionService.ObtenXId(parametro.IdRequisicion);
            var insumosXRequisicion = await _insumoXRequisicionService.ObtenXIdRequisicion(requisicion.Id);
            var insumoXRequsicion = new InsumoXRequisicionDTO();
            var listaInsumos = await _insumoService.ObtenerInsumoXProyecto(requisicion.IdProyecto);
            var objetoinsumo = listaInsumos.Where(z => z.Descripcion.ToLower() == parametro.Descripcion.ToLower() && z.Unidad.ToLower() == parametro.Unidad.ToLower()).ToList();
            InsumoDTO crearInsumo = new InsumoDTO();
            if (objetoinsumo.Count <= 0)
            {
                crearInsumo = await _insumoService.CrearYObtener(new InsumoCreacionDTO()
                {
                    Descripcion = parametro.Descripcion,
                    Unidad = parametro.Unidad,
                    IdProyecto = requisicion.IdProyecto,
                    idFamiliaInsumo = null,
                    idTipoInsumo = parametro.idTipoInsumo,
                    CostoUnitario = 0,
                    Codigo = requisicion.NoRequisicion + (insumosXRequisicion.Count() + 1).ToString(),
                });
            }
            else
            {
                crearInsumo = objetoinsumo[0];
            }
            var insumosXRequisicionFiltrado = insumosXRequisicion.Where(z => z.IdInsumo == crearInsumo.id).ToList();
            if (insumosXRequisicionFiltrado.Count >= 1)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ya existe este insumo";
                return respuesta;
            }

            insumoXRequsicion.IdRequisicion = requisicion.Id;
            insumoXRequsicion.IdInsumo = crearInsumo.id;
            insumoXRequsicion.Denominacion = parametro.Denominacion;
            insumoXRequsicion.PersonaIniciales = requisicion.Residente;
            insumoXRequsicion.Folio = (insumosXRequisicion.Count() + 1).ToString();
            insumoXRequsicion.Cantidad = parametro.Cantidad;
            insumoXRequsicion.CantidadComprada = 0;
            insumoXRequsicion.CantidadEnAlmacen = 0;
            insumoXRequsicion.Observaciones = parametro.Observaciones;
            insumoXRequsicion.FechaEntrega = parametro.FechaEntrega;
            insumoXRequsicion.EstatusInsumoComprado = 0;
            insumoXRequsicion.EstatusInsumoRequisicion = 1;
            insumoXRequsicion.EstatusInsumoSurtido = 0;

            var crearInsumoXRequisicion = await _insumoXRequisicionService.Crear(insumoXRequsicion);
            if (!crearInsumoXRequisicion.Estatus) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creo el insumo";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creo el insumo";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearRequisicion1(RequisicionCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            if (usuarioNombre.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No esta logueado";
                return respuesta;
            }
            if (parametros.IdProyecto <= 0 ||
                parametros.Observaciones == "" ||
                parametros.ListaInsumosRequisicion.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Capture todos los campos";
                return respuesta;
            }
            for (int i = 0; i < parametros.ListaInsumosRequisicion.Count; i++)
            {
                if (parametros.ListaInsumosRequisicion[i].Unidad == "" ||
                   parametros.ListaInsumosRequisicion[i].Cantidad == 0 ||
                   //parametros.ListaInsumosRequisicion[i].PersonaIniciales ==""||
                   parametros.ListaInsumosRequisicion[i].Descripcion == "" ||
                   parametros.ListaInsumosRequisicion[i].Observaciones == ""
                   ) {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Capture todos los campos";
                    return respuesta;
                }
            }
            RequisicionDTO requisicion = new RequisicionDTO();

            requisicion.IdProyecto = parametros.IdProyecto;
            requisicion.Observaciones = parametros.Observaciones;
            requisicion.Residente = parametros.Residente;
            requisicion.EstatusRequisicion = 1;
            requisicion.EstatusInsumosComprados = 0;
            requisicion.EstatusInsumosSurtIdos = 0;

            var requisiciones = await _requisicionService.ObtenXIdProyecto(parametros.IdProyecto);
            var proyecto = await _proyectoService.ObtenXId(parametros.IdProyecto);
            var codigoProyecto = proyecto.CodigoProyecto.Substring(0, 3);
            string numero = "";
            if (requisiciones.Count <= 0)
            {
                numero = codigoProyecto+"_R_1";
            }
            else {
               
                var ultimoObjeto = requisiciones.Last();
                numero = codigoProyecto+"_R_"+(requisiciones.Count()+1);
            }
            requisicion.NoRequisicion = numero.ToString();
            requisicion.PersonaSolicitante = usuarioNombre[0].Value;

            var crearobjetoR = await _requisicionService.CrearYObtener(requisicion);
            if (crearobjetoR.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la requisición";
                return respuesta;
            }

            var listaInsumos = await _insumoService.ObtenerInsumoXProyecto(parametros.IdProyecto);
            
            List<InsumoXRequisicionDTO> insumos = new List<InsumoXRequisicionDTO>();

            for (int i = 0; i < parametros.ListaInsumosRequisicion.Count; i++) {
                var objetoinsumo = listaInsumos.Where(z => z.Descripcion.ToLower() == parametros.ListaInsumosRequisicion[i].Descripcion.ToLower() && z.Unidad.ToLower() == parametros.ListaInsumosRequisicion[i].Unidad.ToLower()).ToList();
                InsumoDTO crearInsumo = new InsumoDTO();
                if (objetoinsumo.Count <= 0) {
                    crearInsumo = await _insumoService.CrearYObtener(new InsumoCreacionDTO()
                    {
                        Descripcion = parametros.ListaInsumosRequisicion[i].Descripcion,
                        Unidad = parametros.ListaInsumosRequisicion[i].Unidad,
                        IdProyecto = parametros.IdProyecto,
                        idFamiliaInsumo = null,
                        idTipoInsumo = parametros.ListaInsumosRequisicion[i].idTipoInsumo,
                        CostoUnitario = 0,
                        Codigo = crearobjetoR.NoRequisicion+(i+1).ToString(),
                    });
                }
                else
                {
                    crearInsumo = objetoinsumo[0];
                }
                listaInsumos.Add(crearInsumo);
                var insumosXRequisicionFiltrado = insumos.Where(z => z.PersonaIniciales.ToLower() == parametros.ListaInsumosRequisicion[i].PersonaIniciales.ToLower() 
                && z.IdInsumo == crearInsumo.id).ToList();
                if (insumosXRequisicionFiltrado.Count > 1)
                {
                    continue;
                }
                insumos.Add(new InsumoXRequisicionDTO()
                {
                    IdRequisicion = crearobjetoR.Id,
                    IdInsumo = crearInsumo.id,
                    Denominacion = parametros.ListaInsumosRequisicion[i].Denominacion,
                    PersonaIniciales = parametros.Residente,
                    Folio = (i + 1).ToString(),
                    Cantidad = parametros.ListaInsumosRequisicion[i].Cantidad,
                    CantidadComprada = 0,
                    CantidadEnAlmacen = 0,
                    Observaciones = parametros.ListaInsumosRequisicion[i].Observaciones,
                    FechaEntrega = parametros.ListaInsumosRequisicion[i].FechaEntrega,
                    EstatusInsumoComprado = 0,
                    EstatusInsumoRequisicion = 1,
                    EstatusInsumoSurtido = 0
                });
            }

            var resultado = await _insumoXRequisicionService.CrearLista(insumos);
            if (!resultado) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un problema";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Requisicón creada";
            return respuesta;
        }

        public async Task<List<InsumoXRequisicionDTO>> InsumosXRequisicion(int IdRequisicion) {
            var requisicion =  await _requisicionService.ObtenXId(IdRequisicion);
            var idProyecto = requisicion.IdProyecto;
            var insumos = await _insumoService.ObtenXIdProyecto(idProyecto);
            var insumosXRequicision = await _insumoXRequisicionService.ObtenXIdRequisicion(IdRequisicion);

            List<InsumoXRequisicionDTO> listInsumosXRequisicion = new List<InsumoXRequisicionDTO>();
            foreach (var item in insumosXRequicision) {
                var filtro = insumos.Where(z => z.id == item.IdInsumo).ToList();
                if (filtro.Count() > 0) {
                    string descripcionEstatusInsumoComprado = "";
                    switch (item.EstatusInsumoComprado)
                    {
                        case 0:
                            descripcionEstatusInsumoComprado = "No hay ordenes de compra";
                            break;
                        case 1:
                            descripcionEstatusInsumoComprado = "Capturada";
                            break;
                        case 2:
                            descripcionEstatusInsumoComprado = "Autorizada";
                            break;
                        case 3:
                            descripcionEstatusInsumoComprado = "Comprada";
                            break;
                        case 4:
                            descripcionEstatusInsumoComprado = "Cancelada";
                            break;
                        default:
                            descripcionEstatusInsumoComprado = "";
                            break;
                    }
                    string descripcionEstatusInsumoSurtido = "";
                    switch (item.EstatusInsumoSurtido)
                    {
                        case 0:
                            descripcionEstatusInsumoSurtido = "No hay entradas de material";
                            break;
                        case 1:
                            descripcionEstatusInsumoSurtido = "Pendiente";
                            break;
                        case 2:
                            descripcionEstatusInsumoSurtido = "Surtido Parcialmente";
                            break;
                        case 3:
                            descripcionEstatusInsumoSurtido = "Surtido Totalmente";
                            break;
                        default:
                            descripcionEstatusInsumoSurtido = "";
                            break;
                    }
                    listInsumosXRequisicion.Add(new InsumoXRequisicionDTO
                    {
                        Id = item.Id,
                        Descripcion = filtro[0].Descripcion,
                        Unidad = filtro[0].Unidad,
                        Denominacion = item.Denominacion,
                        PersonaIniciales = item.PersonaIniciales,
                        Folio = item.Folio,
                        Cantidad = item.Cantidad,
                        Observaciones = item.Observaciones,
                        FechaEntrega = item.FechaEntrega,
                        EstatusInsumoCompradoDescripcion = descripcionEstatusInsumoComprado,
                        EstatusInsumoSurtidoDescripcion = descripcionEstatusInsumoSurtido,
                        IdInsumo = item.IdInsumo
                    });
                }
            }

            return listInsumosXRequisicion;
        }
        public async Task ActualizarEstatusRequisicion(int idRequisicion)
        {
            var insumosXidRequisicion = await _insumoXRequisicionService.ObtenXIdRequisicion(idRequisicion);
            var insumosCancelados = insumosXidRequisicion.Where(z => z.EstatusInsumoRequisicion == 4);
            var insumosComprados = insumosXidRequisicion.Where(z => z.EstatusInsumoRequisicion == 3);
            var insumosAutorizados = insumosXidRequisicion.Where(z => z.EstatusInsumoRequisicion == 2);
            var insumosCapturados = insumosXidRequisicion.Where(z => z.EstatusInsumoRequisicion == 1);
            var totales = insumosXidRequisicion.Count() - insumosCancelados.Count();

            if (insumosCancelados.Count() == insumosXidRequisicion.Count())
            {
                await _requisicionService.ActualizarEstatusCancelar(idRequisicion);
            }
            if (insumosComprados.Count() == totales)
            {
               await _requisicionService.ActualizarEstatusComprar(idRequisicion);
            }
            if (insumosAutorizados.Count() >= 1)
            {
                await _requisicionService.ActualizarEstatusAutorizar(idRequisicion);
            }
        }
        public async Task<RespuestaDTO> AutorizarTodos(int idRequisicion)
        {
            RespuestaDTO respuesta = new RespuestaDTO();

            var insumosXidRequisicion = await _insumoXRequisicionService.ObtenXIdRequisicion(idRequisicion);
            var insumosXrequisicionCapturados = insumosXidRequisicion.Where(z => z.EstatusInsumoRequisicion == 1).ToList();

            foreach (var element in insumosXrequisicionCapturados)
            {
                var AutorizarInsumo = await _insumoXRequisicionService.ActualizarEstatusAutorizar(element.Id);
                
                if (!AutorizarInsumo.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se autorizo el insumo";
                    return respuesta;
                }
            }
            ActualizarEstatusRequisicion(idRequisicion);
            respuesta.Estatus = true;
            respuesta.Descripcion = "Insumos autorizados";
            return respuesta;

        }

        public async Task<List<RequisicionDTO>> ListarRequisicionesXIdProyecto(int IdProyecto)
        {
            List<RequisicionDTO> lista = new List<RequisicionDTO>();
            var requisiciones = await _requisicionService.ObtenXIdProyecto(IdProyecto);
            var ordenUltimoaPrimero = requisiciones.OrderBy(x => x.Id).Reverse();
            foreach (var element in ordenUltimoaPrimero)
            {
                await _actualizaEstatusSubProceso.ActualizaEstatusRequisicionInsumos(element.Id);
                string descripcionEstatusInsumosComprados = "";
                switch (element.EstatusInsumosComprados)
                {
                    case 0:
                        descripcionEstatusInsumosComprados = "No hay cotizaciones";
                        break;
                    case 1:
                        descripcionEstatusInsumosComprados = "Capturada";
                        break;
                    case 2:
                        descripcionEstatusInsumosComprados = "Autorizada";
                        break;
                    case 3:
                        descripcionEstatusInsumosComprados = "Comprada";
                        break;
                    case 4:
                        descripcionEstatusInsumosComprados = "Cancelada";
                        break;
                    default:
                        descripcionEstatusInsumosComprados = "";
                        break;
                }
                string descripcionEstatusInsumosSurtidos = "";
                switch (element.EstatusInsumosSurtIdos)
                {
                    case 0:
                        descripcionEstatusInsumosSurtidos = "No hay ordenes compra";
                        break;
                    case 1:
                        descripcionEstatusInsumosSurtidos = "Pendiente";
                        break;
                    case 2:
                        descripcionEstatusInsumosSurtidos = "Surtido parcialmente";
                        break;
                    case 3:
                        descripcionEstatusInsumosSurtidos = "Surtido totalmente";
                        break;
                    default:
                        descripcionEstatusInsumosSurtidos = "";
                        break;
                }
                lista.Add(new RequisicionDTO()
                {
                    Id = element.Id,
                    IdProyecto = element.IdProyecto,
                    NoRequisicion = element.NoRequisicion,
                    PersonaSolicitante = element.PersonaSolicitante,
                    Observaciones = element.Observaciones,
                    FechaRegistro = element.FechaRegistro,
                    Residente = element.Residente == null? "S/D" : element.Residente,
                    EstatusRequisicion = element.EstatusRequisicion,
                    EstatusInsumosComprados = element.EstatusInsumosComprados,
                    EstatusInsumosCompradosDescripcion = descripcionEstatusInsumosComprados,
                    EstatusInsumosSurtIdos = element.EstatusInsumosSurtIdos,
                    EstatusInsumosSurtIdosDescripcion = descripcionEstatusInsumosSurtidos
                });
            }
            return lista;
        }

        public async Task<RespuestaDTO> AutorizarXIdInsumoRequisicion(int idInsumoRequisicion)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var insumoXRequisicion = await _insumoXRequisicionService.ObtenXId(idInsumoRequisicion);
            if (insumoXRequisicion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se encontró el insumo cotizado";
                return respuesta;
            }
            if (insumoXRequisicion.EstatusInsumoRequisicion == 2)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El inusmo ya está autorizado";
                return respuesta;
            }
            else
            {
                var AutorizarInsumo = await _insumoXRequisicionService.ActualizarEstatusAutorizar(idInsumoRequisicion);
                if (!AutorizarInsumo.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se autorizo el insumo";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo autorizado";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> AutorizarInsumosRequisicionSeleccionados(List<InsumoXRequisicionDTO> seleccionados)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (seleccionados.Count <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se seleccionaron insumos cotizados";
                return respuesta;
            }
            foreach (var item in seleccionados)
            {
                var verificacion = await _insumoXRequisicionService.ObtenXId(item.Id);
                if (verificacion.EstatusInsumoRequisicion  != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se autorizaron los insumos cotizados";
                    return respuesta;
                }
            }
            foreach (var item in seleccionados)
            {
                var AutorizarInsumo = await _insumoXRequisicionService.ActualizarEstatusAutorizar(item.Id);
                if (!AutorizarInsumo.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se autorizaron los insumos cotizados";
                    return respuesta;
                }
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Los insumos han sido autorizados";
            return respuesta;
        }
    }
}
