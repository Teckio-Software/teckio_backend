using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO
{
    public class MovimientoBancarioSaldoProeceso<T> where T : DbContext
    {
        private readonly IMovimientoBancarioSaldoService<T> _movimientoBancarioSaldoService;
        public MovimientoBancarioSaldoProeceso(
            IMovimientoBancarioSaldoService<T> movimientoBancarioSaldoService
            ) {
            _movimientoBancarioSaldoService = movimientoBancarioSaldoService;
        }

        public async Task<RespuestaDTO> ActualizarSaldos(MovimientoBancarioSaldoDTO objeto) {
            RespuestaDTO respuesta = new RespuestaDTO();
            respuesta.Estatus = false;
            var mbSaldo = await _movimientoBancarioSaldoService.ObtenerXIdCuentaBancaria(objeto.IdCuentaBancaria);
            var existenMBS = (from saldo in mbSaldo
                             orderby saldo.Anio ascending, saldo.Mes ascending
                             select saldo).ToList();

            if (existenMBS.Count() <= 0) {
                var crearObjeto = await _movimientoBancarioSaldoService.Crear(objeto);
                if (!crearObjeto) {
                    respuesta.Descripcion = "No se creo el nuevo saldo";
                    return respuesta;
                }
            }
            else
            {
                var existeMBS = existenMBS.Where(z => z.IdCuentaBancaria == objeto.IdCuentaBancaria && z.Anio == objeto.Anio && z.Mes == objeto.Mes).ToList();
                var indice = existenMBS.FindIndex(z => z.IdCuentaBancaria == objeto.IdCuentaBancaria && z.Anio == objeto.Anio && z.Mes == objeto.Mes);
                if (existeMBS.Count() == 1 && indice == 0)
                {
                    var actualizarObjeto = await _movimientoBancarioSaldoService.ActualizarSaldoXIdCuentaBancariaYAnioYMes(objeto);
                    if (!actualizarObjeto)
                    {
                        respuesta.Descripcion = "No se actualizo el saldo";
                        return respuesta;
                    }
                    return respuesta;
                }
                if (existeMBS.Count() == 1 && indice > 0)
                {
                    var saldoAnterior = existenMBS[indice-1];
                    objeto.MontoFinal += saldoAnterior.MontoFinal;
                    var actualizarObjeto = await _movimientoBancarioSaldoService.ActualizarSaldoXIdCuentaBancariaYAnioYMes(objeto);
                    if (!actualizarObjeto)
                    {
                        respuesta.Descripcion = "No se actualizo el saldo";
                        return respuesta;
                    }
                    return respuesta;
                }
                else if(existeMBS.Count() <= 0)
                {
                    var ultimoElemento = existenMBS.Last();
                    objeto.MontoFinal += ultimoElemento.MontoFinal;
                    objeto.Id = 0;
                    var crearObjeto = await _movimientoBancarioSaldoService.Crear(objeto);
                    if (!crearObjeto)
                    {
                        respuesta.Descripcion = "No se creo el nuevo saldo";
                        return respuesta;
                    }
                    return respuesta;
                }
            }

            return respuesta;
        }
    }
}
