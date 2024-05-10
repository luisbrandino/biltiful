using System.Text.RegularExpressions;

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
            if (cnpj.Length != 14)
                return false;

            foreach (char caracter in cnpj)
                if (!char.IsDigit(caracter))
                    return false;

            int[] multiplicadoresPrimeiroDigito = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadoresSegundoDigito = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicadoresPrimeiroDigito[i];

            int resto = soma % 11;
            int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            tempCnpj += primeiroDigitoVerificador;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicadoresSegundoDigito[i];

            resto = soma % 11;
            int segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(primeiroDigitoVerificador.ToString() + segundoDigitoVerificador.ToString());
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
            return $"{CNPJ}{RazaoSocial.PadRight(50)}{FormatarData(DataAbertura)}{FormatarData(UltimaCompra)}{FormatarData(DataCadastro)}{Situacao}";
        }

    }
}
