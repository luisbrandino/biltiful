namespace biltiful.Modulos.Operacoes.Entradas
{
    internal class EntradaChar : Entrada<char>
    {

        protected override char Formatar(string valor)
        {
            return valor.First();
        }

    }
}
