namespace biltiful.Classes
{
    internal class Produto : IEntidade
    {
        public string CodigoBarras {  get; set; }
        public string Nome { get; set; }
        public float ValorVenda { get; set; }
        public DateOnly UltimaVenda { get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         * Construtor vazio para permitir a população do objeto posteriormente com a linha vinda do arquivo
         */
        public Produto()
        {

        }

        /**
         *  Construtor para criar o objeto a partir da linha vinda do arquivo
         */
        public Produto(string dados)
        {
            LinhaParaObjeto(dados);
        }

        /**
         *  Construtor para criar o objeto com as propriedades diretamente 
         */
        public Produto(string codigoBarras, string nome, float valorVenda)
        {
            if (!VerificarCodigoDeBarras(codigoBarras))
                throw new Exception("Código de barras não segue o padrão EAN-13");

            if (!VerificarValorDeVenda(valorVenda))
                throw new Exception("Valor de venda deve ser menor ou igual à R$999,99");

            CodigoBarras = codigoBarras;
            Nome = nome;
            ValorVenda = valorVenda;
        }

        /**
         *  Esse método verifica se o valor de venda cumpre seus requerimentos
         */
        static bool VerificarValorDeVenda(float valorVenda)
        {
            return valorVenda < 1000;
        }

        /**
         *  Esse método verifica se o código de barras é válido de acordo com o padrão EAN-13
         */
        public static bool VerificarCodigoDeBarras(string codigoDeBarras)
        {
            if (codigoDeBarras.Length != 13)
                return false;

            if (codigoDeBarras.Substring(0, 3) != "789")
                return false;

            foreach (char caracter in codigoDeBarras)
                if (!char.IsDigit(caracter))
                    return false;

            int somaPares = 0;
            int somaImpares = 0;

            for (int i = 0; i < 12; i++)
            {
                int digito = int.Parse(codigoDeBarras[i].ToString());

                if (i % 2 == 0)
                    somaPares += digito;
                else
                    somaImpares += digito * 3;
            }

            int total = somaPares + somaImpares;
            int digitoVerificador = (10 - (total % 10)) % 10;

            return digitoVerificador == int.Parse(codigoDeBarras[12].ToString());
        }

        /**
         *  Formata o tipo de data (não pertence aqui)
         */
        public string FormatarData(DateOnly data)
        {
            return $"{data.Day.ToString("00")}{data.Month.ToString("00")}{data.Year.ToString("0000")}";
        }

        /**
         *  Esse método transforma o objeto atual em uma linha que pode ser escrita
         */
        public string FormatarParaArquivo()
        {
            return $"{CodigoBarras}{Nome.PadRight(Constantes.TAMANHO_NOME_PRODUTO)}{(ValorVenda * 100).ToString("00000")}{FormatarData(UltimaVenda)}{FormatarData(DataCadastro)}{Situacao}"; ;
        }

        public void LinhaParaObjeto(string linha)
        {
            if (linha.Length != 55)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade Produto");

            CodigoBarras = linha.Substring(0, 13);
            Nome = linha.Substring(13, Constantes.TAMANHO_NOME_PRODUTO).Trim();
            ValorVenda = float.Parse(linha.Substring(33, 5)) / 100;

            UltimaVenda = DateOnly.ParseExact(linha.Substring(38, 8), "ddMMyyyy", null);

            DataCadastro = DataCadastro = DateOnly.ParseExact(linha.Substring(46, 8), "ddMMyyyy", null);

            Situacao = linha.Substring(54, 1).First();
        }
    }
}
