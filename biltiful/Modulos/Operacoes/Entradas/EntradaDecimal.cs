namespace biltiful.Modulos.Operacoes.Entradas
{
    internal class EntradaDecimal<T> : Entrada<T>
    {

        protected override T? Formatar(string valor)
        {
            return base.Formatar(valor.Replace(',', '.'));
        }

    }
}
