namespace biltiful
{
    internal class Menu
    {
        string? titulo;
        List<string> opcoes;
        bool limpar = false;

        public Menu(params string[] opcoes)
        {
            this.opcoes = new List<string>(opcoes);
        }

        public void LimparAposImpressao(bool limpar)
        {
            this.limpar = limpar;
        }

        public void DefinirTitulo(string titulo)
        {
            this.titulo = titulo;
        }

        public void AdicionarOpcao(string descricao)
        {
            opcoes.Add(descricao);
        }

        private void Mostrar()
        {
            if (limpar)
                Console.Clear();

            Console.WriteLine(this.titulo);
            for (int i = 0; i < opcoes.Count; i++)
                Console.WriteLine($"{i + 1} - {opcoes[i]}");
        }

        public int Perguntar()
        {
            Mostrar();
            Console.Write("Sua opção: ");

            int opcao;

            while (true)
            {
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Write("Valor inválido, digite apenas números. Tente novamente: ");
                    continue;
                }

                bool opcaoCorreta = opcao >= 1 && opcao <= opcoes.Count;

                if (opcaoCorreta)
                    break;

                Console.Write($"Valor inválido, digite apenas valores entre {1} e {opcoes.Count}. Tente novamente: ");
            }

            return opcao;
        }
    }
}
