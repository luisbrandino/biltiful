using System;

namespace biltiful
{
    class Program
    {
        static void Main(string[] args)
        {
            // Carrega as vendas do arquivo ao iniciar o programa
            Venda.CarregarVendas();

            while (true)
            {
                Console.WriteLine("\n==== MENU VENDA ====");
                Console.WriteLine("1. Cadastrar nova venda");
                Console.WriteLine("2. Excluir venda");
                Console.WriteLine("3. Listar vendas");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        Venda.CadastrarVenda();
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
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("\n==== LISTA DE VENDAS ====");
                        Venda.ImprimirVendas();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Escolha novamente.");
                        break;
                }
            }
        }
    }
}
