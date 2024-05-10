using System;
using System.Collections.Generic;

namespace biltiful
{
    internal class ItemVenda
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public int ValorUnitario { get; set; }
        public int TotalItem { get; set; }

        public ItemVenda(int id, string produto, int quantidade, int valorUnitario, int totalItem)
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

        public List<ItemVenda> CadastrarItemVenda(int id)
        {
            List<ItemVenda> itensVenda = new List<ItemVenda>();

            Console.WriteLine("\n  ---- CADASTRAR VENDA ----");

            Console.WriteLine("\n| Atenção! São permitidos apenas 3 tipos de produtos por venda!|");
            Console.WriteLine("| A escolha não pode ser alterada.");
            Console.WriteLine("\n| Digite |SAIR| a qualquer momento para cancelar a venda"); //TALVEZ   
            Console.Write("\n\n| Pressione qualquer tecla para continuar: ");
            Console.ReadLine();
            Console.Clear();

            Console.Write("|Informe quantos produtos fazem parte da venda: ");
            int qtdItens = int.Parse(Console.ReadLine());
            Console.Clear();
            if (qtdItens <= 3)
            {
                for (int i = 0; i < qtdItens; i++)
                {

                    Console.WriteLine($"\nVENDA - CÓDIGO (ID): {id + 1}");

                    Console.Write("\n|Informe o ID (Código de Barras) do produto: ");
                    string produto = Console.ReadLine();

                    Console.Write("|Informe a quantidade de produtos (máximo 999): ");
                    int quantidadeItem = int.Parse(Console.ReadLine());

                    if (quantidadeItem <= 999)
                    {
                        Console.Write("|Informe o valor da unidade do produto (Centavos): ");
                        int valorUnitario = int.Parse(Console.ReadLine());

                        int totalProduto = quantidadeItem * valorUnitario; // Calcula o valor total do item

                        if (totalProduto > 9999)
                        {
                            Console.WriteLine("\n| O valor total do item excede o limite permitido (9.999).");
                            Console.WriteLine("| A venda foi cancelada.");
                            Console.ReadLine() ;
                            return new List<ItemVenda>(); // Retorna uma lista vazia indicando que a venda foi cancelada
                        }

                        itensVenda.Add(new ItemVenda(id, produto, quantidadeItem, valorUnitario, totalProduto));
                    }
                    else
                    {
                        Console.WriteLine("\n| A quantidade de produtos excede o limite permitido (999).");
                        Console.WriteLine("| A venda foi cancelada.");
                        return new List<ItemVenda>(); // Retorna uma lista vazia indicando que a venda foi cancelada
                    }
                }
            }
            else
            {
                Console.WriteLine("\n|A quantidade de produtos excede o limite permitido.");
                Console.WriteLine("\n|A venda foi cancelada.");
                Console.ReadKey();
            }

            return itensVenda;
        }
    }
}
