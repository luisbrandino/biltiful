namespace biltiful.Classes
{
    internal class MPrima : IEntidade
    {
        public string Id { get; private set; }
        public string Nome { get; set; }
        public DateOnly UltimaCompra {  get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         *  Construtor vazio para que seja possível popular o objeto posteriormente com a linha vinda do arquivo
         */
        public MPrima()
        {

        }

        /**
         *  Construtor para criar o objeto a partir da linha vinda do arquivo
         */
        public MPrima(string dados)
        {
            LinhaParaObjeto(dados);
        }

        /**
         *  Construtor para criar o objeto com as propriedades diretamente 
         */
        public MPrima(string id, string nome)
        {
            id = id.ToUpper();

            if (!VerificarId(id))
                throw new ArgumentException("ID informado inválido");

            Id = id.ToUpper();
            Nome = nome.ToUpper();
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        /**
         *  Verifica se o campo id cumpre os requisitos esperados
         */
        public static bool VerificarId(string id)
        {
            id = id.ToUpper();

            if (id.Length != 6)
                return false;

            foreach (char caracter in id.Substring(2, 4))
                if (!char.IsDigit(caracter))
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
            return $"{Id.ToUpper()}{Nome.PadRight(Constantes.TAMANHO_NOME_MPRIMA).ToUpper()}{FormatarData(UltimaCompra)}{FormatarData(DataCadastro)}{char.ToUpper(Situacao)}";
        }

        public void LinhaParaObjeto(string linha)
        {
            if (linha.Length != 43)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade MPrima");

            Id = linha.Substring(0, 6).ToUpper();
            Nome = linha.Substring(6, Constantes.TAMANHO_NOME_MPRIMA).Trim();
            UltimaCompra = DateOnly.ParseExact(linha.Substring(26, 8), "ddMMyyyy");
            DataCadastro = DateOnly.ParseExact(linha.Substring(34, 8), "ddMMyyyy");
            Situacao = linha.Substring(42, 1).First();
        }

        public override string ToString()
        {
            return $"ID: {Id}\nNome: {Nome.ToUpper()}";
        }
    }
}
