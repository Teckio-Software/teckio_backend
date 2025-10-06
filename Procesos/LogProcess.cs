using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace ERP_TECKIO.Procesos
{
    public enum NivelesLog
    {
        Debug,
        Info,
        Warn,
        Error,
        Critical,
    }
    public class LogProcess
    {
        private IConfiguration _configuration;

        public LogProcess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task RegistrarLog(NivelesLog Nivel, string Metodo, string Descripcion, string DbContext, int IdUsuario, int IdEmpresa)
        {
            try
            {
                //string connectionString = "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;";
                string? connectionString = _configuration["ssoConection"];
                if(connectionString == null)
                {
                    return;
                }

                string query = $"INSERT INTO dbo.Logs(Nivel,Metodo,Descripcion,DbContext,IdUsuario,IdEmpresa) VALUES('{Nivel.ToString()}','{Metodo}','{Descripcion}','{DbContext}',{IdUsuario},{IdEmpresa});";

                // Crear conexión SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir conexión
                    connection.Open();

                    // Crear comando SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                    //connection.Close();
                }
            }
            catch
            {
                
                return;
            }
        } 

        
    }
}
