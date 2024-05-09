namespace biltiful.Classes
{
    internal class Produto
    {
        public string CodigoBarras {  get; set; }
        public string Nome { get; set; }
        public double ValorVenda { get; set; }
        public DateOnly UltimaVenda { get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         *  Construtor para criar o objeto a partir da linha vinda do arquivo
         */
        public Produto(string dados)
        {

        }

        /**
         *  Construtor para criar o objeto com as propriedades diretamente 
         */
        public Produto(string codigoBarras, string nome, int valorVenda, DateOnly ultimaVenda, DateOnly dataCadastro)
        {

        }

        /**
         *  Esse método verifica se o código de barras é válido de acordo com o padrão EAN-13
         */
        static bool VerificarCodigoBarras(string codigoDeBarras)
        {
            return true;
        }

        /**
         *  Esse método transforma o objeto atual em uma linha que pode ser escrita
         */
        public string FormatarParaArquivo()
        {
            return string.Empty;
        }
    }
}
