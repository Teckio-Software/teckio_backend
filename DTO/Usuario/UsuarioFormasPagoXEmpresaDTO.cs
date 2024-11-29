
namespace ERP_TECKIO
{
    public class UsuarioFormasPagoXEmpresaDTO
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdEmpresa { get; set; }

        public bool EsPue { get; set; }

        public bool EsPpd { get; set; }
    }
}
