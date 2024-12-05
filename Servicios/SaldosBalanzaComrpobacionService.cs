using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class SaldosBalanzaComrpobacionService<T> : ISaldosBalanzaComprobacionService<T> where T : DbContext
    {
        private readonly IGenericRepository<SaldosBalanzaComprobacion, T> _Repositorio;
        private readonly IMapper _Mapper;

        public SaldosBalanzaComrpobacionService(
            IGenericRepository<SaldosBalanzaComprobacion, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(SaldosBalanzaComprobacionDTO saldo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<SaldosBalanzaComprobacion>(saldo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Saldo de balanza creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal con la creación del saldo de la balanza";
                return respuesta;
            }
        }

        public async Task<List<SaldosBalanzaComprobacionDTO>> ObtenTodosXRangoFecha(SaldosBalanzaComproblacionXRangoFechaDTO rangoFechas)
        {
            try
            {
                var saldos = await _Repositorio.ObtenerTodos(z => (z.Anio >= rangoFechas.AnioInicio) && (z.Anio <= rangoFechas.AnioTermino));
                var saldosMap = _Mapper.Map<List<SaldosBalanzaComprobacionDTO>>(saldos);
                for (int i = 0; i < saldosMap.Count; i++)
                {
                    var diasEnMes = DateTime.DaysInMonth(saldosMap[i].Anio, saldos[i].Mes);
                    saldosMap[i].FechaFiltrado = new DateOnly(saldosMap[i].Anio, saldos[i].Mes, diasEnMes);
                }
                var diasEnMesTermino = DateTime.DaysInMonth(rangoFechas.AnioTermino, rangoFechas.MesTermino);
                var fechaTermino = new DateOnly(rangoFechas.AnioTermino, rangoFechas.MesTermino, diasEnMesTermino);
                var fechaInicio = new DateOnly(rangoFechas.AnioInicio, rangoFechas.MesInicio, 1);
                var saldosFiltrados1 = saldosMap.Where(z => (z.FechaFiltrado >= fechaInicio) && (z.FechaFiltrado <= fechaTermino)).ToList();
                //return _Mapper.Map<List<SaldosBalanzaComprobacionDTO>>(saldosFiltrados);
                return saldosFiltrados1;
                //var saldosFiltrados = saldos.Where(z => z.Anio >= rangoFechas.AnioInicio && z.Anio <= rangoFechas.AnioTermino).Where(z => z.Mes >= rangoFechas.MesInicio || z.Mes <= rangoFechas.MesTermino).ToList();
                //if (rangoFechas.AnioInicio == rangoFechas.AnioTermino)
                //{
                //    var saldosFiltrados = saldos.Where(z => (z.Anio == rangoFechas.AnioInicio && z.Mes >= rangoFechas.MesInicio && z.Mes <= rangoFechas.MesTermino)).ToList();
                //    return _Mapper.Map<List<SaldosBalanzaComprobacionDTO>>(saldosFiltrados);
                //}
                //else
                //{
                //    //var saldosFiltrados = saldos.Where(z => (z.Anio == rangoFechas.AnioInicio && z.Mes >= rangoFechas.MesInicio) || (z.Anio > rangoFechas.AnioInicio && z.Anio < rangoFechas.AnioTermino) || (z.Anio == rangoFechas.AnioTermino && z.Mes <= rangoFechas.MesTermino)).ToList();
                //    //var saldosFiltrados = saldos.Where(z => (z.Anio == rangoFechas.AnioInicio && z.Mes >= rangoFechas.MesInicio) || (z.Anio > rangoFechas.AnioInicio && z.Anio < rangoFechas.AnioTermino) || (z.Anio == rangoFechas.AnioTermino && z.Mes <= rangoFechas.MesTermino)).ToList();
                //    var diasEnMesTermino = DateTime.DaysInMonth(rangoFechas.AnioTermino, rangoFechas.MesTermino);
                //    var fechaTermino = new DateOnly(rangoFechas.AnioTermino, rangoFechas.MesTermino, diasEnMesTermino);
                //    var fechaInicio = new DateOnly(rangoFechas.AnioInicio, rangoFechas.MesInicio, 1);
                //    var saldosFiltrados1 = saldosMap.Where(z => (z.FechaFiltrado >= fechaInicio) && (z.FechaFiltrado <= fechaTermino)).ToList();
                //    //return _Mapper.Map<List<SaldosBalanzaComprobacionDTO>>(saldosFiltrados);
                //    return saldosFiltrados1;
                //}
            }
            catch
            {
                return new List<SaldosBalanzaComprobacionDTO>();
            }
        }

        public async Task<List<SaldosBalanzaComprobacionDTO>> ObtenTodosXPeriodo(SaldosBalanzaComproblacionXPeriodoDTO periodo)
        {
            try
            {
                var saldos = await _Repositorio.ObtenerTodos();
                var saldosFiltrados = saldos.Where(z => z.Anio == periodo.Anio && z.Mes == periodo.Mes);
                return _Mapper.Map<List<SaldosBalanzaComprobacionDTO>>(saldosFiltrados);
            }
            catch
            {
                return new List<SaldosBalanzaComprobacionDTO>();
            }
        }

        public async Task<List<SaldosBalanzaComprobacionDTO>> ObtenTodos()
        {
            try
            {
                var saldos = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<SaldosBalanzaComprobacionDTO>>(saldos);
            }
            catch
            {
                return new List<SaldosBalanzaComprobacionDTO>();
            }
        }

        public async Task<SaldosBalanzaComprobacionDTO> ObtenXId(int Id)
        {
            try
            {
                var saldo = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<SaldosBalanzaComprobacionDTO>(saldo);
            }
            catch
            {
                return new SaldosBalanzaComprobacionDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(SaldosBalanzaComprobacionDTO saldo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<SaldosBalanzaComprobacion>(saldo);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El saldo no existe";
                    return respuesta;
                }
                objetoEncontrado.Anio = saldo.Anio;
                objetoEncontrado.Mes = saldo.Mes;
                objetoEncontrado.SaldoInicial = saldo.SaldoInicial;
                objetoEncontrado.SaldoFinal = saldo.SaldoFinal;
                objetoEncontrado.Debe = saldo.Debe;
                objetoEncontrado.Haber = saldo.Haber;
                objetoEncontrado.EsUltimo = objetoEncontrado.EsUltimo;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar el saldo";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Saldo editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del saldo";
                return respuesta;
            }
        }

        public async Task RecorrerSaldos(SaldosBalanzaComprobacionDTO saldo)
        {
            try
            {
                var saldos = await _Repositorio.ObtenerTodos();
                var saldosFiltrados = saldos.Where(z => (z.Anio == saldo.Anio && z.IdCuentaContable == saldo.IdCuentaContable && z.Mes > saldo.Mes) || (z.Anio > saldo.Anio && z.IdCuentaContable == saldo.IdCuentaContable)).OrderBy(z => z.Mes).ThenBy(z => z.Anio).ToList();
                if(saldosFiltrados.Count > 0)
                {
                    saldosFiltrados[0].SaldoInicial = saldo.SaldoFinal;
                    saldosFiltrados[0].SaldoFinal = saldosFiltrados[0].SaldoFinal + saldosFiltrados[0].Debe - saldosFiltrados[0].Haber;
                    await _Repositorio.Editar(saldosFiltrados[0]);
                    for (int i = 1; i < saldosFiltrados.Count; i++)
                    {
                        saldosFiltrados[i].SaldoInicial = saldosFiltrados[i - 1].SaldoFinal;
                        saldosFiltrados[i].SaldoFinal = saldosFiltrados[i].SaldoFinal + saldosFiltrados[i].Debe - saldosFiltrados[i].Haber;
                        await _Repositorio.Editar(saldosFiltrados[i]);
                    }
                }
                return;
            }
            catch
            {
                return;
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
                    respuesta.Descripcion = "El saldo no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Saldo eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal al eliminar el saldo";
                return respuesta;
            }
        }

        public async Task<List<VistaBalanzaComprobacionDTO>> CrearVistaXPeriodo(List<CuentaContableDTO> cuentas, List<SaldosBalanzaComprobacionDTO> saldos, SaldosBalanzaComproblacionXPeriodoDTO periodo)
        {
            var todosLosSaldos = await ObtenTodos();
            List<VistaBalanzaComprobacionDTO> registros = new List<VistaBalanzaComprobacionDTO>();
            for (int i = 0; i < cuentas.Count; i++)
            {
                VistaBalanzaComprobacionDTO registro = new VistaBalanzaComprobacionDTO();
                registro.CuentaContableCodigo = cuentas[i].Codigo;
                registro.CuentaContableDescripcion = cuentas[i].Descripcion;
                registro.IdCuentaContable = cuentas[i].Id;
                registro.Nivel = cuentas[i].Nivel;
                registro.IdPadre = cuentas[i].IdPadre;
                registro.Expandido = true;
                var saldo = saldos.Where(z => z.IdCuentaContable == registro.IdCuentaContable);
                if (saldo.Count() <= 0)
                {
                    var saldosAnteriores = todosLosSaldos.Where(z => z.IdCuentaContable == registro.IdCuentaContable && ((z.Mes < periodo.Mes && z.Anio == periodo.Anio) || (z.Anio < periodo.Anio))).OrderBy(z =>z.Mes).ThenBy(z => z.Anio);
                    if(saldosAnteriores.Count() <= 0)
                    {
                        registro.Debe = 0;
                        registro.Haber = 0;
                        registro.SaldoInicial = 0;
                        registro.SaldoFinal = 0;
                    }
                    else
                    {
                        var ultimoSaldo = saldosAnteriores.LastOrDefault();
                        registro.Debe = 0;
                        registro.Haber = 0;
                        registro.SaldoFinal = ultimoSaldo.SaldoFinal;
                        registro.SaldoInicial = ultimoSaldo.SaldoFinal;
                    }
                }
                else
                {
                    var saldoActual = saldo.FirstOrDefault();
                    registro.Debe = saldoActual.Debe;
                    registro.Haber = saldoActual.Haber;
                    registro.SaldoFinal = saldoActual.SaldoFinal;
                    registro.SaldoInicial = saldoActual.SaldoInicial;
                }
                registros.Add(registro);
            }
            return registros;
        }

        public async Task<List<VistaBalanzaComprobacionDTO>> CrearVistaXRangoFecha(List<CuentaContableDTO> cuentas, List<SaldosBalanzaComprobacionDTO> saldos, SaldosBalanzaComproblacionXRangoFechaDTO rangoFecha)
        {
            var todosLosSaldos = await ObtenTodos();
            List<VistaBalanzaComprobacionDTO> registros = new List<VistaBalanzaComprobacionDTO>();
            for (int i = 0; i < cuentas.Count; i++)
            {
                VistaBalanzaComprobacionDTO registro = new VistaBalanzaComprobacionDTO();
                registro.CuentaContableCodigo = cuentas[i].Codigo;
                registro.CuentaContableDescripcion = cuentas[i].Descripcion;
                registro.IdCuentaContable = cuentas[i].Id;
                registro.Nivel = cuentas[i].Nivel;
                registro.IdPadre = cuentas[i].IdPadre;
                registro.Expandido = true;
                var saldosXCuentaContable = saldos.Where(z => z.IdCuentaContable == registro.IdCuentaContable).ToList();
                if (saldosXCuentaContable.Count <= 0)
                {
                    var saldosAnteriores = todosLosSaldos.Where(z => z.IdCuentaContable == registro.IdCuentaContable && ((z.Mes < rangoFecha.MesInicio && z.Anio == rangoFecha.AnioInicio) || (z.Anio < rangoFecha.AnioInicio))).OrderBy(z => z.Mes).ThenBy(z => z.Anio);
                    if (saldosAnteriores.Count() <= 0)
                    {
                        registro.Debe = 0;
                        registro.Haber = 0;
                        registro.SaldoInicial = 0;
                        registro.SaldoFinal = 0;
                    }
                    else
                    {
                        var ultimoSaldo = saldosAnteriores.LastOrDefault();
                        registro.Debe = 0;
                        registro.Haber = 0;
                        registro.SaldoFinal = ultimoSaldo.SaldoFinal;
                        registro.SaldoInicial = ultimoSaldo.SaldoFinal;
                    }
                }
                else
                {
                    decimal haber = 0;
                    decimal debe = 0;
                    for (int j = 0; j < saldosXCuentaContable.Count(); j++)
                    {
                        haber = haber + saldosXCuentaContable[j].Haber;
                        debe = debe + saldosXCuentaContable[j].Debe;
                    }
                    var primerSaldo = saldosXCuentaContable.FirstOrDefault();
                    var ultimoSaldo = saldosXCuentaContable.LastOrDefault();
                    registro.Haber = haber;
                    registro.Debe = debe;
                    registro.SaldoInicial = primerSaldo.SaldoInicial;
                    registro.SaldoFinal = ultimoSaldo.SaldoFinal;
                }
                registros.Add(registro);
            }
            return registros;
        }

        public async Task<List<VistaBalanzaComprobacionDTO>> EstructurarRegistros(List<VistaBalanzaComprobacionDTO> balanzaDeComprobacionSinOrdenar)
        {
            var padres = balanzaDeComprobacionSinOrdenar.Where(z => z.IdPadre == 0).ToList();
            for (int i = 0; i < padres.Count(); i++)
            {
                padres[i].hijos = ObtenHijos(balanzaDeComprobacionSinOrdenar, padres[i]).Result;
                if (padres[i].hijos.Count > 0)
                {
                    decimal saldoInicial = 0;
                    decimal saldoFinal = 0;
                    decimal debe = 0;
                    decimal haber = 0;
                    for (int j = 0; j < padres[i].hijos.Count; j++)
                    {
                        saldoInicial = saldoInicial + padres[i].hijos[j].SaldoInicial;
                        saldoFinal = saldoFinal + padres[i].hijos[j].SaldoFinal;
                        debe = debe + padres[i].hijos[j].Debe;
                        haber = haber + padres[i].hijos[j].Haber;
                    }
                    padres[i].SaldoInicial = saldoInicial;
                    padres[i].SaldoFinal = saldoFinal;
                    padres[i].Debe = debe;
                    padres[i].Haber = haber;
                    padres[i].Expandido = false;
                }
            }
            return padres;
        }

        public async Task<List<VistaBalanzaComprobacionDTO>> ObtenHijos(List<VistaBalanzaComprobacionDTO> balanzaDeComprobacionSinOrdenar, VistaBalanzaComprobacionDTO padre)
        {
            try
            {
                var saldosHijos = balanzaDeComprobacionSinOrdenar.Where(z => z.IdPadre == padre.IdCuentaContable).ToList();
                if(saldosHijos.Count <= 0)
                {
                    return new List<VistaBalanzaComprobacionDTO>();
                }
                for(int i = 0; i < saldosHijos.Count; i++)
                {
                    var hijos = await ObtenHijos(balanzaDeComprobacionSinOrdenar, saldosHijos[i]);
                    decimal saldoInicial = 0;
                    decimal saldoFinal = 0;
                    decimal debe = 0;
                    decimal haber = 0;
                    if(hijos.Count > 0)
                    {
                        for (int j = 0; j < hijos.Count; j++)
                        {
                            saldoInicial = saldoInicial + hijos[j].SaldoInicial;
                            saldoFinal = saldoFinal + hijos[j].SaldoFinal;
                            debe = debe + hijos[j].Debe;
                            haber = haber + hijos[j].Haber;
                        }
                        saldosHijos[i].SaldoInicial = saldoInicial;
                        saldosHijos[i].SaldoFinal = saldoFinal;
                        saldosHijos[i].Debe = debe;
                        saldosHijos[i].Haber = haber;
                    }
                    saldosHijos[i].hijos = hijos;
                }
                return saldosHijos;
            }
            catch
            {
                return new List<VistaBalanzaComprobacionDTO>();
            }
        }
    }
}
