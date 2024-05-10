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
            Nome = dados.Substring(6, 20).Trim();
            UltimaCompra = DateOnly.ParseExact(dados.Substring(26, 8), "ddMMyyyy");
            DataCadastro = DateOnly.ParseExact(dados.Substring(34, 8), "ddMMyyyy");
            Situacao = dados.Substring(42, 1).First();
        }

        /**
         *  Construtor para criar o objeto com as propriedades diretamente 
         */
        public MPrima(string id, string nome)
        {
            id = id.ToUpper();

            if (!VerificarId(id))
                throw new ArgumentException("ID informado inválido");

            Id = id;
            Nome = nome;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        /**
         *  Verifica se o campo id cumpre os requisitos esperados
         */
        public static bool VerificarId(string id)
        {
            if (id.Length != 6)
                return false;

            return id.StartsWith("MP");
        }

        /**
         *  Formata o tipo de data (não pertence aqui)
         */
        public string FormatarData(DateOnly data)
        {
            return $"{data.Day.ToString("00")}{data.Month.ToString("00")}{data.Year.ToString("0000")}";
        }

        /**
         * Esse método transforma o objeto atual em uma linha que pode ser escrita
         */
        public string FormatarParaArquivo()
        {
            return $"{Id.ToUpper()}{Nome.PadRight(20)}{FormatarData(UltimaCompra)}{FormatarData(DataCadastro)}{Situacao}";
        }
    }
}
