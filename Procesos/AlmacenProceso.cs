
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class AlmacenProceso<T> where T: DbContext
    {
        private readonly IAlmacenService<T> _almacenService;
        private readonly IProyectoService<T> _proyectoService;


        public AlmacenProceso(IAlmacenService<T> almacenService, IProyectoService<T> proyectoService)
        {
            _almacenService = almacenService;
            _proyectoService = proyectoService;
        }

        public async Task<List<AlmacenDTO>> ObtenerConNombresProyecto()
        {
            var almacenes = await _almacenService.ObtenTodos();
            var proyectos = await _proyectoService.Lista();
            foreach(var almacen in almacenes)
            {
                if(almacen.IdProyecto == null)
                {
                    if (almacen.Central == true)
                    {
                        almacen.AlmacenNombre = "Central | " + almacen.AlmacenNombre;
                    }
                    else
                    {
                        continue;
                    }
                }
                var proyecto = proyectos.Find(p => p.Id == almacen.IdProyecto);
                if(proyecto == null)
                {
                    continue;
                }
                almacen.AlmacenNombre =  proyecto.Nombre + " | " + almacen.AlmacenNombre ;
            }
            return almacenes;
        }
    }
}
