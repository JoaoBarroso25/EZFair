using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EZFair.Class;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EZFair.Pages
{
    public class IndexModel : PageModel
    {
        SqlConnection connection = new SqlConnection("Server=tcp:ezfair.database.windows.net,1433;Initial Catalog=EZFair;Persist Security Info=False;User ID=ezfair;Password=LI4-muitofixe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        private readonly ILogger<IndexModel> _logger;

        public static List<Feira> feiras = new List<Feira>();
        public static List<Feira> feirasTerminadas = new List<Feira>();
        public static List<Feira> feirasBrevemente = new List<Feira>();

        public int idEmpresa { get; set; }
        public string empresa { get; set; }
        public int idCategoria { get; set; }
        public string categoria { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            feiras.Clear(); feirasTerminadas.Clear(); feirasBrevemente.Clear();
            getFeiras();
        }

        public string getEmpresaNome(Feira feira)
        {
            string empresa;
            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = "SELECT nomeEmpresa FROM Empresa WHERE idEmpresa = @res";
                command.Parameters.AddWithValue("@res", feira.empresa);
                empresa = command.ExecuteScalar() as string;
            }

            connection.Close();
            return empresa;
        }

        public string getCategoriaNome(Feira feira)
        {
            string categoria;
            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = "SELECT nomeCategoria FROM Categoria WHERE idCategoria = @res";
                command.Parameters.AddWithValue("@res", feira.categoria);
                categoria = command.ExecuteScalar() as string;
            }

            connection.Close();

            return categoria;
        }

        public int getDuracao(Feira feira)
        {
            DateTime current = DateTime.Now;

            TimeSpan difference = feira.dataFinal - current;
            int numberOfDays = difference.Days;

            return numberOfDays;
        }

        private void getFeiras()
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = "SELECT empresa, nomeFeira, dataInicio, dataFim, categoria, idFeira FROM Feira";
                command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idEmpresa = reader.GetInt32(0);
                        string nomeFeira = reader.GetString(1);
                        DateTime inicio = reader.GetDateTime(2);
                        DateTime fim = reader.GetDateTime(3);
                        int idCategoria = reader.GetInt32(4);
                        int idFeira = reader.GetInt32(5);

                        string estado = setEstado(inicio, fim);
                        Feira newFeira = new Feira(idEmpresa, nomeFeira, inicio, fim, idCategoria, estado, idFeira);

                        if (estado == "Terminada")
                            feirasTerminadas.Add(newFeira);
                        else if (estado == "Brevemente")
                            feirasBrevemente.Add(newFeira);
                        else
                            feiras.Add(newFeira);

                        
                    }
                }
            }
            connection.Close();

        }

        private string setEstado(DateTime inicio, DateTime fim)
        {
            DateTime current = DateTime.Now;
            string estado = "Terminada";

            if (inicio > current)
                estado = "Brevemente";

            else if (inicio < current && current < fim)
                estado = "Decorrer";

            return estado;
        }

        [HttpPost]
        public IActionResult OnPostEscolher(string estado, int idFeira, string nomeFeira)
        {
            updateEstado(estado, idFeira);
            Console.WriteLine(estado, idFeira);
            return RedirectToPage("Feira", new { nomeFeira = nomeFeira });
        }
    
        private void updateEstado(string estado, int idFeira)
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;

                command.CommandText = "UPDATE Feira SET estado = @estado WHERE idFeira = @id;";
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@id", idFeira);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            connection.Close();
        }

    }
}