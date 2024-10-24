using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace EZFair.Pages
{
    public class LoginEmpresaModel : PageModel
    {
        SqlConnection connection = new SqlConnection("Server=tcp:ezfair.database.windows.net,1433;Initial Catalog=EZFair;Persist Security Info=False;User ID=ezfair;Password=LI4-muitofixe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            bool isValid = ValidarCredenciais(Username, Password);

            if (isValid)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Username));
                identity.AddClaim(new Claim(ClaimTypes.Name, Username));
                identity.AddClaim(new Claim(ClaimTypes.Role, "Empresa"));

                // Set the forms authentication ticket
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                // Redirect to the protected resource or the home page
                return Redirect("/");
            }
            else
            {
                // Show an error message
                ModelState.AddModelError("", "Invalid username or password");
                return Page();
            }
        }

        private bool ValidarCredenciais(string Username, string Password)
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Empresa WHERE nomeEmpresa = @username AND password = @password", connection))
            {
                command.Parameters.AddWithValue("@username", Username);
                command.Parameters.AddWithValue("@password", Password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // The user was found in the database
                        return true;
                    }
                    else
                    {
                        // The user was not found in the database
                        return false;
                    }
                }
            }
        }
    }
}
