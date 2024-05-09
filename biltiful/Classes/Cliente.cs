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
            if (dados.Length < 87)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade Cliente");

            CPF = dados.Take(10).ToString();
            Nome = dados.Skip(10).Take(49).ToString();

            int dia = int.Parse(dados.Skip(60).Take(1).ToString());
            int mes = int.Parse(dados.Skip(61).Take(1).ToString());
            int ano = int.Parse(dados.Skip(62).Take(3).ToString());

            DataNascimento = new DateOnly(ano, mes, dia);

        }

        /**
         *  Esse construtor cria o objeto com suas informações sendo passadas diretamente
         */
        public Cliente(string cpf, string nome, string sexo, DateOnly dataCadastro)
        {

        }

        /**
         *  Esse método verifica se o CPF é válido
         */
        public bool ValidarCPF(string cpf)
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
