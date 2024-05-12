namespace biltiful.Modulos.Operacoes
{
    internal class Navegador<T>
    {
        List<T> elementos;
        int indiceAtual = 0;
        bool navegando = false;

        public Navegador(List<T> elementos)
        {
            this.elementos = elementos;
        }

        void MostrarListagemAtual()
        {
            Console.Clear();

            T elemento = elementos.ElementAt(indiceAtual);

            Console.WriteLine($"{elemento.GetType().Name} {indiceAtual + 1}:");
            Console.WriteLine(elemento);
            Console.WriteLine();
            Console.Write("\t");

            Console.Write(indiceAtual > 0 ? "<- " : "   ");

            Console.Write($"{indiceAtual + 1}/{elementos.Count}");

            if (indiceAtual < elementos.Count - 1)
                Console.Write(" ->");
        }

        void PegarAcaoDoUsuario()
        {
            Console.Write("\nAperte 0 para sair, 1 para voltar ao início da listagem e 2 para ir ao final da listagem\n");

            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        Anterior();
                        break;
                    case ConsoleKey.RightArrow:
                        Proximo();
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        Parar();
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Inicio();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Final();
                        break;
                    default:
                        continue;
                }

                break;
            }

            
        }

        public void Proximo()
        {
            if (indiceAtual + 1 < elementos.Count)
                indiceAtual++;
        }

        public void Anterior()
        {
            if (indiceAtual > 0)
                indiceAtual--;
        }

        public void Final()
        {
            indiceAtual = elementos.Count - 1;
        }

        public void Inicio()
        {
            indiceAtual = 0;
        }

        public void Parar()
        {
            navegando = false;
        }

        public void Iniciar()
        {
            if (elementos.Count <= 0)
                return;

            navegando = true;

            while (navegando)
            {
                MostrarListagemAtual();
                PegarAcaoDoUsuario();
            }
        }
    }
}
