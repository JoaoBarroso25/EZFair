using EZFair.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EZFair.Pages
{
    [Authorize(Roles = "Empresa")]
    public class PerfilEmpresaModel : PageModel
    {
        SqlConnection connection = new SqlConnection("Server=tcp:ezfair.database.windows.net,1433;Initial Catalog=EZFair;Persist Security Info=False;User ID=ezfair;Password=LI4-muitofixe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        public string nome { get; set; }
        public string email { get; set; }
        public static int id { get; set; }

        public static List<Feira> feiras = new List<Feira>();
        public void OnGet()
        {
            feiras.Clear();

            var user = User.Identity;

            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;

                // First query
                command.CommandText = "SELECT nomeEmpresa, email, idEmpresa FROM Empresa WHERE nomeEmpresa = @username";
                command.Parameters.AddWithValue("@username", user.Name);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nome = reader.GetString(0);
                        email = reader.GetString(1);
                        id = reader.GetInt32(2);
                    }
                }

                // Second query
                command.CommandText = "SELECT empresa, nomeFeira, dataInicio, dataFim, numParticipantes FROM Feira WHERE empresa = @idEmpresa";
                command.Parameters.AddWithValue("@idEmpresa", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idEmpresa = reader.GetInt32(0);
                        string nomeFeira = reader.GetString(1);
                        DateTime inicio = reader.GetDateTime(2);
                        DateTime fim = reader.GetDateTime(3);

                        Feira newFeira = new Feira(idEmpresa, nomeFeira, inicio, fim);
                        feiras.Add(newFeira);
                    }
                }
            }

            connection.Close();
        }

        [HttpPost]
        public IActionResult OnPostCriarFeira()
        {
            return RedirectToPage("CriarFeira", new { idEmpresa = id });
        }
    }
}
