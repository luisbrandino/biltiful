using biltiful.Classes;

namespace biltiful.Modulos.Operacoes
{
    internal class OperacaoImpressaoCliente
    {
        Arquivo<Cliente> arquivo;

        public OperacaoImpressaoCliente()
        {
            arquivo = new Arquivo<Cliente>(Constantes.DIRETORIO, Constantes.CLIENTE_ARQUIVO);   
        }

        public void Executar()
        {
            Console.Clear();
            List<Cliente> clientes = arquivo.Ler();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Não há clientes cadastrados!");
                Console.ReadKey();

                return;
            }

            if (clientes.Count == 1)
            {
                Console.WriteLine(clientes[0]);

                Console.WriteLine("\nNão há mais clientes a serem exibidos");
                Console.ReadKey();

                return;
            }

            int indiceAtual = 0;
            bool continuar = true;

            while (continuar)
            {
                Cliente? cliente = clientes.ElementAtOrDefault(indiceAtual);

                if (cliente == null)
                {
                    indiceAtual = indiceAtual > 0 ? indiceAtual - 1 : indiceAtual + 1;
                    continue;
                }

                Console.Clear();
                Console.WriteLine($"Cliente {indiceAtual + 1}:");
                Console.WriteLine(cliente);
                Console.WriteLine();
                Console.Write("\t");

                Console.Write(indiceAtual > 0 ? "<- " : "   ");

                Console.Write($"{indiceAtual + 1}/{clientes.Count}");

                if (indiceAtual < clientes.Count - 1)
                    Console.Write(" ->");

                Console.WriteLine("\nAperte 0 para sair, 1 para voltar ao início da listagem e 2 para ir ao final da listagem\n");

                while (true)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            indiceAtual--;
                            break;
                        case ConsoleKey.RightArrow:
                            indiceAtual++;
                            break;
                        case ConsoleKey.D0: case ConsoleKey.NumPad0:
                            continuar = false;
                            break;
                        case ConsoleKey.D1: case ConsoleKey.NumPad1:
                            indiceAtual = 0;
                            break;
                        case ConsoleKey.D2: case ConsoleKey.NumPad2:
                            indiceAtual = clientes.Count - 1;
                            break;
                        default:
                            continue;
                    }

                    break;
                }
            }
        }

    }
}
