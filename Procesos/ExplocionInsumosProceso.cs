using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ExplocionInsumosProceso<T> where T : DbContext
    {
        private readonly PrecioUnitarioXEmpleadoProceso<T> _precioUnitarioXEmpleadoProceso;
        private readonly PrecioUnitarioProceso<T> _precioUnitarioProceso;
        public ExplocionInsumosProceso(
            PrecioUnitarioXEmpleadoProceso<T> precioUnitarioXEmpleadoProceso,
            PrecioUnitarioProceso<T> precioUnitarioProceso

            )
        {
            _precioUnitarioXEmpleadoProceso = precioUnitarioXEmpleadoProceso;
            _precioUnitarioProceso = precioUnitarioProceso;
        }


        public async Task<List<InsumoParaExplosionDTO>> obtenerExplosionXEmpleado(int IdProyecto, int IdEmpleado)
        {
            var ExplosionDeInsumos = new List<InsumoParaExplosionDTO>();
            var ExplosionDeInsumosSinRepetir = new List<InsumoParaExplosionDTO>();
            var Registros = await _precioUnitarioXEmpleadoProceso.ObtenerXIdEmpleado(IdEmpleado);
            Registros = Registros.Where(z => z.IdProyceto == IdProyecto).ToList();
            for (int i = 0; i < Registros.Count; i++)
            {
                var ExplosionConcepto = await _precioUnitarioProceso.ObtenerExplosionDeInsumoXConcepto(Registros[i].IdPrecioUnitario);
                for (int j = 0; j < ExplosionConcepto.Count; j++)
                {
                    ExplosionDeInsumos.Add(ExplosionConcepto[j]);
                }
            }
            for (int i = 0; i < ExplosionDeInsumos.Count; i++)
            {
                var aux = ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).ToList();
                if (aux.Count > 0)
                {
                    ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().Cantidad = (ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().Cantidad + ExplosionDeInsumos[i].Cantidad);
                    ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().Importe = (ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().Cantidad * ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().CostoUnitario);
                }
                else
                {
                    ExplosionDeInsumosSinRepetir.Add(ExplosionDeInsumos[i]);
                }
            }
            return ExplosionDeInsumosSinRepetir;
        }

    }
}
