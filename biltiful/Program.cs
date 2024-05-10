using biltiful;

int resposta;
ControleCompra compra = new ControleCompra();

//Execução do Programa
#region MenuOpcoes

int Menu()
{
    Console.WriteLine(">>> Menu <<<<");
    Console.WriteLine("Selecione a opção desejada: ");
    Console.WriteLine("1 - Comprar a Matéria Prima");
    Console.WriteLine("2 - Localizar Compra");
    Console.WriteLine("3 - Excluir Compra");
    Console.WriteLine("4 - Imprimir Compra");
    Console.WriteLine("5 - Sair");
    Console.Write("Sua resposta: ");
    resposta = int.Parse(Console.ReadLine());

    return resposta;
}

do
{
    switch (Menu())
    {
        case 1:
            Console.WriteLine("\n--- Compra da Matéria Prima ---");
            compra.RealizarCompra();
            break;

        case 2:
            Console.WriteLine("\n--- Localizar Compra ---");
            compra.LocalizarCompra();
            break;

        case 3:
            Console.WriteLine("\n--- Excluir Compra ---");
            compra.ExcluirCompra();
            break;

        case 4:
            Console.WriteLine("\n--- Imprimir Compra ---");
            compra.ImpressaoPorRegistro();
            break;

        case 5:
            Console.WriteLine("\nSaindo...");
            break;

        default:
            Console.WriteLine("\nOpção Inválida!");
            break;
    }
    Console.WriteLine();
} while (Menu() != 5);
#endregion