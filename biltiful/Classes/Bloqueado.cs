namespace biltiful.Classes
{
    internal class Bloqueado : IEntidade
    {
        public string CNPJ { get; private set; }

        public Bloqueado()
        {

        }

        public Bloqueado(string cnpj)
        {
            if (!Fornecedor.VerificarCNPJ(cnpj))
                throw new ArgumentException("CNPJ informado é inválido");

            LinhaParaObjeto(cnpj);
        }

        public string FormatarParaArquivo()
        {
            return CNPJ;
        }

        public void LinhaParaObjeto(string linha)
        {
            if (linha.Length != 14)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade Bloqueado");

            CNPJ = linha.Substring(0, 14);
        }
    }
}
