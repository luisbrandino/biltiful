using biltiful.Classes;
using System.Collections.Generic;
using System.Reflection.Metadata;
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
            List<string> listaCodigoBarrasCosmetico = LerChavesAquivo(caminho, arquivoCosmetico, 13);
            List<string> listaIdMateriaCosmetico = LerChavesAquivo(caminho, arquivoMateria, 6);

            List<Producao> listaProducao = arqProducao.Ler();
            List<ItemProducao> listaItemProducao = arqItemProducao.Ler();
            #endregion


            #region Funcoes

            List<string> LerChavesAquivo(string caminho, string arquivo, int tamanhoChave)
            {
                List<string> list = new List<string>();

                foreach (var item in File.ReadAllLines(caminho + arquivo))
                {
                    list.Add(item.Substring(0, tamanhoChave));
                }

                return list;
            }


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

                do
                {
                    Console.WriteLine("Insira o código de barras do produto a ser produzido: ");
                    produto = Console.ReadLine();
                    if (!listaCodigoBarrasCosmetico.Contains(produto))
                    {
                        Console.WriteLine("Cosmetico não encontrado...\nInsira novamente por favor.");
                    }
                    else
                    {
                        break;
                    }

                } while (true);

                do
                {
                    Console.WriteLine("Insira a quantidade a ser produzida");
                    Console.WriteLine("Maximo de caracteres: 6");
                    quantidade = double.Parse(Console.ReadLine());
                    if (quantidade > 999.99)
                    {
                        Console.WriteLine("Valor ultrapassa o quantidade máxima de caracteres...\nTente novamente.");
                    }
                    else
                        break;
                } while (true);

                return new(id, dataProducao, produto, quantidade);
            }

            #endregion
            
            arqProducao.Inserir(CadastrarProducao());

            Console.WriteLine();
        }

    }
}
