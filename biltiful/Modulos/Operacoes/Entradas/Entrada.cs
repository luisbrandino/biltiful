using System.Globalization;

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
            Console.Write(mensagemDeErro);
        }

        public bool Verificar(T valor) => validacao.Invoke(valor);
    }

    internal class Entrada<T>
    {
        protected List<Regra<T>> regras = new List<Regra<T>>();
        protected string mensagemValorInvalido = "Valor inválido, tente novamente: ";

        public void AdicionarRegra(Func<T, bool> validacao)
        {
            regras.Add(new(validacao));
        }

        public void AdicionarRegra(Func<T, bool> validacao, string mensagemDeErro)
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

        protected void MostrarValorInvalido()
        {
            Console.Write(mensagemValorInvalido);
        }

        protected virtual T? Formatar(string valor)
        {
            return (T?)Convert.ChangeType(valor, typeof(T), CultureInfo.InvariantCulture);
        }

        public T Pegar()
        {
            while (true)
            {
                T? entrada;

                try
                {
                    entrada = Formatar(Console.ReadLine());
                }
                catch (Exception) 
                {
                    MostrarValorInvalido();
                    continue;
                }

                if (ValidarRegras(entrada))
                    return entrada;

                Console.Write(". Tente novamente: ");
            }
        }
    }
}
