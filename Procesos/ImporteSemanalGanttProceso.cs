using ERP_TECKIO.Modelos;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;

namespace ERP_TECKIO.Procesos
{
    public class ImporteSemanalGanttProceso<T> where T : DbContext
    {
        private readonly T _dbContex;
        ProgramacionEstimadaGanttProceso<T> _programacionEstimadaGanttProceso;
        PrecioUnitarioProceso<T> _precioUnitarioProceso;
        public ImporteSemanalGanttProceso(
            T dbContex,
            ProgramacionEstimadaGanttProceso<T> programacionEstimadaGanttProceso,
            PrecioUnitarioProceso<T> precioUnitarioProceso
            ) {
            _dbContex = dbContex;
            _programacionEstimadaGanttProceso = programacionEstimadaGanttProceso;
            _precioUnitarioProceso = precioUnitarioProceso;
        }

        public async Task<List<ImporteSemanalDTO>> ImporteSemanal(int IdProyecto) { //True en EsSabado o EsDomingo = no se trabaja
            var db = _dbContex;

            var registros = await _programacionEstimadaGanttProceso.ObtenerProgramacionEstimadaXIdProyecto(IdProyecto, db);
            if (registros.Count() <= 0) {
                return new List<ImporteSemanalDTO>();
            }

            DateTime fechaInicial = registros.Min(z => z.Start);
            DateTime fechaFinal = registros.Max(z => z.End);

            var indice = (int)fechaInicial.DayOfWeek;
            DateTime startOfWeek = fechaInicial.AddDays(((int)(fechaInicial.DayOfWeek) * -1) + 1);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            CultureInfo cul = CultureInfo.CurrentCulture;

            var EndSemana = cul.Calendar.GetWeekOfYear(endOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var semanas = new List<ImporteSemanalDTO>();
            var DomingoLaboral = registros.First().EsDomingo;
            var SabadoLaboral = registros.First().EsSabado;

            registros = registros.Where(z => (z.Parent == "0" || z.Parent == null)).ToList();
            while (startOfWeek < fechaFinal)
            {
                decimal SubTotalSemana = 0;

                foreach (var z in registros) {
                    DateTime fI = z.Start;
                    DateTime fF = z.End;
                    int domingos = 0;
                    int sabados = 0;
                    var dias = z.End - z.Start;
                    if (DomingoLaboral) {
                        while (fI <= fF) {
                            if (fI.DayOfWeek == DayOfWeek.Sunday) {
                                domingos++;
                            }
                            if (fI.DayOfWeek == DayOfWeek.Saturday)
                            {
                                sabados++;
                            }
                            fI = fI.AddDays(1);
                        }
                    }

                    var costoDia = z.Importe / (dias.Days - domingos - sabados);
                    int diasSemanas = 7;
                    if (DomingoLaboral)
                    {
                        diasSemanas--;
                    }
                    if (SabadoLaboral)
                    {
                        diasSemanas--;
                    }
                    bool esInicio = (int)z.Start.DayOfWeek == 1;

                    if (esInicio) {
                        bool esFin = (int)z.End.DayOfWeek == 0;
                        if (esFin) {
                            if (startOfWeek >= z.Start && endOfWeek <= z.End)
                            {
                                var total = diasSemanas * costoDia;
                                SubTotalSemana = SubTotalSemana + total;
                                continue;
                            }
                            else {
                                continue;
                            }
                        } else {
                            var indice2 = (int)z.End.DayOfWeek;
                            var nuevaFechaFin = z.End.AddDays(7 - indice2);
                            if (startOfWeek >= z.Start && endOfWeek <= nuevaFechaFin)
                            {
                                decimal total = 0;
                                if (EndSemana == cul.Calendar.GetWeekOfYear(nuevaFechaFin, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                {
                                    if (indice2 == 6 && SabadoLaboral) {
                                        indice2 = 5;
                                    }
                                    total = indice2 * costoDia;
                                }
                                else
                                {
                                    total = diasSemanas * costoDia;
                                }
                                SubTotalSemana = SubTotalSemana + total;
                                continue;
                            }
                            else 
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        var nuevaFecha = z.Start.AddDays(((int)(z.Start.DayOfWeek) * -1) + 1);
                        bool esFin = (int)z.End.DayOfWeek == 0;
                        if (esFin)
                        {
                            if (startOfWeek >= nuevaFecha && endOfWeek <= z.End)
                            {
                                var total = diasSemanas * costoDia;
                                SubTotalSemana = SubTotalSemana + total;
                                continue;
                            }
                            else {
                                continue;
                            }
                        }
                        else
                        {
                            var indice2 = (int)z.End.DayOfWeek;
                            var nuevaFechaFin = z.End.AddDays(7 - indice2);
                            if (startOfWeek >= nuevaFecha && endOfWeek <= nuevaFechaFin)
                            {
                                decimal total = 0;
                                if (EndSemana == cul.Calendar.GetWeekOfYear(nuevaFechaFin, CalendarWeekRule.FirstDay, DayOfWeek.Monday)) {
                                    if (indice2 == 6 && SabadoLaboral)
                                    {
                                        indice2 = 5;
                                    }
                                    total = indice2 * costoDia;
                                }
                                else
                                {
                                    total = diasSemanas * costoDia;
                                }
                                SubTotalSemana = SubTotalSemana + total;
                                continue;
                            }
                            else 
                            {
                                continue;
                            }
                        }
                    }
                }

                semanas.Add(new ImporteSemanalDTO()
                {
                    NumeroSemana = EndSemana,
                    Anio = startOfWeek.Year,
                    FechaInicio = startOfWeek,
                    FechaFin = endOfWeek,
                    Total = SubTotalSemana,
                    TotalConFormato = String.Format("{0:#,##0.00}", SubTotalSemana)
                });

                startOfWeek = startOfWeek.AddDays(7);
                endOfWeek = startOfWeek.AddDays(6);
                EndSemana = cul.Calendar.GetWeekOfYear(endOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            }

            return semanas;
        }

        public async Task<ImportesSemanalesPorTipoDTO> ObtenerTotalesPorTipo(int IdProyecto)
        {
            var db = _dbContex;

            var registros = await _programacionEstimadaGanttProceso.ObtenerProgramacionEstimadaXIdProyecto(IdProyecto, db);
            if (registros.Count <= 0)
            {
                return new ImportesSemanalesPorTipoDTO();
            }
            var PreciosUnitarios = await _precioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(IdProyecto);

            DateTime fechaInicial = registros.Min(z => z.Start);
            DateTime fechaFinal = registros.Max(z => z.End);

            var indice = (int)fechaInicial.DayOfWeek;
            DateTime startOfWeek = fechaInicial.AddDays(((int)(fechaInicial.DayOfWeek) * -1) + 1);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            CultureInfo cul = CultureInfo.CurrentCulture;

            var EndSemana = cul.Calendar.GetWeekOfYear(endOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var semanasMDO = new List<ImporteSemanalDTO>();
            var semanasMaterial = new List<ImporteSemanalDTO>();
            var semanasEquipo = new List<ImporteSemanalDTO>();

            var DomingoLaboral = registros.First().EsDomingo;
            var SabadoLaboral = registros.First().EsSabado;
            var ProgramacionesExistentesEnIntervaloDeTiempo = registros.Where(z => z.TipoPrecioUnitario == 1);
            var ImportesSemanales = new ImportesSemanalesPorTipoDTO();
            while (startOfWeek < fechaFinal)
            {
                decimal TotalMDOSemana = 0;
                decimal TotalEquipoSemana = 0;
                decimal TotalMaterialSemana = 0;
                foreach (ProgramacionEstimadaGanttDTO registro in ProgramacionesExistentesEnIntervaloDeTiempo)
                {
                    decimal TotalMDO = 0;
                    decimal TotalEquipo = 0;
                    decimal TotalMaterial = 0;
                    var explosionDeInsumos = await _precioUnitarioProceso.ObtenerExplosionDeInsumoXConcepto(registro.IdPrecioUnitario);
                    foreach(InsumoParaExplosionDTO registroInsumo in explosionDeInsumos)
                    {
                        int IdPadre = 0;
                        if(registroInsumo.idTipoInsumo == 10000)
                        {
                            decimal totalAuxiliar = registroInsumo.Importe;
                            var PrecioUnitarioRegistro = PreciosUnitarios.Where(z => z.Id == registro.IdPrecioUnitario).FirstOrDefault();
                            IdPadre = PrecioUnitarioRegistro.IdPrecioUnitarioBase;
                            while(IdPadre != 0)
                            {
                                var padre = PreciosUnitarios.Where(z => z.Id == IdPadre).FirstOrDefault();
                                totalAuxiliar = totalAuxiliar * padre.Cantidad;
                                IdPadre = padre.IdPrecioUnitarioBase;
                            }
                            TotalMDO = TotalMDO + totalAuxiliar;
                        }
                        if(registroInsumo.idTipoInsumo == 10003)
                        {
                            decimal totalAuxiliar = registroInsumo.Importe;
                            var PrecioUnitarioRegistro = PreciosUnitarios.Where(z => z.Id == registro.IdPrecioUnitario).FirstOrDefault();
                            IdPadre = PrecioUnitarioRegistro.IdPrecioUnitarioBase;
                            while (IdPadre != 0)
                            {
                                var padre = PreciosUnitarios.Where(z => z.Id == IdPadre).FirstOrDefault();
                                totalAuxiliar = totalAuxiliar * padre.Cantidad;
                                IdPadre = padre.IdPrecioUnitarioBase;
                            }
                            TotalEquipo = TotalEquipo + totalAuxiliar;
                        }
                        if (registroInsumo.idTipoInsumo == 10004)
                        {
                            decimal totalAuxiliar = registroInsumo.Importe;
                            var PrecioUnitarioRegistro = PreciosUnitarios.Where(z => z.Id == registro.IdPrecioUnitario).FirstOrDefault();
                            IdPadre = PrecioUnitarioRegistro.IdPrecioUnitarioBase;
                            while (IdPadre != 0)
                            {
                                var padre = PreciosUnitarios.Where(z => z.Id == IdPadre).FirstOrDefault();
                                totalAuxiliar = totalAuxiliar * padre.Cantidad;
                                IdPadre = padre.IdPrecioUnitarioBase;
                            }
                            TotalMaterial = TotalMaterial + totalAuxiliar;
                        }
                    }
                    foreach(var z in registros)
                    {
                        DateTime fI = z.Start;
                        DateTime fF = z.End;
                        int domingos = 0;
                        int sabados = 0;
                        var dias = z.End - z.Start;
                        if (DomingoLaboral)
                        {
                            while (fI <= fF)
                            {
                                if (fI.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    domingos++;
                                }
                                if (fI.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    sabados++;
                                }
                                fI = fI.AddDays(1);
                            }
                        }
                        var dias2 = dias.Days - domingos - sabados;
                        if(dias2 <= 0) {
                            dias2 = 1;
                        }
                        var costoDiaMDO = TotalMDO / (dias2);
                        var costoDiaEquipo = TotalEquipo / (dias2);
                        var costoDiaMaterial = TotalMaterial / (dias2);
                        int diasSemanas = 7;
                        if (DomingoLaboral)
                        {
                            diasSemanas--;
                        }
                        if (SabadoLaboral)
                        {
                            diasSemanas--;
                        }
                        bool esInicio = (int)z.Start.DayOfWeek == 1;

                        if (esInicio)
                        {
                            bool esFin = (int)z.End.DayOfWeek == 0;
                            if (esFin)
                            {
                                if (startOfWeek >= z.Start && endOfWeek <= z.End)
                                {
                                    decimal totalMDO = diasSemanas * costoDiaMDO;
                                    decimal totalEquipo = diasSemanas * costoDiaEquipo;
                                    decimal totalMaterial = diasSemanas * costoDiaMaterial;
                                    TotalMDOSemana = TotalMDOSemana + totalMDO;
                                    TotalEquipoSemana = TotalEquipoSemana + totalEquipo;
                                    TotalMaterialSemana = TotalMaterialSemana + totalMaterial;
                                    continue;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                var indice2 = (int)z.End.DayOfWeek;
                                var nuevaFechaFin = z.End.AddDays(7 - indice2);
                                if (startOfWeek >= z.Start && endOfWeek <= nuevaFechaFin)
                                {
                                    decimal totalMDO = 0;
                                    decimal totalEquipo = 0;
                                    decimal totalMaterial = 0;
                                    if (EndSemana == cul.Calendar.GetWeekOfYear(nuevaFechaFin, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                    {
                                        if (indice2 == 6 && SabadoLaboral)
                                        {
                                            indice2 = 5;
                                        }
                                        totalMDO = indice2 * costoDiaMDO;
                                        totalEquipo = indice2 * costoDiaEquipo;
                                        totalMaterial = indice2 * costoDiaMaterial;
                                    }
                                    else
                                    {
                                        totalMDO = diasSemanas * costoDiaMDO;
                                        totalEquipo = diasSemanas * costoDiaEquipo;
                                        totalMaterial = diasSemanas * costoDiaMaterial;
                                    }
                                    TotalMDOSemana = TotalMDOSemana + totalMDO;
                                    TotalEquipoSemana = TotalEquipoSemana + totalEquipo;
                                    TotalMaterialSemana = TotalMaterialSemana + totalMaterial;
                                    continue;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            var nuevaFecha = z.Start.AddDays(((int)(z.Start.DayOfWeek) * -1) + 1);
                            bool esFin = (int)z.End.DayOfWeek == 0;
                            if (esFin)
                            {
                                if (startOfWeek >= nuevaFecha && endOfWeek <= z.End)
                                {
                                    decimal totalMDO = diasSemanas * costoDiaMDO;
                                    decimal totalEquipo = diasSemanas * costoDiaEquipo;
                                    decimal totalMaterial = diasSemanas * costoDiaMaterial;
                                    TotalMDOSemana = TotalMDOSemana + totalMDO;
                                    TotalEquipoSemana = TotalEquipoSemana + totalEquipo;
                                    TotalMaterialSemana = TotalMaterialSemana + totalMaterial;
                                    continue;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                var indice2 = (int)z.End.DayOfWeek;
                                var nuevaFechaFin = z.End.AddDays(7 - indice2);
                                if (startOfWeek >= nuevaFecha && endOfWeek <= nuevaFechaFin)
                                {
                                    decimal totalMDO = 0;
                                    decimal totalEquipo = 0;
                                    decimal totalMaterial = 0;
                                    if (EndSemana == cul.Calendar.GetWeekOfYear(nuevaFechaFin, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                    {
                                        if (indice2 == 6 && SabadoLaboral)
                                        {
                                            indice2 = 5;
                                        }
                                        totalMDO = indice2 * costoDiaMDO;
                                        totalEquipo = indice2 * costoDiaEquipo;
                                        totalMaterial = indice2 * costoDiaMaterial;
                                    }
                                    else
                                    {
                                        totalMDO = diasSemanas * costoDiaMDO;
                                        totalEquipo = diasSemanas * costoDiaEquipo;
                                        totalMaterial = diasSemanas * costoDiaMaterial;
                                    }
                                    TotalMDOSemana = TotalMDOSemana + totalMDO;
                                    TotalEquipoSemana = TotalEquipoSemana + totalEquipo;
                                    TotalMaterialSemana = TotalMaterialSemana + totalMaterial;
                                    continue;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                semanasMDO.Add(new ImporteSemanalDTO()
                {
                    NumeroSemana = EndSemana,
                    Anio = startOfWeek.Year,
                    FechaInicio = startOfWeek,
                    FechaFin = endOfWeek,
                    Total = TotalMDOSemana,
                    TotalConFormato = String.Format("{0:#,##0.00}", TotalMDOSemana)

                });
                semanasMaterial.Add(new ImporteSemanalDTO()
                {
                    NumeroSemana = EndSemana,
                    Anio = startOfWeek.Year,
                    FechaInicio = startOfWeek,
                    FechaFin = endOfWeek,
                    Total = TotalMaterialSemana,
                    TotalConFormato = String.Format("{0:#,##0.00}", TotalMaterialSemana)
                });
                semanasEquipo.Add(new ImporteSemanalDTO()
                {
                    NumeroSemana = EndSemana,
                    Anio = startOfWeek.Year,
                    FechaInicio = startOfWeek,
                    FechaFin = endOfWeek,
                    Total = TotalEquipoSemana,
                    TotalConFormato = String.Format("{0:#,##0.00}", TotalEquipoSemana)
                });
                startOfWeek = startOfWeek.AddDays(7);
                endOfWeek = startOfWeek.AddDays(6);
                EndSemana = cul.Calendar.GetWeekOfYear(endOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            }
            ImportesSemanales.semanasMDO = semanasMDO;
            ImportesSemanales.semanasMaterial = semanasMaterial;
            ImportesSemanales.semanasEquipo = semanasEquipo;
            return ImportesSemanales;
        }
    }
}
