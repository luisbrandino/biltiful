using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace biltiful
{
    internal class Venda
    {
        private static int proximoId = 1; // Próximo ID disponível para venda
        private const int maxId = 99999; // Valor máximo do ID de venda
        private const string idFormat = "00000"; // Formato para preenchimento do ID com zeros à esquerda

        public int Id { get; } // ID da venda
        public DateOnly DataVenda { get; set; }
        public string Cliente { get; set; }
        public int ValorTotal { get; set; }
        public List<ItemVenda> ItensVenda { get; set; }

        // Construtor que aceita os parâmetros necessários
        public Venda(int id, DateOnly dataVenda, string cliente, int valorTotal, List<ItemVenda> itensVenda)
        {
            Id = id;
            DataVenda = dataVenda;
            Cliente = cliente;
            ValorTotal = valorTotal;
            ItensVenda = itensVenda;
        }

        // Construtor padrão
        public Venda()
        {
        }

        public Venda(string data)
        {
            Id = int.Parse(data.Substring(0, 5));
            DataVenda = DateOnly.ParseExact(data.Substring(5, 8), "ddMMyyyy", null);
            Cliente = data.Substring(13, 11);
            ValorTotal = int.Parse((data.Substring(24, 7))) / 100;
        }

        // Método para cadastrar uma nova venda
        public Venda CadastrarVenda()
        {
            List<ItemVenda> itensVenda = new List<ItemVenda>();

            ItemVenda itemVenda = new ItemVenda();
            var items = itemVenda.CadastrarItemVenda(Id); // Adiciona os itens de venda à lista de itens da venda
            if (items.Count > 0 && items.Count <= 3)
            {
                itensVenda.AddRange(items);

                DateOnly dataVenda = DateOnly.FromDateTime(DateTime.Now);
                //var teste = DateTime.Today;

                Console.Write("|Informe o CPF do cliente: ");
                string cliente = Console.ReadLine();

                // Calcula o valor total da venda
                int valorTotal = 0;
                foreach (var item in itensVenda)
                {
                    valorTotal += item.TotalItem; //Não pode ser maior que 99.999
                }

                // Cria e retorna a venda
                return new Venda(Id, dataVenda, cliente, valorTotal, itensVenda);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("                   FALHA AO CADASTRAR VENDA!");                        
                Console.WriteLine("------------------------------------------------------------------------");
              
                Console.WriteLine("\n|A venda foi cancelada devido ao valor inválido de itens permitidos|");
                Console.ReadKey();
          
                return null;
            }

           

        }
     
    }
}
