namespace biltiful
{
    internal class ItemVenda
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public int ValorUnitario { get; set; }
        public int TotalItem { get; set; }

        public ItemVenda(int id, string? produto, int quantidade, int valorUnitario, int totalItem)
        {
            Id = id;
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }

        public ItemVenda()
        {

        }

        public ItemVenda CadastrarItemVenda(int id)
        {
            int totalItem = 0;
            List<ItemVenda> itensVenda = new List<ItemVenda>();

            Console.WriteLine("\nAtenção! São permitidos apenas 3 tipos de produtos por venda!\n");

            do
            {
                Console.Write("Informe quantos produtos fazem parte da venda: ");
                int qtdItens = int.Parse(Console.ReadLine());
                if (qtdItens > 3)
                {
                    Console.WriteLine("\nO valor informado excede o limite da venda de produtos!");
                    Console.Write("Pressione qualquer tecla: ");
                    Console.ReadLine();
                }

                for (int i = 0; i < qtdItens; i++)
                {
                    Console.WriteLine($"\nVENDA - CÓDIGO (ID): {id}");

                    Console.Write("Informe o ID do produto: ");
                    string produto = Console.ReadLine();

                    Console.Write("Informe a quantidade de produtos: ");
                    int quantidadeItem = int.Parse(Console.ReadLine());

                    Console.Write("Informe o valor da unidade do produto: ");
                    int valorUnitario = int.Parse(Console.ReadLine());

                    int totalProduto = quantidadeItem * valorUnitario;
                    totalItem += totalProduto;

                    itensVenda.Add(new ItemVenda(id, produto, quantidadeItem, valorUnitario, totalProduto));
                }

            } while (itensVenda.Count < 1 || itensVenda.Count > 3);

            return itensVenda[itensVenda.Count - 1];
        }
    }
}