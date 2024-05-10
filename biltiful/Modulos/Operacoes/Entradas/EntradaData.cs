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
                    MostrarMensagemDeErro();
                    Console.Write("Tente novamente: ");
                    continue;
                }

                bool invalid = false;

                foreach (Func<DateOnly?, bool> regra in regras)
                {
                    if (!regra(data))
                    {
                        invalid = true;
                        break;
                    }
                }

                if (!invalid)
                    return data;

                MostrarMensagemDeErro();
                Console.Write("Tente novamente: ");
            }
        }

    }
}
