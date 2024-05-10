namespace biltiful.Modulos.Operacoes.Entradas
{
    internal class Regra<T>
    {
        Func<T, bool> validacao;
        string? mensagemDeErro;

        public Regra(Func<T, bool> validacao, string mensagemDeErro)
        {
            this.validacao = validacao;
            this.mensagemDeErro = mensagemDeErro;
        }

        public Regra(Func<T, bool> validacao)
        {
            this.validacao = validacao;
        }

        public void MostrarMensagemDeErro()
        {
            Console.WriteLine(mensagemDeErro);
        }

        public bool Verificar(T valor) => validacao.Invoke(valor);
    }

    internal class Entrada<T>
    {
        protected List<Regra<T>> regras = new List<Regra<T>>();

        public void AdicionarRegra(Func<T?, bool> validacao)
        {
            regras.Add(new(validacao));
        }

        public void AdicionarRegra(Func<T?, bool> validacao, string mensagemDeErro)
        {
            regras.Add(new(validacao, mensagemDeErro));
        }

        protected bool ValidarRegras(T valor)
        {
            foreach (Regra<T> regra in regras)
                if (!regra.Verificar(valor))
                {
                    regra.MostrarMensagemDeErro();
                    return false;
                }

            return true;
        }

        public T? Pegar()
        {
            while (true)
            {
                T? entrada;

                try
                {
                    entrada = (T?) Convert.ChangeType(Console.ReadLine(), typeof(T));
                }
                catch (Exception) 
                {
                    Console.Write("Valor inválido, tente novamente: ");
                    continue;
                }

                if (ValidarRegras(entrada))
                    return entrada;

                Console.Write("Tente novamente: ");
            }
        }
    }
}
