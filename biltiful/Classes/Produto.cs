﻿namespace biltiful.Classes
{
    internal class Produto
    {
        public string CodigoBarras {  get; set; }
        public string Nome { get; set; }
        public float ValorVenda { get; set; }
        public DateOnly UltimaVenda { get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         *  Construtor para criar o objeto a partir da linha vinda do arquivo
         */
        public Produto(string dados)
        {
            if (dados.Length != 55)
                throw new Exception("Linha não possui o tamanho padrão para a entidade Produto");

            CodigoBarras = dados.Substring(0, 13);
            Nome = dados.Substring(13, 20).Trim();
            ValorVenda = float.Parse(dados.Substring(33, 5)) / 100;

            UltimaVenda = DateOnly.ParseExact(dados.Substring(38, 8), "ddMMyyyy", null);

            DataCadastro = DataCadastro = DateOnly.ParseExact(dados.Substring(46, 8), "ddMMyyyy", null);

            Situacao = dados.Substring(54, 1).First();
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
        public string FormatarData(DateOnly data)
        {
            return $"{data.Day.ToString("00")}{data.Month.ToString("00")}{data.Year.ToString("0000")}";
        }

        /**
         *  Esse método transforma o objeto atual em uma linha que pode ser escrita
         */
        public string FormatarParaArquivo()
        {
            return $"{CodigoBarras}{Nome.PadRight(20)}{ValorVenda * 100}{FormatarData(UltimaVenda)}{FormatarData(DataCadastro)}{Situacao}"; ;
        }
    }
}
