using EZFair.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Web.Helpers;

namespace EZFair.Pages
{
    [Authorize(Roles = "Cliente")]
    public class PerfilClienteModel : PageModel
    {
        SqlConnection connection = new SqlConnection("Server=tcp:ezfair.database.windows.net,1433;Initial Catalog=EZFair;Persist Security Info=False;User ID=ezfair;Password=LI4-muitofixe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        public string nome { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string numTelemovel { get; set; }
        public void OnGet()
        {
            var user = User.Identity;

            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT nome, email, username, numTelemovel FROM Cliente WHERE Username = @username", connection))
            {
                command.Parameters.AddWithValue("@username", user.Name);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nome = reader.GetString(0);
                        email = reader.GetString(1);
                        username = reader.GetString(2);
                        numTelemovel = reader.GetString(3);
                    }
                }
            }
        }
    }
}