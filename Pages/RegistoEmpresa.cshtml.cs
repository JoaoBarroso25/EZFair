using EZFair.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace EZFair.Pages
{
    public class RegistoEmpresaModel : PageModel
    {
        SqlConnection connection = new SqlConnection("Server=tcp:ezfair.database.windows.net,1433;Initial Catalog=EZFair;Persist Security Info=False;User ID=ezfair;Password=LI4-muitofixe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        [BindProperty] 
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string NIPC { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int id = LastID();
            id++;

            if (UsernameEmUso(Name) == 0)
            {
                Empresa newEmpresa = new Empresa(id, Name, Email, Password, NIPC);

                await RegistarEmpresa(newEmpresa);

                return RedirectToPage("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Empresa já existe.";

                return null;
            }
        }

        private async Task RegistarEmpresa(Empresa empresa)
        {
            // Open the connection
            connection.Open();

            using (SqlCommand command = new SqlCommand("INSERT INTO Empresa (idEmpresa, nomeEmpresa, email, password, nipc) VALUES (@idEmpresa, @nomeEmpresa, @email, @password, @nipc)", connection))
            {
                // Add the parameters and their values
                command.Parameters.AddWithValue("@idEmpresa", empresa.idEmpresa);
                command.Parameters.AddWithValue("@nomeEmpresa", empresa.nomeEmpresa);
                command.Parameters.AddWithValue("@email", empresa.email);
                command.Parameters.AddWithValue("@password", empresa.password);
                command.Parameters.AddWithValue("@nipc", empresa.nipc);

                // Open the connection and execute the query
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        private int LastID()
        {
            // Open the connection
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT MAX(idEmpresa) AS LastID FROM Empresa;", connection))
            {
                int lastId = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return lastId;
            }
        }

        private int UsernameEmUso(string username)
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT CASE WHEN EXISTS (SELECT * FROM Empresa WHERE nomeEmpresa = @nomeEmpresa) THEN 1 ELSE 0 END;", connection))
            {
                command.Parameters.AddWithValue("@nomeEmpresa", Name);

                int response = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return response;
            }
        }
    }
}
