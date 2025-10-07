using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class IndirectosProceso<T> where T : DbContext
    {
        private readonly IConjuntoIndirectosService<T> _conjuntoIndirectosService;
        private readonly IIndirectosService<T> _indirectosService;
        public IndirectosProceso(
            IConjuntoIndirectosService<T> conjuntoIndirectosService,
            IIndirectosService<T> indirectosService
            ) {
            _conjuntoIndirectosService = conjuntoIndirectosService;
            _indirectosService = indirectosService;
        }

        public async Task<ConjuntoIndirectosDTO> ObtenerConjuntoIndirecto(int IdProyecto) {
            var conjunto = await _conjuntoIndirectosService.ObtenerXIdProyecto(IdProyecto);
            conjunto.PorcentajeConFormato = String.Format("{0:#,##0.0000}", conjunto.Porcentaje);
            return conjunto;
        }

        public async Task<RespuestaDTO> CrearConjuntoIndirecto(int IdProyecto) {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = false;
            respuesta.Descripcion = "Ocurrio un error al crear";

            var conjuntoIndiracto = new ConjuntoIndirectosDTO();
            conjuntoIndiracto.IdProyecto = IdProyecto;
            conjuntoIndiracto.TipoCalculo = 0;
            conjuntoIndiracto.Porcentaje = 1;

            var crearConjuntoIndirecto = await _conjuntoIndirectosService.CrearYObtener(conjuntoIndiracto);
            if (crearConjuntoIndirecto.Id <= 0) {
                return respuesta;
            }
            var indirectos = new IndirectosDTO();
            indirectos.IdConjuntoIndirectos = crearConjuntoIndirecto.Id;
            indirectos.IdIndirectoBase = 0;
            string Codigo = "00";
            for (int i = 0; i < 4; i++) {
                indirectos.TipoIndirecto = i;
                indirectos.Codigo = Codigo + (i + 1).ToString();
                indirectos.Porcentaje = 0;
                switch (i) {
                    case 0:
                        indirectos.Descripcion = "Indirecto";
                        break;
                    case 1:
                        indirectos.Descripcion = "Financiamiento";
                        break;
                    case 2:
                        indirectos.Descripcion = "Utilidad";
                        break;
                    case 3:
                        indirectos.Descripcion = "Cargos Adicionales";
                        break;
                }

                var crearIndirecto = await _indirectosService.CrearYObtener(indirectos);
                if (crearIndirecto.Id <= 0) {
                    respuesta.Descripcion = "Error al crear los indirectos";
                    return respuesta;
                }
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Creacion correcta";
            return respuesta;
        }

        public async Task<List<IndirectosDTO>> ObtenerIndirectos(int IdProyecto) {
            var lista = new List<IndirectosDTO>();

            var conjunto = await _conjuntoIndirectosService.ObtenerXIdProyecto(IdProyecto);

            var indirectos = await _indirectosService.ObtenerXIdConjunto(conjunto.Id);

            indirectos = await ObtenerIndirectosConEstructura(indirectos);
            foreach(var indirecto in indirectos)
            {
                indirecto.PorcentajeConFormato = String.Format("{0:#,##0.00}%", indirecto.Porcentaje);
            }

            return indirectos;
        }

        public async Task<List<IndirectosDTO>> ObtenerIndirectosConEstructura(List<IndirectosDTO> indirectos) {
            var indirectosPadre = indirectos.Where(z => z.IdIndirectoBase == 0).ToList();

            foreach (var indirecto in indirectosPadre)
            {
                indirecto.Nivel = 1;
                indirecto.Hijos = await ObtenerHijos(indirectos, indirecto);
                indirecto.Expandido = true;
            }
            return indirectosPadre;
        }

        public async Task<List<IndirectosDTO>> ObtenerHijos(List<IndirectosDTO> indirectos, IndirectosDTO padre) {
            var indirectosPadre = indirectos.Where(z => z.IdIndirectoBase == padre.Id).ToList();

            foreach (var indirecto in indirectosPadre)
            {
                indirecto.Nivel = padre.Nivel + 1;
                indirecto.Hijos = await ObtenerHijos(indirectos, indirecto);
                indirecto.Expandido = true;
            }
            return indirectosPadre;
        }

        public async Task<RespuestaDTO> CrearIndirecto(IndirectosDTO indirecto) {
            var respuesta = await _indirectosService.Crear(indirecto);
            if (respuesta.Estatus) {
                await Recalcular(indirecto);
            }
            return respuesta;
        }

        public async Task<RespuestaDTO> EditarIndirecto(IndirectosDTO indirecto)
        {
            var respuesta = await _indirectosService.Editar(indirecto);
            if (respuesta.Estatus)
            {
                await Recalcular(indirecto);
            }
            return respuesta;
        }

        public async Task<RespuestaDTO> EliminarIndirecto(int idIndirecto)
        {
            var respuesta = new RespuestaDTO();
            var indirecto = await _indirectosService.ObtenerXId(idIndirecto);
            if (indirecto.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El indirecto no existe";
                return respuesta;
            }
            var conjunto = await _conjuntoIndirectosService.ObtenerXId(indirecto.IdConjuntoIndirectos);

            var indirectos = await _indirectosService.ObtenerXIdConjunto(conjunto.Id);
            var existenHijos = indirectos.Where(z => z.IdIndirectoBase == indirecto.Id);
            if (existenHijos.Count() > 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El indirecto contiene subniveles";
                return respuesta;
            }
            var resultado = await _indirectosService.Eliminar(indirecto);
            if (resultado.Estatus)
            {
                await Recalcular(indirecto);
            }
            else
            {
                respuesta = resultado;
                return respuesta;
            }
            respuesta = resultado;
            return respuesta;
        }

        public async Task<bool> Recalcular(IndirectosDTO indirecto) {
            var conjunto = await _conjuntoIndirectosService.ObtenerXId(indirecto.IdConjuntoIndirectos);

            var indirectos = await _indirectosService.ObtenerXIdConjunto(conjunto.Id);

            while (indirecto.IdIndirectoBase != 0) {
                var hermanos = indirectos.Where(z => z.IdIndirectoBase == indirecto.IdIndirectoBase);
                var indirectoPadre = indirectos.Where(z => z.Id == indirecto.IdIndirectoBase).FirstOrDefault();
                indirectoPadre.Porcentaje = hermanos.Sum(z => z.Porcentaje);
                var indirectoEditar = await _indirectosService.Editar(indirectoPadre);
                var actualiza = indirectos.FindIndex(z => z.Id == indirectoPadre.Id);
                indirectos[actualiza] = indirectoPadre;
                indirecto = indirectoPadre;
            }
            await RecalcularConjunto(conjunto);
            return true;
        }

        public async Task<RespuestaDTO> RecalcularConjunto(ConjuntoIndirectosDTO conjunto) {
            var indirectos = await _indirectosService.ObtenerXIdConjunto(conjunto.Id);

            if (conjunto.TipoCalculo == 0)
            {
                conjunto.Porcentaje = indirectos.Where(z => z.IdIndirectoBase == 0).Sum(z => z.Porcentaje) / 100 + 1;
            }
            else
            {
                indirectos = indirectos.Where(z => z.IdIndirectoBase == 0).ToList();
                decimal porcentaje = 1;
                foreach (var indi in indirectos)
                {
                    if (indi.Porcentaje != 0)
                    {
                        decimal actual = indi.Porcentaje / 100 + 1;
                        porcentaje *= actual;
                    }
                }
                conjunto.Porcentaje = porcentaje;
            }
            var editarConjunto = await _conjuntoIndirectosService.Editar(conjunto);

            return editarConjunto;
        }
    }
}
