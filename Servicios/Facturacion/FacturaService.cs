using AutoMapper;
using AutoMapper.Configuration.Annotations;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FacturaService<T> : IFacturaService<T> where T : DbContext
    {
        private readonly IGenericRepository<Factura, T> _repository;
        private readonly IMapper _mapper;
        private readonly T db;
        public FacturaService(
            IGenericRepository<Factura, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Cancelar(FacturaDTO parametro)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.Id == parametro.Id);
            if (objetoEncontrado.Id <= 0) {
                return false;
            }
            objetoEncontrado.Estatus = 2;
            var editar = await _repository.Editar(objetoEncontrado);
            if (!editar) {
                return false;
            }
            return true;
        }

        public async Task<bool> Crear(FacturaDTO parametro)
        {
            try
            {
                var resultado = await _repository.Crear(_mapper.Map<Factura>(parametro));
                return resultado.Id > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<FacturaDTO> CrearYObtener(FacturaDTO parametro)
        {
            parametro.EstatusEnviadoCentroCostos = false;
            var objetoCreado = await _repository.Crear(_mapper.Map<Factura>(parametro));
            if (objetoCreado.Id == 0)
                return new FacturaDTO();
            return _mapper.Map<FacturaDTO>(objetoCreado);
        }

        public async Task<bool> Editar(FacturaDTO parametro)
        {
            try
            {
                var objetoEncontrado = await _repository.Obtener(z => z.Id == parametro.Id);
                if(objetoEncontrado.Id <= 0)
                    return false;
                objetoEncontrado.Uuid = parametro.Uuid;
                objetoEncontrado.FechaValidacion = parametro.FechaValidacion;
                objetoEncontrado.FechaTimbrado = parametro.FechaTimbrado;
                objetoEncontrado.FechaEmision = parametro.FechaEmision;
                objetoEncontrado.RfcEmisor = parametro.RfcEmisor;
                objetoEncontrado.Subtotal = parametro.Subtotal;
                objetoEncontrado.Total = parametro.Total;
                objetoEncontrado.SerieCfdi = parametro.SerieCfdi;
                objetoEncontrado.FolioCfdi = parametro.FolioCfdi;
                objetoEncontrado.Estatus = parametro.Estatus;
                objetoEncontrado.Tipo = parametro.Tipo;
                objetoEncontrado.Modalidad = parametro.Modalidad;
                objetoEncontrado.IdArchivo = parametro.IdArchivo;
                objetoEncontrado.MetodoPago = parametro.MetodoPago;
                objetoEncontrado.Descuento = parametro.Descuento;
                objetoEncontrado.IdArchivoPdf = parametro.IdArchivoPdf;
                objetoEncontrado.EstatusEnviadoCentroCostos = parametro.EstatusEnviadoCentroCostos;
                objetoEncontrado.VersionFactura = parametro.VersionFactura;
                objetoEncontrado.CodigoPostal = parametro.CodigoPostal;
                objetoEncontrado.TipoCambio = parametro.TipoCambio;
                objetoEncontrado.IdCliente = parametro.IdCliente;
                objetoEncontrado.IdFormaPago = parametro.IdFormaPago;
                objetoEncontrado.IdRegimenFiscalSat = parametro.IdRegimenFiscalSat;
                objetoEncontrado.IdUsoCfdi = parametro.IdUsoCfdi;
                objetoEncontrado.IdMonedaSat = parametro.IdMonedaSat;
                var resultado = await _repository.Editar(objetoEncontrado);
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EsEnviado(int IdFactura)
        {
            try
            {
                var objetoEncontrado = await _repository.Obtener(z => z.Id == IdFactura);
                if (objetoEncontrado.Id <= 0)
                    return false;
                
                var resultado = await _repository.Eliminar(objetoEncontrado);
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public Task<List<FacturaDTO>> ObtenSoloValidas()
        {
            //Donde el estatus es 2
            throw new NotImplementedException();
        }

        public async Task<List<FacturaDTO>> ObtenTodos()
        {
            var lista = await _repository.ObtenerTodos();
            return _mapper.Map<List<FacturaDTO>>(lista);
        }

        public Task<List<FacturaDTO>> ObtenTodosT()
        {
            throw new NotImplementedException();
        }

        public async Task<FacturaDTO> ObtenXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            return _mapper.Map<FacturaDTO>(objeto);
        }

        public Task<FacturaDTO> ObtenXIdT(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FacturaDTO>> ObtenXRfcEmisor(string rfcEmisor)
        {
            var lista = await _repository.ObtenerTodos(f => f.RfcEmisor == rfcEmisor);
            return _mapper.Map<List<FacturaDTO>>(lista);
        }

        public async Task<List<FacturaDTO>> ObtenXRfcReceptor(string rfcReceptor)
        {
            var items = db.Database.SqlQueryRaw<FacturaDTO>(""""
              SELECT f.[Id]
              ,f.[UUID]
              ,f.[FechaValidacion]
              ,f.[FechaTimbrado]
              ,f.[FechaEmision]
              ,f.[RfcEmisor]
              ,f.[Subtotal]
              ,f.[Total]
              ,f.[SerieCfdi]
              ,f.[FolioCfdi]
              ,f.[Estatus]
              ,f.[Tipo]
              ,f.[Modalidad]
              ,f.[IdArchivo]
              ,f.[MetodoPago]
              ,f.[Descuento]
              ,f.[IdArchivoPdf]
              ,f.[EstatusEnviadoCentroCostos]
              ,f.[VersionFactura]
              ,f.[CodigoPostal]
              ,f.[TipoCambio]
              ,f.[IdCliente]
              ,f.[IdFormaPago]
              ,f.[IdRegimenFiscalSat]
              ,f.[IdUsoCfdi]
              ,f.[IdMonedaSat]
              FROM dbo.Clientes c join Factura.Factura f on f.[IdCliente] = c.Id where c.Rfc = '
              """" + rfcReceptor+""""';"""").ToList();
            if (items.Count <= 0)
            {
                return new List<FacturaDTO>();
            }
            
            return items;
        }

        public async Task<List<FacturaDTO>> ObtenXUuid(string uuid)
        {
            var lsita = await _repository.ObtenerTodos(z => z.Uuid == uuid);
            return _mapper.Map<List<FacturaDTO>>(lsita);
        }
    }
}
