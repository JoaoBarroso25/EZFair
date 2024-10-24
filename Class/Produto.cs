namespace EZFair.Class
{
    public class Produto
    {
        public int idProduto { get; set; }
        public int stock { get; set; }
        public float preco { get; set; }
        public string nomeProduto { get; set; }

        public Produto(int stock, float preco, string nomeProduto)
        {
            this.stock = stock;
            this.preco = preco;
            this.nomeProduto = nomeProduto;
        }

        public Produto(int idProduto, int stock, float preco, string nomeProduto)
        {
            this.idProduto = idProduto;
            this.stock = stock;
            this.preco = preco;
            this.nomeProduto = nomeProduto;
        }
    }
}
