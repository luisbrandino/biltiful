namespace biltiful.Modulos.Operacoes.Entradas
{
    internal class EntradaData : Entrada<DateOnly>
    {

        public DateOnly Pegar()
        {
            DateOnly data;

            while (true)
            {
                string? entrada = Console.ReadLine();

                try
                {
                    data = DateOnly.Parse(entrada);
                }
                catch (Exception)
                {
                    Console.Write($"Data inválida. Formato: dd/mm/aaaa.\n");
                    Console.Write("Tente novamente: ");
                    continue;
                }

                if (ValidarRegras(data))
                    return data;

                Console.Write("Tente novamente: ");
            }
        }

    }
}
