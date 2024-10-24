namespace EZFair.Class
{
    public class Anuncio
    {
        public int idAnuncio { get; set; }
        public string descricao { get; set; }
        public int produto { get; set; }
        public int feira{ get; set; }

        public Anuncio(int produto, int feira)
        {
            this.produto = produto;
            this.feira = feira;
        }
    }
}
