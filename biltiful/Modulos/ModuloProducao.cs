﻿using biltiful.Classes;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace biltiful.Modulos
{
    internal class ModuloProducao
    {

        public void Executar()
        {
            #region Caminhos de arquivos
            string caminho = @"C:\Biltiful\";
            string arquivoProducao = "Producao.dat";
            string arquivoCosmetico = "Cosmetico.dat";
            string arquivoMateria = "Materia.dat";
            string arquivoItemProducao = "ItemProducao.dat";
            #endregion

            Arquivo<Producao> arqProducao = new(caminho, arquivoProducao);
            Arquivo<ItemProducao> arqItemProducao = new(caminho, arquivoItemProducao);
            Arquivo<Produto> arqProduto = new(caminho, arquivoCosmetico);
            Arquivo<MPrima> arqMateria = new(caminho, arquivoMateria);

            #region Preenchendo Listas
            List<Produto> listaCosmeticos = arqProduto.Ler();
            List<MPrima> listaMaterias = arqMateria.Ler();

            List<Producao> listaProducao = arqProducao.Ler();
            List<ItemProducao> listaItemProducao = arqItemProducao.Ler();
            #endregion


            #region Funcoes

            Producao CadastrarProducao()
            {
                int id;
                if (listaProducao.Count() == 0)
                    id = 1;
                else
                    id = listaProducao.Last().Id + 1;

                string produto;
                double quantidade;
                DateOnly dataProducao = DateOnly.FromDateTime(DateTime.Now);
                bool continua = true;
                Produto p = new Produto();
                do
                {
                    Console.WriteLine("\nInsira o código de barras do produto a ser produzido: ");
                    produto = Console.ReadLine();


                    foreach (var item in listaCosmeticos)
                    {
                        if (item.CodigoBarras == produto && item.Situacao == 'A')
                        {
                            p = item;
                            Console.WriteLine("Produto encontrado e disponível");

                            continua = false;
                            break;
                        }

                    }
                    if (continua)
                        Console.WriteLine("\nCosmético não encontrado ou inativo\nInsira novamente por favor.");
                } while (continua);

                do
                {
                    Console.WriteLine($"\nInsira a quantidade de {p.Nome} a ser produzida");
                    Console.WriteLine("Quantidade máxima: 999,99");
                    quantidade = double.Parse(Console.ReadLine());
                    if (quantidade > 999.99)
                    {
                        Console.WriteLine("Valor ultrapassa o quantidade máxima de caracteres...\nTente novamente.");
                    }
                    else
                        break;
                } while (true);

                Producao producao = new(id, dataProducao, produto, quantidade);
                int opcao;
                do
                {
                    CadastrarItemProducao(id);
                    Console.WriteLine("Deseja inserir outra matéria prima?");
                    Console.WriteLine("1 - sim | 2 - não");
                    opcao = int.Parse(Console.ReadLine());

                    if (opcao == 1)
                        continua = true;

                    else
                        break;


                } while (continua);
                return producao;
            }

            void CadastrarItemProducao(int id)
            {
                DateOnly dataProducao = DateOnly.FromDateTime(DateTime.Now);
                string materiaPrima;
                double quantidade;
                bool continua = true;
                MPrima materia = new();
                do
                {
                    Console.WriteLine("\nDigite o identificador da matéria prima: ");
                    materiaPrima = Console.ReadLine().ToUpper();
                    foreach (var item in listaMaterias)
                    {
                        if (item.Id == materiaPrima && item.Situacao == 'A')
                        {
                            materia = item;
                            Console.WriteLine("\nMateria prima encontrada e disponível");
                            continua = false;
                            break;
                        }
                    }
                    if (continua)
                        Console.WriteLine("\nMateria prima não encontrada ou inativa\nInsira novamente por favor.");


                } while (continua);

                do
                {
                    Console.WriteLine($"znInsira a quantidade de {materia.Nome} a ser utilizada");
                    Console.WriteLine("Quantidade máxima: 999,99");
                    quantidade = double.Parse(Console.ReadLine());
                    if (quantidade > 999.99)
                    {
                        Console.WriteLine("\nValor ultrapassa o quantidade máxima de caracteres...\nTente novamente.");
                    }
                    else
                        break;
                } while (true);
                ItemProducao itemProducao = new(id, dataProducao, materia.Id, quantidade);
                arqItemProducao.Inserir(itemProducao);
            }

            Producao Localicar()
            {

                Console.Write("Digite o Id da produção: ");
                int id = int.Parse(Console.ReadLine());

                foreach (var item in listaProducao)
                {
                    if (item.Id == id)
                        return item;
                }
                throw new Exception();

                return new();
            }

            void Imprimir(Producao p)
            {
                try
                {

                    foreach (var item in listaCosmeticos)
                    {
                        if (p.Produto == item.CodigoBarras)
                        {
                            Console.WriteLine($"Nome do produto: {item.Nome}");
                            break;
                        }
                    }
                    Console.Write(p.Imprimir());

                    Console.WriteLine();
                    Console.WriteLine("Itens utilizados: ");
                    foreach (var item in listaItemProducao)
                    {
                        if (item.Id == p.Id)
                        {
                            Console.WriteLine();
                            foreach (var materiaPrima in listaMaterias)
                            {
                                if (item.MateriaPrima == materiaPrima.Id)
                                    Console.WriteLine("Nome: " + materiaPrima.Nome);
                            }
                            Console.Write(item.Imprimir());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: Não foi possível localizar a produção");

                }
            }

            void Navegar()
            {
                int indice = 0, opc = 0;
                do
                {

                    Console.WriteLine("[1] <- Retroceder | Avançar -> [2]");
                    Console.WriteLine("[3] <- Primeiro | Ultimo -> [4]");
                    Console.WriteLine("|[0] Sair|");
                    opc = int.Parse(Console.ReadLine());

                    switch (opc)
                    {

                        case 1:
                            indice--;
                            if (indice >= 0)
                                Imprimir(listaProducao[indice]);
                            else
                            {
                                Console.WriteLine("Não é possível retroceder mais...");
                                indice++;
                            }
                            break;
                        case 2:
                            indice++;
                            if (indice <= listaProducao.Count() - 1)
                            {
                                Imprimir(listaProducao[indice]);
                            }
                            else
                            {
                                Console.WriteLine("Não é possível avançar mais...");
                                indice--;
                            }
                            break;
                        case 3:
                            Imprimir(listaProducao.First());
                            break;
                        case 4:
                            Imprimir(listaProducao.Last());
                            break;
                        case 0:
                            Console.WriteLine("Saindo...");
                            break;
                        default:
                            Console.WriteLine("Opção não exisente");
                            break;

                    }

                    Console.ReadLine();
                    Console.Clear();
                } while (opc != 0);
            }
            #endregion



            arqProducao.Inserir(CadastrarProducao());
            //Imprimir(Localicar());
            Console.Clear();

            Navegar();

            Console.WriteLine();
        }

    }
}
