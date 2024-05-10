namespace biltiful
{

    internal class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public string Cliente { get; set; }
        public int ValorTotal { get; set; }
        public List<ItemVenda> ItensVenda { get; set; }

        public Venda(int id, DateTime dataVenda, string cliente, int valorTotal, List<ItemVenda> itensVenda)
        {
            Id = id;
            DataVenda = dataVenda;
            Cliente = cliente;
            ValorTotal = valorTotal;
            ItensVenda = itensVenda;
        }

        public Venda()
        {
        }

        public Venda CadastrarVenda()
        {
            Console.Write("Informe o ID da venda: ");
            int id = int.Parse(Console.ReadLine());

            List<ItemVenda> itensVenda = new List<ItemVenda>();

            ItemVenda itemVenda = new ItemVenda();
            itensVenda.Add(itemVenda.CadastrarItemVenda(id));

            DateTime dataVenda = DateTime.Today;

            Console.Write("Informe o CPF do cliente: ");
            string cliente = Console.ReadLine();

            int valorTotal = 0;
            foreach (var item in itensVenda)
            {
                valorTotal += item.TotalItem;
            }

            return new Venda(id, dataVenda, cliente, valorTotal, itensVenda);
        }
    }
}