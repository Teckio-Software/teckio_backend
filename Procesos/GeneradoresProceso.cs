using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class GeneradoresProceso<TContext> where TContext : DbContext
    {
        private readonly IGeneradoresService<TContext> _Service;
        private readonly IPrecioUnitarioService<TContext> _PrecioUnitarioService;
        private readonly IConceptoService<TContext> _ConceptoService;
        private readonly PrecioUnitarioProceso<TContext> _PrecioUnitarioProceso;
        public GeneradoresProceso(
             IGeneradoresService<TContext> service
            , IPrecioUnitarioService<TContext> precioUnitarioService
            , IConceptoService<TContext> conceptoService
            , PrecioUnitarioProceso<TContext> precioUnitarioProceso)
        {
            _Service = service;
            _PrecioUnitarioService = precioUnitarioService;
            _PrecioUnitarioProceso = precioUnitarioProceso;
            _ConceptoService = conceptoService;
        }

        public async Task<ActionResult<GeneradoresDTO>> Post( GeneradoresDTO parametro)
        {
            try
            {
                var Generador = await _Service.CrearYObtener(parametro);
                var precioUnitario = await _PrecioUnitarioService.ObtenXId(parametro.IdPrecioUnitario);
                var Generadores = await _Service.ObtenerTodosXIdPrecioUnitario(parametro.IdPrecioUnitario);
                decimal total = 0;
                for (int i = 0; i < Generadores.Count; i++)
                {
                    total = total + Generadores[i].X * Generadores[i].Y * Generadores[i].Z * Generadores[i].Cantidad;
                }
                precioUnitario.Cantidad = total;
                var concepto = await _ConceptoService.ObtenXId(precioUnitario.IdConcepto);
                precioUnitario.Codigo = concepto.Codigo;
                precioUnitario.Descripcion = concepto.Descripcion;
                precioUnitario.Unidad = concepto.Unidad;
                precioUnitario.CostoUnitario = concepto.CostoUnitario;
                await _PrecioUnitarioProceso.Editar(precioUnitario);
                return Generador;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new GeneradoresDTO();
            }
        }

        public async Task<ActionResult<GeneradoresDTO>> Put(GeneradoresDTO parametros)
        {
            try
            {
                var resultado = await _Service.Editar(parametros);
                var generador = await _Service.ObtenXId(parametros.Id);
                var precioUnitario = await _PrecioUnitarioService.ObtenXId(parametros.IdPrecioUnitario);
                var Generadores = await _Service.ObtenerTodosXIdPrecioUnitario(parametros.IdPrecioUnitario);
                decimal total = 0;
                for (int i = 0; i < Generadores.Count; i++)
                {
                    total = total + Generadores[i].X * Generadores[i].Y * Generadores[i].Z * Generadores[i].Cantidad;
                }
                precioUnitario.Cantidad = total;
                var concepto = await _ConceptoService.ObtenXId(precioUnitario.IdConcepto);
                precioUnitario.Codigo = concepto.Codigo;
                precioUnitario.Descripcion = concepto.Descripcion;
                precioUnitario.Unidad = concepto.Unidad;
                precioUnitario.CostoUnitario = concepto.CostoUnitario;
                await _PrecioUnitarioProceso.Editar(precioUnitario);
                return generador;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new GeneradoresDTO();
            }
        }

        public async Task<ActionResult<List<GeneradoresDTO>>> Get(int IdPrecioUnitario)
        {
            var query = _Service.ObtenerTodosXIdPrecioUnitario(IdPrecioUnitario).Result.AsQueryable();
            var lista = query.OrderBy(z => z.Id).ToList();
            if (lista.Count <= 0)
            {
                return new List<GeneradoresDTO>();
            }
            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].CantidadTotal = lista[i].Cantidad * lista[i].X * lista[i].Y * lista[i].Z;
            }
            return lista;
        }

        public async Task Delete(int Id)
        {
            try
            {
                var Generador = await _Service.ObtenXId(Id);
                if(Generador.Id != 0)
                {
                    var resultado = await _Service.Eliminar(Id);
                    var precioUnitario = await _PrecioUnitarioService.ObtenXId(Generador.IdPrecioUnitario);
                    var Generadores = await _Service.ObtenerTodosXIdPrecioUnitario(Generador.IdPrecioUnitario);
                    decimal total = 0;
                    for (int i = 0; i < Generadores.Count; i++)
                    {
                        total = total + Generadores[i].X * Generadores[i].Y * Generadores[i].Z * Generadores[i].Cantidad;
                    }
                    precioUnitario.Cantidad = total;
                    var concepto = await _ConceptoService.ObtenXId(precioUnitario.IdConcepto);
                    precioUnitario.Codigo = concepto.Codigo;
                    precioUnitario.Descripcion = concepto.Descripcion;
                    precioUnitario.Unidad = concepto.Unidad;
                    precioUnitario.CostoUnitario = concepto.CostoUnitario;
                    await _PrecioUnitarioProceso.Editar(precioUnitario);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
        }
    }
}
