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
            if (dados.Length != 89)
                throw new Exception("Linha não possui o tamanho padrão para a entidade Fornecedor");

            CNPJ = dados.Substring(0, 14);
            RazaoSocial = dados.Substring(14, 50).Trim();

            int dia = int.Parse(dados.Substring(64, 2));
            int mes = int.Parse(dados.Substring(66, 2));
            int ano = int.Parse(dados.Substring(68, 4));

            DataAbertura = new DateOnly(ano, mes, dia);

            dia = int.Parse(dados.Substring(72, 2));
            mes = int.Parse(dados.Substring(74, 2));
            ano = int.Parse(dados.Substring(76, 4));

            UltimaCompra = new DateOnly(ano, mes, dia);

            dia = int.Parse(dados.Substring(80, 2));
            mes = int.Parse(dados.Substring(82, 2));
            ano = int.Parse(dados.Substring(84, 4));

            DataCadastro = new DateOnly(ano, mes, dia);

            Situacao = dados.Substring(88, 1).First();
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
