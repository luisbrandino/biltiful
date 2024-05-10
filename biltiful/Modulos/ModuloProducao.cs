using biltiful.Classes;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

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

            int idAtual;

            #region Preenchendo Listas
            List<string> listaCodigoBarrasCosmetico = LerChavesAquivo(caminho, arquivoCosmetico, 13);
            List<string> listaIdMateriaCosmetico = LerChavesAquivo(caminho, arquivoMateria, 6);
            List<Producao> listaProducao = LerArquivoProducao();
            List<ItemProducao> listaItemProducao = LerArquivoItemProducao();
            #endregion


            #region Funcoes

            bool ChecarSeExisteArquivo(string caminho, string arquivo)
            {
                try
                {
                    if (!Directory.Exists(caminho))
                    {
                        Directory.CreateDirectory(caminho);
                    }
                    if (!File.Exists(caminho + arquivo))
                    {
                        File.Create(caminho + arquivo).Close();
                        
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    ChecarSeExisteArquivo(caminho, arquivo);
                }
                catch (FileNotFoundException)
                {
                    ChecarSeExisteArquivo(caminho, arquivo);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return true;
            }

            List<string> LerChavesAquivo(string caminho, string arquivo, int tamanho)
            {
                List<string> list = new List<string>();
                if (ChecarSeExisteArquivo(caminho, arquivo))
                {
                    foreach (var item in File.ReadAllLines(caminho + arquivo))
                    {
                        list.Add(item.Substring(0, tamanho));
                    }
                }

                return list;
            }

            List<Producao> LerArquivoProducao()
            {
                List<Producao> lista = new List<Producao>();
                if (ChecarSeExisteArquivo(caminho, arquivoProducao))
                {
                    int id;
                    double quantidade;
                    string produto, dia, mes, ano;
                    DateOnly dataProducao;

                    foreach (var item in File.ReadAllLines(caminho + arquivoProducao))
                    {
                        idAtual = id = int.Parse(item.Substring(0, 5));
                        dia = item.Substring(5, 2);
                        mes = item.Substring(7, 2);
                        ano = item.Substring(9, 4);
                        dataProducao = DateOnly.Parse($"{dia}/{mes}/{ano}");
                        produto = item.Substring(13, 13);
                        quantidade = double.Parse(item.Substring(26, 5).Insert(3, ","));
                        Console.WriteLine();

                        lista.Add(new(id, dataProducao, produto, quantidade));
                    }
                    
                }
                return lista;
            }

            List<ItemProducao> LerArquivoItemProducao()
            {
                List<ItemProducao> lista = new();
                if (ChecarSeExisteArquivo(caminho, arquivoItemProducao))
                {
                    int id, dia, mes, ano;
                    string materiaPrima;
                    DateOnly dataProducao;
                    double quantidadeMateriaPrima;

                    foreach (var item in File.ReadAllLines(caminho + arquivoItemProducao))
                    {
                        id = int.Parse(item.Substring(0, 5));
                        dia = int.Parse (item.Substring(5, 2));
                        mes = int.Parse (item.Substring(7, 2));
                        ano = int.Parse (item.Substring(9, 4));
                        dataProducao = new(ano, mes, dia);
                        materiaPrima = item.Substring(13, 6);
                        quantidadeMateriaPrima = double.Parse(item.Substring(19, 5).Insert(3, ","));

                        lista.Add(new ItemProducao(id, dataProducao, materiaPrima, quantidadeMateriaPrima));
                    }
                }
                return lista;
            }


            #endregion

            Console.WriteLine();
        }

    }
}
