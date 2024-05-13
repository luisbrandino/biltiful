namespace biltiful.Classes
{
    internal class Inadimplente : IEntidade
    {
        public string CPF { get; private set; }

        public Inadimplente()
        {
            
        }

        public Inadimplente(string cpf)
        {
            if (!Cliente.VerificarCPF(cpf))
                throw new ArgumentException("CPF informado é inválido");

            LinhaParaObjeto(cpf);
        }

        public string FormatarParaArquivo()
        {
            return CPF;
        }

        public void LinhaParaObjeto(string linha)
        {
            if (linha.Length != 11)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade Inadimplente");

            CPF = linha.Substring(0, 11);
        }
    }
}
