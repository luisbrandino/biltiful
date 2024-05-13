namespace biltiful.Classes
{
    internal class Cliente : IEntidade
    {
        public string CPF { get; private set; }
        public string Nome { get; set; }
        public DateOnly DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateOnly UltimaCompra {  get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         *  Esse construtor permite a criação de um objeto vazio para ser posteriormente populado com a linha vinda do arquivo
         */
        public Cliente()
        {

        }

        /**
         *  Esse construtor serve para carregar um objeto Cliente com a linha vinda do arquivo referente
         */
        public Cliente(string linha)
        {
            LinhaParaObjeto(linha);
        }

        /**
         *  Esse construtor cria o objeto com suas informações sendo passadas diretamente
         */
        public Cliente(string cpf, string nome, DateOnly dataNascimento, char sexo)
        {
            if (!VerificarCPF(cpf))
                throw new ArgumentException("CPF informado é inválido");

            if (!VerificarDataDeNascimento(dataNascimento))
                throw new ArgumentException("Data de nascimento não pode ser posterior à data atual");

            if (!VerificarSexo(sexo))
                throw new ArgumentException("Sexo inválido");

            CPF = cpf;
            Nome = nome.ToUpper();
            Sexo = char.ToUpper(sexo);
            DataNascimento = dataNascimento;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        /**
         *  Esse método verifica se o sexo é válido
         */
        public static bool VerificarSexo(char sexo)
        {
            return char.ToUpper(sexo) == 'F' || char.ToUpper(sexo) == 'M';
        }

        /**
         *  Esse método verifica se a data de nascimento é superior à data atual
         */
        public static bool VerificarDataDeNascimento(DateOnly dataNascimento)
        {
            return dataNascimento <= DateOnly.FromDateTime(DateTime.Now);
        }

        /**
         *  Esse método verifica se o CPF é válido
         */
        public static bool VerificarCPF(string cpf)
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
            return $"{CPF}{Nome.PadRight(Constantes.TAMANHO_NOME_CLIENTE)}{FormatarData(DataNascimento)}{char.ToUpper(Sexo)}{FormatarData(UltimaCompra)}{FormatarData(DataCadastro)}{char.ToUpper(Situacao)}";
        }

        /**
         *  Esse método constrói o objeto a partir da linha vinda do arquivo
         */
        public void LinhaParaObjeto(string linha)
        {
            if (linha.Length != 87)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade Cliente");

            CPF = linha.Substring(0, 11);
            Nome = linha.Substring(11, Constantes.TAMANHO_NOME_CLIENTE).Trim().ToUpper();

            int dia = int.Parse(linha.Substring(61, 2));
            int mes = int.Parse(linha.Substring(63, 2));
            int ano = int.Parse(linha.Substring(65, 4));

            DataNascimento = new DateOnly(ano, mes, dia);

            Sexo = linha.Substring(69, 1).First();

            dia = int.Parse(linha.Substring(70, 2));
            mes = int.Parse(linha.Substring(72, 2));
            ano = int.Parse(linha.Substring(74, 4));

            UltimaCompra = new DateOnly(ano, mes, dia);

            dia = int.Parse(linha.Substring(78, 2));
            mes = int.Parse(linha.Substring(80, 2));
            ano = int.Parse(linha.Substring(82, 4));

            DataCadastro = new DateOnly(ano, mes, dia);

            Situacao = linha.Substring(86, 1).First();
        }

        public override string ToString()
        {
            return $"CPF: {CPF}\nNome: {Nome}";
        }
    }
}
