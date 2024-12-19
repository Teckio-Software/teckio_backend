using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ERP_TECKIO.Procesos
{
    public class ImporteSemanalGanttProceso<T> where T : DbContext
    {
        private readonly T _dbContex;
        ProgramacionEstimadaGanttProceso<T> _programacionEstimadaGanttProceso;
        public ImporteSemanalGanttProceso(
            T dbContex,
            ProgramacionEstimadaGanttProceso<T> programacionEstimadaGanttProceso
            ) {
            _dbContex = dbContex;
            _programacionEstimadaGanttProceso = programacionEstimadaGanttProceso;
        }

        public async Task<List<ImporteSemanalDTO>> ImporteSemanal(int IdProyecto) {
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

            var UltimaSemana = cul.Calendar.GetWeekOfYear(fechaFinal, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var StartSemana = cul.Calendar.GetWeekOfYear(startOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var EndSemana = cul.Calendar.GetWeekOfYear(endOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var semanas = new List<ImporteSemanalDTO>();

            registros = registros.Where(z => (z.Parent == "0" || z.Parent == null)).ToList();
            while (startOfWeek < fechaFinal)
            {
                decimal SubTotalSemana = 0;

                foreach (var z in registros) {
                    var dias = z.End - z.Start;
                    var costoDia = z.Importe / dias.Days;

                    bool esInicio =(int) z.Start.DayOfWeek == 1;

                    if (esInicio) {
                        bool esFin = (int) z.End.DayOfWeek == 0;
                        if (esFin) {
                            if (startOfWeek >= z.Start && endOfWeek <= z.End)
                            {
                                var difecincia = endOfWeek - startOfWeek;
                                var total = difecincia.Days * costoDia;
                                SubTotalSemana = SubTotalSemana + total;
                                continue;
                            }
                            else {
                                continue;
                            }
                        }else {
                            var indice2 = (int)z.End.DayOfWeek;
                            var nuevaFechaFin = z.End.AddDays(7-indice2);
                            if (startOfWeek >= z.Start && endOfWeek <= nuevaFechaFin)
                            {
                                decimal total = 0;
                                if (EndSemana == cul.Calendar.GetWeekOfYear(nuevaFechaFin, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                {
                                    total = indice2-1 * costoDia;
                                }
                                else
                                {
                                    var difecincia = endOfWeek - startOfWeek;
                                    total = difecincia.Days * costoDia;
                                }
                                SubTotalSemana = SubTotalSemana + total;
                                continue;
                            }
                            else {
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
                                var difecincia = endOfWeek - startOfWeek;
                                var total = difecincia.Days * costoDia;
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
                                    total = indice2-1 * costoDia;
                                }
                                else
                                {
                                    var difecincia = endOfWeek - startOfWeek;
                                    total = difecincia.Days * costoDia;
                                }
                                SubTotalSemana = SubTotalSemana + total;
                                continue;
                            }
                            else {
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
                    Total = SubTotalSemana
                });

                startOfWeek = startOfWeek.AddDays(7);
                endOfWeek = startOfWeek.AddDays(6);
                EndSemana = cul.Calendar.GetWeekOfYear(endOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                
            }

            return semanas;
        }
    }
}
