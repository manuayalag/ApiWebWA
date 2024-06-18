//Recibir mensajes con Api Oficial de WhatsApp en C#
namespace WebApiWA.Models
{
    using System.Data.SqlClient;
    public class Datos
    {
        public void insertar(string mensaje_recibido, string id_wa, string telefono_wa)
        {
            var connection = new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;");
            try
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO aspwa.dbo.registro` (`mensaje_recibido`, `id_wa`, `telefono_wa`) VALUES ('" + mensaje_recibido + "', '" + id_wa + "', '" + telefono_wa + "');";
                connection.Open();
                command.ExecuteNonQuery();
                //return "Mitarbeiter wurde angelegt";
            }
            catch (Exception ex)
            {
                //return ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }

    }

    public class WebHookResponseModel
    {
        public Entry[] entry { get; set; }
    }

    public class Entry
    {
        public Change[] changes { get; set; }
    }

    public class Change
    {
        public Value value { get; set; }
    }

    public class Value
    {
        public int ad_id { get; set; }
        public long form_id { get; set; }
        public long leadgen_id { get; set; }
        public int created_time { get; set; }
        public long page_id { get; set; }
        public int adgroup_id { get; set; }
        public Messages[] messages { get; set; }
    }
    public class Messages
    {
        public string id { get; set; }
        public string from { get; set; }
        public Text text { get; set; }
    }
    public class Text
    {
        public string body { get; set; }
    }
}