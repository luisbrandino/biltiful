﻿using biltiful.Classes;

namespace biltiful.Modulos
{
    internal class ModuloCosmetico
    {

        public void Executar()
        {
            Compra compra;

            int Menu()
            {
                Console.Clear();
                Console.WriteLine(">>> Menu <<<<");
                Console.WriteLine("Selecione a opção desejada: ");
                Console.WriteLine("1 - Comprar a Matéria Prima");
                Console.WriteLine("2 - Localizar Compra");
                Console.WriteLine("3 - Excluir Compra");
                Console.WriteLine("4 - Imprimir Compra");
                Console.WriteLine("5 - Sair");
                Console.Write("Sua resposta: ");
                int resposta = int.Parse(Console.ReadLine());

                return resposta;
            }

            do
            {
                switch (Menu())
                {
                    case 1:
                        compra = new Compra();
                        Console.WriteLine("\n--- Compra da Matéria Prima ---");
                        compra.CadastrarCompra();
                        break;

                    case 2:
                        compra = new Compra();
                        Console.WriteLine("\n--- Localizar Compra ---");
                        compra.LocalizarCompra();
                        break;

                    case 3:
                        compra = new Compra();
                        Console.WriteLine("\n--- Excluir Compra ---");
                        compra.ExcluirCompra();
                        break;

                    case 4:
                        compra = new Compra();
                        Console.WriteLine("\n--- Imprimir Por Registro ---");
                        compra.ImpressaoPorRegistro();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("\nOpção Inválida!");
                        break;
                }
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                Console.WriteLine();
            } while (true);

        }

    }
}
