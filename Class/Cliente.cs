using Azure.Identity;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace EZFair.Class
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string nome { get; set; }
        public string email { get; set;}
        public string username { get; set;}
        public string password { get; set;}
        public string numTelemovel { get; set;}

        public Cliente(int idCliente, string nome, string email, string username, string password, string numTelemovel)
        {
            this.idCliente = idCliente;
            this.nome = nome;
            this.email = email;
            this.username = username;
            this.password = password;
            this.numTelemovel = numTelemovel;
        }
    }

    
}
