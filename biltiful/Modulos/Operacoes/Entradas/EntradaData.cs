using System.Globalization;

namespace biltiful.Modulos.Operacoes.Entradas
{
    internal class EntradaData : Entrada<DateOnly>
    {
        public EntradaData()
        {
            mensagemValorInvalido = "Data inválida. Formato: dd/mm/aaaa. Tente novamente: ";
        }

        protected override DateOnly Formatar(string valor)
        {
            return DateOnly.Parse(valor, new CultureInfo("pt-BR"));
        }

    }
}
