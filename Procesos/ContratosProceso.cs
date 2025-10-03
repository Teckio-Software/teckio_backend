using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class ContratosProceso<TContext> where TContext : DbContext
    {
        private readonly IContratoService<TContext> _ContratoService;
        private readonly IDetalleXContratoService<TContext> _DetalleXContrato;
        private readonly IPorcentajeAcumuladoContratoService<TContext> _PorcentajeAcumuladoContrato;
        private readonly PrecioUnitarioProceso<TContext> _PrecioUnitarioProceso;
        private readonly IPeriodoEstimacionesService<TContext> _periodoEstimacionesService;
        private readonly IContratistaService<TContext> _contratistaService;
        private readonly EstimacionesProceso<TContext> _EstmacionesProceso;
        private readonly IProyectoService<TContext> _proyectoService;

        public ContratosProceso(
              PrecioUnitarioProceso<TContext> precioUnitarioProceso
            , IContratoService<TContext> contratoService
            , IDetalleXContratoService<TContext> detalleXContrato
            , IPorcentajeAcumuladoContratoService<TContext> porcentajeAcumuladoContratoService,
              IPeriodoEstimacionesService<TContext> periodoEstimacionesService,
              IContratistaService<TContext> contratistaService,
              EstimacionesProceso<TContext> EstmacionesProceso,
              IProyectoService<TContext> proyectoService
            )
        {
            _PrecioUnitarioProceso = precioUnitarioProceso;
            _ContratoService = contratoService;
            _DetalleXContrato = detalleXContrato;
            _PorcentajeAcumuladoContrato = porcentajeAcumuladoContratoService;
            _periodoEstimacionesService = periodoEstimacionesService;
            _EstmacionesProceso = EstmacionesProceso;
            _contratistaService = contratistaService;
            _proyectoService = proyectoService;
        }

        public async Task<List<ContratoDTO>> ObtenerContratosDestajos(ParametrosParaBuscarContratos parametrosBusqueda)
        {
            var Contratos = await _ContratoService.ObtenerRegistrosXIdProyecto(parametrosBusqueda.IdProyecto);
            var ContratosPorTipo = Contratos.Where(z => z.TipoContrato == parametrosBusqueda.TipoContrato);
            var ContratosFiltrados = Contratos.Where(z => z.IdContratista == parametrosBusqueda.IdContratista).ToList();
            if (ContratosFiltrados.Count() <= 0) { 
                return new List<ContratoDTO>();
            }

            var proyecto = await _proyectoService.ObtenXId(parametrosBusqueda.IdProyecto);
            var codigoProyecto = "";
            if (proyecto.CodigoProyecto.Count() < 3) {
                codigoProyecto = proyecto.CodigoProyecto;
            }
            else
            {
                codigoProyecto = proyecto.CodigoProyecto.Substring(0, 3);
            }

            foreach (var contratos in ContratosFiltrados) {
                var numeroDestajo = "";
                if (contratos.NumeroDestajo < 10) {
                    numeroDestajo = "00"+contratos.NumeroDestajo.ToString();
                }else if (contratos.NumeroDestajo >= 10 && contratos.NumeroDestajo < 100)
                {
                    numeroDestajo = "0" + contratos.NumeroDestajo.ToString();
                }else if (contratos.NumeroDestajo >= 100)
                {
                    numeroDestajo = contratos.NumeroDestajo.ToString();
                }

                contratos.NumeroDestajoDescripcion = codigoProyecto + "_" + numeroDestajo + "_" + contratos.FechaRegistro.ToString();
            }

            return ContratosFiltrados;
        }

        public async Task CrearContratoDestajo(ContratoDTO nuevoContrato)
        {
            nuevoContrato.Estatus = 0;
            var registros = await _ContratoService.ObtenerRegistrosXIdProyecto(nuevoContrato.IdProyecto);
            var registrosFiltrados = registros.Where(z => z.TipoContrato == nuevoContrato.TipoContrato);
            var registrosFinales = registrosFiltrados.Where(z => z.IdContratista == nuevoContrato.IdContratista).OrderBy(z => z.NumeroDestajo).ToList();
            if (registrosFinales.Count > 0)
            {
                nuevoContrato.NumeroDestajo = registrosFinales.Count + 1;
            }
            else
            {
                nuevoContrato.NumeroDestajo = 1;
            }
            var ContratoCreado = await _ContratoService.CrearYObtener(nuevoContrato);
        }

        public async Task EditarContratoDestajo(ContratoDTO nuevoContrato)
        {
            await _ContratoService.Editar(nuevoContrato);
        }

        public async Task<List<DetalleXContratoParaTablaDTO>> ObtenerDetallesDestajos(int idContrato)
        {
            var Registros = new List<DetalleXContratoParaTablaDTO>();
            var Contrato = await _ContratoService.ObtenXId(idContrato);
            var PreciosUnitarios = _PrecioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(Contrato.IdProyecto).Result;
            if (Contrato.TipoContrato == true)
            {
                for (int i = 0; i < PreciosUnitarios.Count; i++)
                {
                    if (PreciosUnitarios[i].TipoPrecioUnitario == 1)
                    {
                        decimal importeMO = 0;
                        var ExplosionConcepto = await _PrecioUnitarioProceso.ObtenerExplosionDeInsumoXConcepto(PreciosUnitarios[i].Id);
                        var InsumosManoDeObra = ExplosionConcepto.Where(z => z.idTipoInsumo == 10000).ToList();
                        for (int j = 0; j < InsumosManoDeObra.Count; j++)
                        {
                            importeMO = importeMO + InsumosManoDeObra[j].Importe;
                        }
                        PreciosUnitarios[i].Importe = importeMO;
                        if (PreciosUnitarios[i].Cantidad > 0)
                        {
                            PreciosUnitarios[i].CostoUnitario = PreciosUnitarios[i].Importe / PreciosUnitarios[i].Cantidad;
                        }
                    }
                }
            }
            var PorcentajesAcumulados = _PorcentajeAcumuladoContrato.ObtenerRegistros().Result;
            var DetallesXContrato = _DetalleXContrato.ObtenerRegistrosXIdContrato(idContrato).Result;
            for (int i = 0; i < PreciosUnitarios.Count; i++)
            {
                var Registro = new DetalleXContratoParaTablaDTO();
                var Detalle = DetallesXContrato.Where(z => z.IdPrecioUnitario == PreciosUnitarios[i].Id).ToList();
                if (Detalle.Count > 0)
                {
                    Registro.IdDetalleXContrato = Detalle[0].Id;
                    Registro.PorcentajeDestajo = Detalle[0].PorcentajeDestajo;
                    Registro.PorcentajeDestajoConFormato = String.Format("{0:#,##0.00}", Registro.PorcentajeDestajo);
                    Registro.FactorDestajo = Detalle[0].FactorDestajo;
                    Registro.FactorDestajoConFormato = String.Format("{0:#,##0.00}", Registro.FactorDestajo);
                    Registro.ImporteDestajo = Detalle[0].ImporteDestajo;
                    Registro.ImporteDestajoConFormato = String.Format("{0:#,##0.00}", Registro.ImporteDestajo);
                }
                else
                {
                    Registro.IdDetalleXContrato = 0;
                    Registro.PorcentajeDestajo = 0;
                    Registro.PorcentajeDestajoConFormato = String.Format("{0:#,##0.00}", Registro.PorcentajeDestajo);
                    Registro.FactorDestajo = 0;
                    Registro.FactorDestajoConFormato = String.Format("{0:#,##0.00}", Registro.FactorDestajo);
                    Registro.ImporteDestajo = 0;
                    Registro.ImporteDestajoConFormato = String.Format("{0:#,##0.00}", Registro.ImporteDestajo);
                }
                var PorcentajeAcumulado = PorcentajesAcumulados.Where(z => z.IdPrecioUnitario == PreciosUnitarios[i].Id).ToList();
                if (PorcentajeAcumulado.Count > 0)
                {
                    Registro.PorcentajeDestajoAcumulado = PorcentajeAcumulado[0].PorcentajeDestajoAcumulado;
                    Registro.PorcentajeDestajoAcumuladoConFormato = String.Format("{0:#,##0.00}", Registro.PorcentajeDestajoAcumulado);
                }
                else
                {
                    Registro.PorcentajeDestajoAcumulado = 0;
                    Registro.PorcentajeDestajoAcumuladoConFormato = String.Format("{0:#,##0.00}", Registro.PorcentajeDestajoAcumulado);
                }
                Registro.IdPrecioUnitario = PreciosUnitarios[i].Id;
                Registro.Codigo = PreciosUnitarios[i].Codigo;
                Registro.Descripcion = PreciosUnitarios[i].Descripcion;
                Registro.Unidad = PreciosUnitarios[i].Unidad;
                Registro.CostoUnitario = PreciosUnitarios[i].CostoUnitario;
                Registro.CostoUnitarioConFormato = String.Format("{0:#,##0.00}", Registro.CostoUnitario);
                Registro.Cantidad = PreciosUnitarios[i].Cantidad;
                Registro.CantidadConFormato = String.Format("{0:#,##0.0000}", Registro.Cantidad);
                Registro.Importe = PreciosUnitarios[i].Importe;
                Registro.ImporteConFormato = String.Format("${0:#,##0.00}", Registro.Importe);
                Registro.TipoPrecioUnitario = PreciosUnitarios[i].TipoPrecioUnitario;
                Registro.IdContrato = idContrato;
                Registro.IdPrecioUnitarioBase = PreciosUnitarios[i].IdPrecioUnitarioBase;
                Registro.Expandido = true;
                Registros.Add(Registro);
            }
            var listaEstructurada = await Estructurar(Registros);
            var listaResult = listaEstructurada.OrderBy(z => z.IdPrecioUnitario).ToList();
            return listaResult;
        }

        public async Task<List<DetalleXContratoParaTablaDTO>> Estructurar(List<DetalleXContratoParaTablaDTO> registros)
        {
            var padres = registros.Where(z => z.IdPrecioUnitarioBase == 0).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijos(registros, padres[i]);
                if (padres[i].Hijos.Count > 0)
                {
                    decimal importeDestajo = 0;
                    decimal importeAcumulado = 0;
                    decimal importeMO = 0;
                    for (int j = 0; j < padres[i].Hijos.Count; j++)
                    {
                        importeDestajo = importeDestajo + padres[i].Hijos[j].ImporteDestajo;
                        importeAcumulado = importeAcumulado + (padres[i].Hijos[j].Importe * (padres[i].Hijos[j].PorcentajeDestajoAcumulado / 100));
                        importeMO = importeMO + padres[i].Hijos[j].Importe;
                    }
                    padres[i].CostoUnitario = importeMO;
                    padres[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", padres[i].CostoUnitario);
                    padres[i].Importe = padres[i].CostoUnitario * padres[i].Cantidad;
                    padres[i].ImporteConFormato = String.Format("${0:#,##0.00}", padres[i].Importe);
                    padres[i].ImporteDestajo = importeDestajo;
                    padres[i].ImporteDestajoConFormato = String.Format("{0:#,##0.00}", padres[i].ImporteDestajo);
                    if (padres[i].Importe > 0)
                    {
                        padres[i].PorcentajeDestajo = (importeDestajo / padres[i].Importe) * 100;
                        padres[i].PorcentajeDestajoAcumulado = (importeAcumulado / padres[i].Importe) * 100;
                    }
                    padres[i].PorcentajeDestajoConFormato = String.Format("{0:#,##0.00}", padres[i].PorcentajeDestajo);
                    padres[i].PorcentajeDestajoAcumuladoConFormato = String.Format("{0:#,##0.00}", padres[i].PorcentajeDestajoAcumulado);
                }
            }
            return padres;
        }

        private async Task<List<DetalleXContratoParaTablaDTO>> BuscaHijos(List<DetalleXContratoParaTablaDTO> registros, DetalleXContratoParaTablaDTO padre)
        {
            var padres = registros.Where(z => z.IdPrecioUnitarioBase == padre.IdPrecioUnitario).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijos(registros, padres[i]);
                if (padres[i].Hijos.Count > 0)
                {
                    decimal importeDestajo = 0;
                    decimal importeAcumulado = 0;
                    decimal importeMO = 0;
                    for (int j = 0; j < padres[i].Hijos.Count; j++)
                    {
                        importeDestajo = importeDestajo + padres[i].Hijos[j].ImporteDestajo;
                        importeAcumulado = importeAcumulado + (padres[i].Hijos[j].Importe * (padres[i].Hijos[j].PorcentajeDestajoAcumulado / 100));
                        importeMO = importeMO + padres[i].Hijos[j].Importe;
                    }
                    padres[i].CostoUnitario = importeMO;
                    padres[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", padres[i].CostoUnitario);
                    padres[i].Importe = padres[i].CostoUnitario * padres[i].Cantidad;
                    padres[i].ImporteConFormato = String.Format("${0:#,##0.00}", padres[i].Importe);
                    padres[i].ImporteDestajo = importeDestajo;
                    padres[i].ImporteDestajoConFormato = String.Format("{0:#,##0.00}", padres[i].ImporteDestajo);
                    if (padres[i].Importe > 0)
                    {
                        padres[i].PorcentajeDestajo = (importeDestajo / padres[i].Importe) * 100;
                        padres[i].PorcentajeDestajoAcumulado = (importeAcumulado / padres[i].Importe) * 100;
                    }
                    padres[i].PorcentajeDestajoConFormato = String.Format("{0:#,##0.00}", padres[i].PorcentajeDestajo);
                    padres[i].PorcentajeDestajoAcumuladoConFormato = String.Format("{0:#,##0.00}", padres[i].PorcentajeDestajoAcumulado);
                }
            }
            return padres;
        }

        public async Task CrearOEditarDetallePadreOHijo(DetalleXContratoParaTablaDTO registro) {
            if (registro.TipoPrecioUnitario == 0) {
                await CrearOEditarDetallePadre(registro);
            }
            else
            {
                await CrearOEditarDetalle(registro);
            }
        }

        public async Task CrearOEditarDetalle(DetalleXContratoParaTablaDTO registro)
        {
            if (registro.IdDetalleXContrato == 0)
            {
                var nuevoRegistro = new DetalleXContratoDTO();
                nuevoRegistro.IdPrecioUnitario = registro.IdPrecioUnitario;
                nuevoRegistro.IdContrato = registro.IdContrato;
                nuevoRegistro.PorcentajeDestajo = registro.PorcentajeDestajo;
                nuevoRegistro.FactorDestajo = registro.FactorDestajo;
                if (registro.FactorDestajo <= 0)
                {
                    nuevoRegistro.ImporteDestajo = 0;
                }
                else
                {
                    nuevoRegistro.ImporteDestajo = ((registro.CostoUnitario * registro.Cantidad) * (registro.PorcentajeDestajo / 100)) * (registro.FactorDestajo / 100);
                }
                var nuevoRegistroCreado = await _DetalleXContrato.CrearYObtener(nuevoRegistro);
                var PorcentajesAcumulados = await _PorcentajeAcumuladoContrato.ObtenerRegistros();
                var PorcentajesAcumuladosFiltrados = PorcentajesAcumulados.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitario).ToList();
                if (PorcentajesAcumuladosFiltrados.Count > 0)
                {
                    var porcentajeAcumuladoExistente = PorcentajesAcumuladosFiltrados.FirstOrDefault();
                    porcentajeAcumuladoExistente.PorcentajeDestajoAcumulado = porcentajeAcumuladoExistente.PorcentajeDestajoAcumulado + registro.PorcentajeDestajo;
                    await _PorcentajeAcumuladoContrato.Editar(porcentajeAcumuladoExistente);
                }
                else
                {
                    var porcentajeAcumuladoNuevo = new PorcentajeAcumuladoContratoDTO();
                    porcentajeAcumuladoNuevo.PorcentajeDestajoAcumulado = registro.PorcentajeDestajo;
                    porcentajeAcumuladoNuevo.IdPrecioUnitario = registro.IdPrecioUnitario;
                    var registroCreado = await _PorcentajeAcumuladoContrato.CrearYObtener(porcentajeAcumuladoNuevo);
                }
            }
            else
            {
                var registroExistenteAuxiliar = await _DetalleXContrato.ObtenXId(registro.IdDetalleXContrato);
                var nuevoRegistro = new DetalleXContratoDTO();
                nuevoRegistro.Id = registro.IdDetalleXContrato;
                nuevoRegistro.IdPrecioUnitario = registro.IdPrecioUnitario;
                nuevoRegistro.IdContrato = registro.IdContrato;
                nuevoRegistro.PorcentajeDestajo = registro.PorcentajeDestajo;
                nuevoRegistro.FactorDestajo = registro.FactorDestajo;
                if (registro.FactorDestajo <= 0)
                {
                    nuevoRegistro.ImporteDestajo = 0;
                }
                else
                {
                    nuevoRegistro.ImporteDestajo = ((registro.CostoUnitario * registro.Cantidad) * (registro.PorcentajeDestajo / 100)) * (registro.FactorDestajo / 100);
                }
                var registroEditado = await _DetalleXContrato.Editar(nuevoRegistro);
                var PorcentajesAcumulados = await _PorcentajeAcumuladoContrato.ObtenerRegistros();
                var PorcentajesAcumuladosFiltrados = PorcentajesAcumulados.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitario).ToList();
                if (PorcentajesAcumuladosFiltrados.Count > 0)
                {
                    var porcentajeAcumuladoExistente = PorcentajesAcumuladosFiltrados.FirstOrDefault();
                    porcentajeAcumuladoExistente.PorcentajeDestajoAcumulado = porcentajeAcumuladoExistente.PorcentajeDestajoAcumulado - registroExistenteAuxiliar.PorcentajeDestajo;
                    porcentajeAcumuladoExistente.PorcentajeDestajoAcumulado = porcentajeAcumuladoExistente.PorcentajeDestajoAcumulado + registro.PorcentajeDestajo;
                    await _PorcentajeAcumuladoContrato.Editar(porcentajeAcumuladoExistente);
                }
                else
                {
                    var porcentajeAcumuladoNuevo = new PorcentajeAcumuladoContratoDTO();
                    porcentajeAcumuladoNuevo.PorcentajeDestajoAcumulado = registro.PorcentajeDestajo;
                    porcentajeAcumuladoNuevo.IdPrecioUnitario = registro.IdPrecioUnitario;
                    var registroCreado = await _PorcentajeAcumuladoContrato.CrearYObtener(porcentajeAcumuladoNuevo);
                }
            }
        }

        public async Task CrearOEditarDetallePadre(DetalleXContratoParaTablaDTO registro)
        {
            var detallesEstructurados = await ObtenerDetallesDestajos(registro.IdContrato);
            bool detalleEncontrado = false;
            foreach (var detalles in detallesEstructurados)
            {
                if (registro.IdPrecioUnitario == detalles.IdPrecioUnitario)
                {
                    if (registro.AplicarPorcentajeDestajoHijos) {
                        detalles.PorcentajeDestajo = registro.PorcentajeDestajo;
                    }
                    //editar registro
                    if (registro.FactorDestajo > 0)
                    {
                        detalles.FactorDestajo = detalles.TipoPrecioUnitario == 0 ? 0 : registro.FactorDestajo;
                    }
                    await CrearOEditarDetalle(detalles);
                    detalleEncontrado = true;
                }
                else
                {
                    detalleEncontrado = false;
                }
                if (detalles.Hijos.Count() > 0) {
                    await buscandoDetalles(detalles.Hijos, registro, detalleEncontrado);
                }
            }
        }

        public async Task buscandoDetalles(List<DetalleXContratoParaTablaDTO> hijos, DetalleXContratoParaTablaDTO registro, bool detalleEncotrado)
        {
            if (detalleEncotrado) {
                foreach (var hijo in hijos)
                {
                    if (registro.AplicarPorcentajeDestajoHijos)
                    {
                        hijo.PorcentajeDestajo = registro.PorcentajeDestajo;
                    }
                    // ediatr registro
                    if (registro.FactorDestajo > 0) { 
                        hijo.FactorDestajo = hijo.TipoPrecioUnitario == 0 ? 0 : registro.FactorDestajo;
                    }
                    await CrearOEditarDetalle(hijo);
                    if (hijo.Hijos.Count() > 0) {
                        await buscandoDetalles(hijo.Hijos, registro, detalleEncotrado);
                    }
                }
            }
            else
            {
                foreach (var hijo in hijos)
                {
                    if (registro.IdPrecioUnitario == hijo.IdPrecioUnitario)
                    {
                        if (registro.AplicarPorcentajeDestajoHijos)
                        {
                            hijo.PorcentajeDestajo = registro.PorcentajeDestajo;
                        }
                        //editar registro
                        if (registro.FactorDestajo > 0)
                        {
                            hijo.FactorDestajo = hijo.TipoPrecioUnitario == 0 ? 0 : registro.FactorDestajo;
                        }
                        await CrearOEditarDetalle(hijo);
                        detalleEncotrado = true;
                    }
                    else
                    {
                        detalleEncotrado = false;
                    }
                    if (hijo.Hijos.Count() > 0) {
                        await buscandoDetalles(hijo.Hijos, registro, detalleEncotrado);
                    }
                }
            }
        }

        public async Task<ActionResult<RespuestaDTO>> finiquitarXContrato(ParametrosParaBuscarContratos parametros) {
            var respuesta = new RespuestaDTO();
            var PreciosUnitarios = _PrecioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(parametros.IdProyecto).Result;
            var Periodos = await _periodoEstimacionesService.ObtenTodosXIdProyecto(parametros.IdProyecto);
            var detallesContrato = await _DetalleXContrato.ObtenerRegistrosXIdContrato(parametros.IdContrato);

            if (parametros.TipoContrato == true)
            {
                for (int i = 0; i < PreciosUnitarios.Count; i++)
                {
                    if (PreciosUnitarios[i].TipoPrecioUnitario == 1)
                    {
                        decimal importeMO = 0;
                        var ExplosionConcepto = await _PrecioUnitarioProceso.ObtenerExplosionDeInsumoXConcepto(PreciosUnitarios[i].Id);
                        var InsumosManoDeObra = ExplosionConcepto.Where(z => z.idTipoInsumo == 10000).ToList();
                        for (int j = 0; j < InsumosManoDeObra.Count; j++)
                        {
                            importeMO = importeMO + InsumosManoDeObra[j].Importe;
                        }
                        PreciosUnitarios[i].Importe = importeMO;
                        if (PreciosUnitarios[i].Cantidad > 0)
                        {
                            PreciosUnitarios[i].CostoUnitario = PreciosUnitarios[i].Importe / PreciosUnitarios[i].Cantidad;
                        }
                    }
                }
            }
            var PorcentajesAcumulados = _PorcentajeAcumuladoContrato.ObtenerRegistros().Result;
            foreach (var detalles in detallesContrato) {
                var nuevoDet = new DetalleXContratoDTO();

                var PrecioU = PreciosUnitarios.Where(z => z.Id == detalles.IdPrecioUnitario).ToList();
                decimal PorcentajeAvanceConcepto = 0;
                foreach (var per in Periodos) {
                    var estimaciones = await _EstmacionesProceso.ObtenerEstimacionesXIdPeriodoSinEstructurar(per.Id);
                    var estimacion = estimaciones.Where(z => z.IdPrecioUnitario == detalles.IdPrecioUnitario).ToList();
                    PorcentajeAvanceConcepto += estimacion.Count() <= 0 ? 0 : estimacion[0].PorcentajeAvance * detalles.PorcentajeDestajo / 100;
                }
                if (detalles.PorcentajeDestajo > PorcentajeAvanceConcepto) {

                    nuevoDet.Id = detalles.Id;
                    nuevoDet.IdPrecioUnitario = detalles.IdPrecioUnitario;
                    nuevoDet.IdContrato = detalles.IdContrato;
                    nuevoDet.PorcentajeDestajo = PorcentajeAvanceConcepto;
                    nuevoDet.FactorDestajo = detalles.FactorDestajo;
                    decimal nuevo = detalles.PorcentajeDestajo - PorcentajeAvanceConcepto;
                    nuevoDet.ImporteDestajo = (PrecioU[0].CostoUnitario * PrecioU[0].Cantidad) * (PorcentajeAvanceConcepto / 100) * (detalles.FactorDestajo / 100);
                    var porcentajesAcumuladosFiltrados = PorcentajesAcumulados.Where(z => z.IdPrecioUnitario == detalles.IdPrecioUnitario).FirstOrDefault();
                    porcentajesAcumuladosFiltrados.PorcentajeDestajoAcumulado = porcentajesAcumuladosFiltrados.PorcentajeDestajoAcumulado - nuevo;
                    await _PorcentajeAcumuladoContrato.Editar(porcentajesAcumuladosFiltrados);

                    var editarDetalle = await _DetalleXContrato.Editar(nuevoDet);
                    if (!editarDetalle.Estatus) {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "no se edito el detalle";
                        return respuesta;
                    }
                }
            }
            var contrato = await _ContratoService.ObtenXId(parametros.IdContrato);
            contrato.Estatus = 3;
            var bloquearContrato = await _ContratoService.Editar(contrato);
            if (!bloquearContrato.Estatus) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "no se bloqueo el contrato";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se actualizo el avance";
            return respuesta;
        }

        public async Task<List<DestajistasXConceptoDTO>> ObtenerDestajistasXConcepto(DetalleXContratoParaTablaDTO parametro) {
            var DestajistasXConcepto = new List<DestajistasXConceptoDTO>();
            var detalles = await _DetalleXContrato.ObtenerRegistrosXIdPrecioUnitario(parametro.IdPrecioUnitario);
            var contratoPricipal = await _ContratoService.ObtenXId(parametro.IdContrato);
            var contratos = await _ContratoService.ObtenerRegistrosXIdProyecto(contratoPricipal.IdProyecto);
            var contratistas = await _contratistaService.ObtenTodos();
            foreach (var detalle in detalles) {
                var contrato = contratos.Where(z => z.Id == detalle.IdContrato).FirstOrDefault();
                var contratista = contratistas.Where(z => z.Id == contrato.IdContratista).FirstOrDefault();
                DestajistasXConcepto.Add(new DestajistasXConceptoDTO()
                {
                    IdPrecioUnitario = detalle.IdPrecioUnitario,
                    IdContratista = contratista.Id,
                    RazonSocial = contratista.RazonSocial,
                    IdContrato = contrato.Id,
                    NumeroDestajo = contrato.NumeroDestajo.ToString(),
                    IdDetalleXContrato = detalle.Id,
                    PorcentajeDestajo = detalle.PorcentajeDestajo,
                    PorcentajeDestajoConFormato = String.Format("{0:#,##0.00}", detalle.PorcentajeDestajo)
                });
            }
            return DestajistasXConcepto;
        }
    }
}