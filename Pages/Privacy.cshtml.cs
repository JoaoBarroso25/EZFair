using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace EZFair.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public static string TestQuery()
        {
            using (SqlConnection connection = new SqlConnection("Server=tcp:ezfair.database.windows.net,1433;Initial Catalog=EZFair;Persist Security Info=False;User ID=ezfair;Password=LI4-muitofixe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the query
                SqlCommand command = new SqlCommand("INSERT INTO Cliente (idCliente, nome, email, username, password, numTelemovel)\r\nVALUES (2, 'John', 'john@example.com', 'john', '1234', 'test')\r\n", connection);

                command.ExecuteNonQuery();

                using (SqlCommand idk = new SqlCommand("SELECT * FROM Cliente", connection))
                {
                    using (SqlDataReader reader = idk.ExecuteReader()) 
                    {

                        reader.Read();
                        
                        return reader.GetString(3);
                    }
                }
            }
        }
    }
}