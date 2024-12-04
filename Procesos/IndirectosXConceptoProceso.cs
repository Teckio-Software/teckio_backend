using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class IndirectosXConceptoProceso<T> where T : DbContext
    {
        private readonly IIndirectosXConceptoService<T> _indirectosXConceptoService;
        private readonly PrecioUnitarioProceso<T> _precioUnitarioProceso;

        public IndirectosXConceptoProceso(
            IIndirectosXConceptoService<T> indirectosXConceptoService,
            PrecioUnitarioProceso<T> precioUnitarioProceso
            ) {
            _indirectosXConceptoService = indirectosXConceptoService;
            _precioUnitarioProceso = precioUnitarioProceso;
        }

        public async Task<RespuestaDTO> CrearIndirectosPadre(int IdConcepto) {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = false;
            respuesta.Descripcion = "Ocurrio un error al crear";

            var indirectos = new IndirectosXConceptoDTO();
            indirectos.IdConcepto = IdConcepto;
            indirectos.IdIndirectoBase = 0;
            string Codigo = "00";
            for (int i = 0; i < 3; i++)
            {
                indirectos.TipoIndirecto = i;
                indirectos.Codigo = Codigo + (i + 1).ToString();
                indirectos.Porcentaje = 0;
                switch (i)
                {
                    case 0:
                        indirectos.Descripcion = "Indirecto";
                        break;
                    case 1:
                        indirectos.Descripcion = "Financiamiento";
                        break;
                    case 2:
                        indirectos.Descripcion = "Utilidad";
                        break;
                }

                var crearIndirecto = await _indirectosXConceptoService.CrearYObtener(indirectos);
                if (crearIndirecto.Id <= 0)
                {
                    respuesta.Descripcion = "Error al crear los indirectos";
                    return respuesta;
                }
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Creacion correcta";
            return respuesta;
        }

        public async Task<List<IndirectosXConceptoDTO>> ObtenerIndirectos(int IdConcepto)
        {
            var lista = new List<IndirectosXConceptoDTO>();

            var indirectos = await _indirectosXConceptoService.ObtenerXIdConcepto(IdConcepto);

            indirectos = await ObtenerIndirectosConEstructura(indirectos);

            return indirectos;
        }

        public async Task<List<IndirectosXConceptoDTO>> ObtenerIndirectosConEstructura(List<IndirectosXConceptoDTO> indirectos)
        {
            var indirectosPadre = indirectos.Where(z => z.IdIndirectoBase == 0).ToList();

            foreach (var indirecto in indirectosPadre)
            {
                indirecto.Hijos = await ObtenerHijos(indirectos, indirecto);
                indirecto.Expandido = true;
            }
            return indirectosPadre;
        }

        public async Task<List<IndirectosXConceptoDTO>> ObtenerHijos(List<IndirectosXConceptoDTO> indirectos, IndirectosXConceptoDTO padre)
        {
            var indirectosPadre = indirectos.Where(z => z.IdIndirectoBase == padre.Id).ToList();

            foreach (var indirecto in indirectosPadre)
            {
                indirecto.Hijos = await ObtenerHijos(indirectos, indirecto);
                indirecto.Expandido = true;
            }
            return indirectosPadre;
        }

        public async Task<RespuestaDTO> CrearIndirecto(IndirectosXConceptoDTO indirecto)
        {
            var respuesta = await _indirectosXConceptoService.Crear(indirecto);
            if (respuesta.Estatus)
            {
                await Recalcular(indirecto);
            }
            return respuesta;
        }

        public async Task<RespuestaDTO> EditarIndirecto(IndirectosXConceptoDTO indirecto)
        {
            var respuesta = await _indirectosXConceptoService.Editar(indirecto);
            if (respuesta.Estatus)
            {
                await Recalcular(indirecto);
            }
            return respuesta;
        }

        public async Task<RespuestaDTO> EliminarIndirecto(int idIndirecto)
        {
            var respuesta = new RespuestaDTO();
            var indirecto = await _indirectosXConceptoService.ObtenerXId(idIndirecto);
            if (indirecto.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El indirecto no existe";
                return respuesta;
            }

            var indirectos = await _indirectosXConceptoService.ObtenerXIdConcepto(indirecto.Id);
            var existenHijos = indirectos.Where(z => z.IdIndirectoBase == indirecto.Id);
            if (existenHijos.Count() > 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El indirecto contiene subniveles";
                return respuesta;
            }
            var resultado = await _indirectosXConceptoService.Eliminar(indirecto);
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

        public async Task<bool> Recalcular(IndirectosXConceptoDTO indirecto)
        {
            var indirectos = await _indirectosXConceptoService.ObtenerXIdConcepto(indirecto.IdConcepto);

            while (indirecto.IdIndirectoBase != 0)
            {
                var hermanos = indirectos.Where(z => z.IdIndirectoBase == indirecto.IdIndirectoBase);
                var indirectoPadre = indirectos.Where(z => z.Id == indirecto.IdIndirectoBase).FirstOrDefault();
                indirectoPadre.Porcentaje = hermanos.Sum(z => z.Porcentaje);
                var indirectoEditar = await _indirectosXConceptoService.Editar(indirectoPadre);
                var actualiza = indirectos.FindIndex(z => z.Id == indirectoPadre.Id);
                indirectos[actualiza] = indirectoPadre;
                indirecto = indirectoPadre;
            }
            decimal porcentajeTotal = indirectos.Where(z => z.IdIndirectoBase == 0).Sum(z => z.Porcentaje) / 100 + 1;
            return await _precioUnitarioProceso.EditarIndirectoXConcepto(indirecto.IdConcepto, porcentajeTotal);
        }
    }
}
