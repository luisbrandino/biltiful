namespace biltiful.Entidades
{
    internal class Cliente
    {
        public string CPF { get; private set; }
        public string Nome { get; set; }
        public DateOnly DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateOnly UltimaCompra {  get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         *  Esse construtor serve para carregar um objeto Cliente com a linha vinda do arquivo referente
         */
        public Cliente(string dados)
        {
            if (dados.Length != 87)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade Cliente");

            CPF = dados.Substring(0, 10);
            Nome = dados.Substring(11, 50);

            int dia = int.Parse(dados.Substring(61, 2));
            int mes = int.Parse(dados.Substring(63, 2));
            int ano = int.Parse(dados.Substring(65, 4));

            DataNascimento = new DateOnly(ano, mes, dia);

            Sexo = dados.Substring(69, 1).First();

            dia = int.Parse(dados.Substring(70, 2));
            mes = int.Parse(dados.Substring(72, 2));
            ano = int.Parse(dados.Substring(74, 4));

            UltimaCompra = new DateOnly(ano, mes, dia);

            dia = int.Parse(dados.Substring(78, 2));
            mes = int.Parse(dados.Substring(80, 2));
            ano = int.Parse(dados.Substring(82, 4));

            DataCadastro = new DateOnly(ano, mes, dia);

            Situacao = dados.Substring(86, 1).First();
        }

        /**
         *  Esse construtor cria o objeto com suas informações sendo passadas diretamente
         */
        public Cliente(string cpf, string nome, char sexo)
        {
            if (!ValidarCPF(cpf))
                throw new Exception("CPF informado é inválido");

            CPF = cpf;
            Nome = nome;
            Sexo = sexo;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        /**
         *  Esse método verifica se o CPF é válido
         */
        public static bool ValidarCPF(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            foreach (char caracter in cpf)
                if (!char.IsDigit(caracter))
                    return false;

            int[] multiplicadoresPrimeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadoresSegundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadoresPrimeiroDigito[i];

            int resto = soma % 11;
            int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            tempCpf += primeiroDigitoVerificador;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadoresSegundoDigito[i];

            resto = soma % 11;
            int segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(primeiroDigitoVerificador.ToString() + segundoDigitoVerificador.ToString());
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
            return $"{CPF}{Nome.PadRight(50)}{FormatarData(DataNascimento)}{Sexo}{FormatarData(UltimaCompra)}{FormatarData(DataCadastro)}{Situacao}";
        }
    }
}
