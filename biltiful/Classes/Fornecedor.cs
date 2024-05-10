namespace biltiful.Classes
{
    internal class Fornecedor
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateOnly DataAbertura { get; set; }
        public DateOnly UltimaCompra { get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         *  Construtor para criar o objeto a partir da linha vinda do arquivo
         */
        public Fornecedor(string dados)
        {

        }

        /**
         *  Construtor para criar o objeto com as propriedades diretamente 
         */
        public Fornecedor(string CNPJ, string razaoSocial, DateOnly dataAbertura, DateOnly ultimaCompra, DateOnly dataCadastro, char situacao)
        {

        }

        /**
         *  Verifica se a data de abertura é no passado
         */
        public static bool VerificarDataAbertura(DateOnly dataAbertura)
        {
            return true;
        }

        /**
         *  Esse método verifica se o CNPJ informado é válido
         */
        public static bool VerificarCNPJ(string cnpj)
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
