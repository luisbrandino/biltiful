using System;

namespace biltiful
{
    class Program
    {
        static void Main(string[] args)
        {
            // Carrega as vendas do arquivo ao iniciar o programa
            Venda.CarregarVendas();
            ItemVenda.CarregarItensVenda();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n---- MENU VENDA ----\n");
                Console.WriteLine("|1| - Cadastrar Nova Venda");
                Console.WriteLine("|2| - Excluir Venda");
                Console.WriteLine("|3| - Listar Vendas");
                Console.WriteLine("|4| - Procurar Venda");
                Console.WriteLine("|5| -  Sair");
                Console.Write("\n|Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        Venda.CadastrarVenda();
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Digite o ID da venda a ser excluída: ");
                        if (int.TryParse(Console.ReadLine(), out int idExcluir))
                        {
                            Venda.ExcluirVenda(idExcluir);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");

                        }
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("\n==== LISTA DE VENDAS ====");
                        Venda.ImprimirVendas();
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.Clear();
                        Venda.ProcurarEImprimirVendaPorId();
                        Console.ReadLine();
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("Saindo...");
                        Console.ReadLine();
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Escolha novamente.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}