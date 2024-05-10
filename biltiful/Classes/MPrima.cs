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
            if (dados.Length != 43)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade MPrima");

            Id = dados.Substring(0, 6);
            Nome = dados.Substring(6, 20);
            UltimaCompra = DateOnly.ParseExact(dados.Substring(26, 8), "ddMMyyyy");
            DataCadastro = DateOnly.ParseExact(dados.Substring(34, 8), "ddMMyyyy");
            Situacao = dados.Substring(42, 1).First();
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
