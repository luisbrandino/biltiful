namespace biltiful.Classes
{
    internal class MPrima
    {
        public string Id { get; private set; }
        public string Nome { get; set; }
        public DateOnly UltimaCompra {  get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         *  Construtor para criar o objeto a partir da linha vinda do arquivo
         */
        public MPrima(string dados)
        {
            
        }

        /**
         *  Construtor para criar o objeto com as propriedades diretamente 
         */
        public MPrima(string id, string nome, DateOnly ultimaCompra, DateOnly dataCadastro, char situacao)
        {

        }

        /**
         *  Verifica se o campo id cumpre os requisitos esperados
         */
        static bool VerificarId(string id)
        {
            return true;
        }

        /**
         * Esse método transforma o objeto atual em uma linha que pode ser escrita
         */
        public string FormatarParaArquivo()
        {
            return string.Empty;
        }
    }
}
