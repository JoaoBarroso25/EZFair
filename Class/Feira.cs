using Microsoft.AspNetCore.Server.IIS.Core;

namespace EZFair.Class
{
    public class Feira
    {
        public int idFeira { get; set; }
        public int empresa { get; set; }
        public int categoria { get; set; }
        public string nomeFeira { get; set; }
        public int numParticipantes { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataFinal { get; set; }
        public string descricao { get; set; }
        public string estado { get; set; }

        public Feira(int idFeira, int empresa, int categoria, string nomeFeira, int numParticipantes, DateTime dataInicio, DateTime dataFinal, string descricao)
        {
            this.idFeira = idFeira;
            this.empresa = empresa;
            this.categoria = categoria;
            this.nomeFeira = nomeFeira;
            this.numParticipantes = numParticipantes;
            this.dataInicio = dataInicio;
            this.dataFinal = dataFinal;
            this.descricao = descricao;
        }

        public Feira(int empresa, string nomeFeira, DateTime inicio, DateTime final, int categoria, string estado, int idFeira)
        {
            this.nomeFeira = nomeFeira;
            this.empresa = empresa;
            this.dataInicio = inicio;
            this.dataFinal = final;
            this.categoria = categoria;
            this.estado = estado;
            this.idFeira = idFeira;
        }

        public Feira(int idEmpresa, string nomeFeira, DateTime inicio, DateTime fim)
        {
            this.empresa = idEmpresa;
            this.nomeFeira = nomeFeira;
            this.empresa = empresa;
            this.dataInicio = inicio;
            this.dataFinal = fim;
        }

        public Feira(int idEmpresa, string nomeFeira, DateTime inicio, DateTime final, string estado, string descricao)
        {
            this.empresa = idEmpresa;
            this.nomeFeira = nomeFeira;
            this.dataInicio = inicio;
            this.dataFinal = final;
            this.estado = estado;
            this.descricao = descricao;
        }
    }
}
