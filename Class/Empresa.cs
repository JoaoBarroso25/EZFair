namespace EZFair.Class
{
    public class Empresa
    {
        public int idEmpresa { get; set; }
        public String nomeEmpresa { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String nipc { get; set; }

        public Empresa (int idEmpresa, String nomeEmpresa, String email, String password, String nipc) 
        {
            this.idEmpresa = idEmpresa;
            this.nomeEmpresa = nomeEmpresa;
            this.email = email;
            this.password = password;
            this.nipc = nipc;   
        }
    }
}
