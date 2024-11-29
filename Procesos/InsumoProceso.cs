using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO
{
    public class InsumoProceso<TContext> where TContext : DbContext
    {
        private readonly IInsumoService<TContext> _Service;
        private readonly IFamiliaInsumoService<TContext> _FamiliaInsumoService;
        private readonly ITipoInsumoService<TContext> _TipoInsumoService;
        private readonly IRelacionFSRInsumoService<TContext> _RelacionFSRInsumoService;
        private readonly IFactorSalarioRealService<TContext> _FactorSalarioRealService;
        public InsumoProceso(
            IInsumoService<TContext> service
            , IFamiliaInsumoService<TContext> familiaService
            , ITipoInsumoService<TContext> tipoInsumoService
            , IRelacionFSRInsumoService<TContext> relacionFSRInsumoService
            , IFactorSalarioRealService<TContext> factorSalarioRealService)
        {
            _Service = service;
            _FamiliaInsumoService = familiaService;
            _TipoInsumoService = tipoInsumoService;
            _RelacionFSRInsumoService = relacionFSRInsumoService;
            _FactorSalarioRealService = factorSalarioRealService;
        }

        public async Task Post([FromBody] InsumoCreacionDTO parametro)
        {
            try
            {
                var resultado = await _Service.CrearYObtener(parametro);
                var tipoInsumo = await _TipoInsumoService.ObtenXId(parametro.idTipoInsumo);
                if(tipoInsumo.Descripcion == "Mano de obra")
                {
                    var FSR = await _FactorSalarioRealService.ObtenXId(parametro.IdProyecto);
                    RelacionFSRInsumoDTO nuevaRelacion = new RelacionFSRInsumoDTO();
                    nuevaRelacion.IdTipoInsumo = tipoInsumo.Id;
                    nuevaRelacion.IdProyecto = parametro.IdProyecto;
                    nuevaRelacion.SueldoBase = parametro.CostoUnitario;
                    nuevaRelacion.Prestaciones = ((FSR.PorcentajeFsr - 1) * nuevaRelacion.SueldoBase);
                    resultado.CostoUnitario = nuevaRelacion.SueldoBase + nuevaRelacion.Prestaciones;
                    await _Service.Editar(resultado);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task Put([FromBody] InsumoDTO parametros)
        {
            try
            {
                var tipoInsumo = await _TipoInsumoService.ObtenXId(parametros.idTipoInsumo);
                if (tipoInsumo.Descripcion == "Mano de obra")
                {
                    var FSR = await _FactorSalarioRealService.ObtenXId(parametros.IdProyecto);
                    RelacionFSRInsumoDTO nuevaRelacion = new RelacionFSRInsumoDTO();
                    nuevaRelacion.IdTipoInsumo = tipoInsumo.Id;
                    nuevaRelacion.IdProyecto = parametros.IdProyecto;
                    nuevaRelacion.SueldoBase = parametros.CostoUnitario;
                    nuevaRelacion.Prestaciones = ((FSR.PorcentajeFsr - 1) * nuevaRelacion.SueldoBase);
                    parametros.CostoUnitario = nuevaRelacion.SueldoBase + nuevaRelacion.Prestaciones;
                    var resultado = await _Service.Editar(parametros);
                }
                else
                {
                    var resultado = await _Service.Editar(parametros);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }


        public async Task<ActionResult<List<InsumoDTO>>> Get(int IdProyecto)
        {
            List<InsumoDTO> listaInsumos = new List<InsumoDTO>();
            var insumos = await _Service.ObtenXIdProyecto(IdProyecto);
            var insumosfiltrados = insumos.Where(z => z.idTipoInsumo != 10000 && z.idTipoInsumo != 10001 && z.idTipoInsumo != 3).ToList();
            var familias = await _FamiliaInsumoService.Lista();
            var tiposInsumo = await _TipoInsumoService.Lista();
            foreach (var insumoF in insumosfiltrados)
            {
                var familia = familias.Where(z => z.Id == insumoF.idFamiliaInsumo).ToList();
                var tipo = tiposInsumo.Where(z => z.Id == insumoF.idTipoInsumo).ToList();
                if (familia.Count() <= 0) {
                    insumoF.DescripcionFamiliaInsumo = "";
                }
                else
                {
                    insumoF.DescripcionFamiliaInsumo = familia[0].Descripcion;
                }
                insumoF.DescripcionTipoInsumo = tipo[0].Descripcion;
            }
            listaInsumos = insumosfiltrados;
            return listaInsumos;
        }

        public async Task Delete(int Id)
        {
            try
            {
                var resultado = await _Service.Eliminar(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return;
        }

        public async Task<ActionResult<List<InsumoDTO>>> ObtenerInsumosParaAutoComplete(int IdProyecto)
        {
            var query = _Service.ObtenXIdProyecto(IdProyecto).Result.AsQueryable();
            var lista = query.OrderBy(z => z.id).ToList();
            var listaFiltrada = lista.Where(z => z.idTipoInsumo != 10001 || z.CostoUnitario == 0).ToList();
            if (listaFiltrada.Count <= 0)
            {
                return new List<InsumoDTO>();
            }
            var familias = await _FamiliaInsumoService.Lista();
            var tiposInsumo = await _TipoInsumoService.Lista();
            for (int i = 0; i < listaFiltrada.Count; i++)
            {
                var familia = familias.Where(z => z.Id == listaFiltrada[i].idFamiliaInsumo).FirstOrDefault();
                var tipo = tiposInsumo.Where(z => z.Id == listaFiltrada[i].idTipoInsumo).FirstOrDefault();
                if (familia != null)
                {
                    listaFiltrada[i].DescripcionFamiliaInsumo = familia.Descripcion;
                }
                listaFiltrada[i].DescripcionTipoInsumo = tipo.Descripcion;
            }
            return listaFiltrada;
        }

        public async Task ImportarInsumosAOtroProyecto(int idProyectoOriginal, int idProyectoAMigrar)
        {
            var insumos = await _Service.ObtenXIdProyecto(idProyectoOriginal);
            foreach(InsumoDTO insumo in insumos)
            {
                var nuevoInsumoCreado = new InsumoCreacionDTO();
                nuevoInsumoCreado.Codigo = insumo.Codigo;
                nuevoInsumoCreado.Descripcion = insumo.Descripcion;
                nuevoInsumoCreado.Unidad = insumo.Unidad;
                nuevoInsumoCreado.idTipoInsumo = insumo.idTipoInsumo;
                nuevoInsumoCreado.idFamiliaInsumo = insumo.idFamiliaInsumo;
                nuevoInsumoCreado.CostoUnitario = insumo.CostoUnitario;
                nuevoInsumoCreado.IdProyecto = idProyectoAMigrar;
                insumo.IdProyecto = idProyectoAMigrar;
                await _Service.Crear(nuevoInsumoCreado);
            }
        }
    }
}
