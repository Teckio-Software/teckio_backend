
namespace ERP_TECKIO
{
    public class DependenciaProgramacionEstimadaDTO
    {
        public int Id { get; set; }
        public int IdProgramacionEstimadaGantt { get; set; }
        public string SourceId { get; set; }
        public int IdProyecto { get; set; }
        public string SourceTarget { get; set; }
        public string OwnTarget { get; set; }
    }

    public class DependenciaProgramacionEstimadaDeserealizadaDTO
    {
        public int Id { get; set; }
        public int IdProgramacionEstimadaGantt { get; set; }
        public int IdProgramacionEstimadaGanttPredecesora { get; set; }
        public int IdProyecto { get; set; }
        public string SourceTarget { get; set; }
        public string OwnTarget { get; set; }
    }
}
