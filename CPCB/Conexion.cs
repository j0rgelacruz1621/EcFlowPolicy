using MySql.Data.MySqlClient; // Asegúrate de instalar MySql.Data vía NuGet

namespace CPCB
{
    public class Conexion
    {
        // Ajusta los valores de Server, Uid y Pwd según tu configuración local
        private static string cadena = "Server=localhost;Database=flow_policy;Uid=root;Pwd=Bonita123.;";

        public static MySqlConnection LeerConexion()
        {
            return new MySqlConnection(cadena);
        }
    }
}