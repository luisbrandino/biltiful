namespace biltiful.Modulos.Operacoes.Entradas
{
    internal class Entrada<T>
    {
        protected List<Func<T?, bool>> regras = new List<Func<T?, bool>>();
        protected string mensagemDeErro;
        protected string? mensagemDeUso;

        public Entrada()
        {
        }

        public Entrada(params Func<T?, bool>[] regras)
        {
            this.regras = new List<Func<T?, bool>>(regras);
        }

        public void DefinirMensagemDeUso(string mensagem)
        {
            mensagemDeUso = mensagem;
        }

        public void DefinirMensagemDeErro(string mensagem)
        {
            mensagemDeErro = mensagem;
        }

        public void AdicionarRegra(Func<T?, bool> regra)
        {
            regras.Add(regra);
        }

        protected void MostrarMensagemDeErro()
        {
            Console.Write(mensagemDeErro != null ? mensagemDeErro + ". " : "");
            Console.Write(mensagemDeUso != null ? mensagemDeUso + ". " : "");
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
                    MostrarMensagemDeErro();
                    Console.Write("Tente novamente: ");
                    continue;
                }

                bool invalid = false;

                foreach (Func<T?, bool> regra in regras)
                {
                    if (!regra(entrada))
                    {
                        invalid = true;
                        break;
                    }
                }

                if (!invalid)
                    return entrada;

                MostrarMensagemDeErro();
                Console.Write("Tente novamente: ");
            }
        }
    }
}
