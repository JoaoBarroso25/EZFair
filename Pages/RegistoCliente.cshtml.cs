using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EZFair.Class;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace EZFair.Pages
{
    public class RegistoCliente: PageModel
    {
        SqlConnection connection = new SqlConnection("Server=tcp:ezfair.database.windows.net,1433;Initial Catalog=EZFair;Persist Security Info=False;User ID=ezfair;Password=LI4-muitofixe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int id = LastID();
            id++;

            if (UsernameEmUso(Username) == 0)
            {
                Cliente newCliente = new Cliente(id, Name, Email, Username, Password, PhoneNumber);

                await RegistarCliente(newCliente);

                return RedirectToPage("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Username já está em uso";

                return null;
            }
        }

        private async Task RegistarCliente(Cliente cliente)
        {
             // Open the connection
             connection.Open();

             using (SqlCommand command = new SqlCommand("INSERT INTO Cliente (idCliente, nome, email, username, password, numTelemovel, autorizacao) VALUES (@idCliente, @nome, @email, @username, @password, @numTelemovel, 1)", connection))
             {
                // Add the parameters and their values
                command.Parameters.AddWithValue("@idCliente", cliente.idCliente);
                command.Parameters.AddWithValue("@nome", cliente.nome);
                command.Parameters.AddWithValue("@email", cliente.email);
                command.Parameters.AddWithValue("@username", cliente.username);
                command.Parameters.AddWithValue("@password", cliente.password);
                command.Parameters.AddWithValue("@numTelemovel", cliente.numTelemovel);

                // Open the connection and execute the query
                command.ExecuteNonQuery();
             }

             connection.Close();
        }

        private int LastID() 
        {
            // Open the connection
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT MAX(idCliente) AS LastID FROM Cliente;", connection))
            {
                int lastId = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return lastId;
            }      
        }

        private int UsernameEmUso(string username)
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT CASE WHEN EXISTS (SELECT * FROM Cliente WHERE username = @username) THEN 1 ELSE 0 END;", connection))
            {
                command.Parameters.AddWithValue("@username", username);
                
                int response = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return response;
            }
        }

        
    }
}
