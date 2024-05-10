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
                        id = int.Parse(item.Substring(0, 5));
                        dia = item.Substring(5, 2);
                        mes = item.Substring(7, 2);
                        ano = item.Substring(9, 4);
                        dataProducao = DateOnly.Parse($"{dia}/{mes}/{ano}");
                        produto = item.Substring(13, 13);
                        quantidade = double.Parse(item.Substring(26, 5).Insert(3, ","));
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
                        dia = int.Parse(item.Substring(5, 2));
                        mes = int.Parse(item.Substring(7, 2));
                        ano = int.Parse(item.Substring(9, 4));
                        dataProducao = new(ano, mes, dia);
                        materiaPrima = item.Substring(13, 6);
                        quantidadeMateriaPrima = double.Parse(item.Substring(19, 5).Insert(3, ","));

                        lista.Add(new ItemProducao(id, dataProducao, materiaPrima, quantidadeMateriaPrima));
                    }
                }
                return lista;
            }

            string RemoverCasasDecimaisDesnecessarias(string str)
            {
                try
                {
                    int indiceVirgula = str.IndexOf(',');
                    str = str.Remove(indiceVirgula + 3);
                }catch(Exception ex)
                {

                }
                return str;
            }
            string RemoverVirgula(string s)
            {
                s = RemoverCasasDecimaisDesnecessarias(s);
                s = s.Replace(",", "");

                return s;
            }

            void CadastrarProducao()
            {
                int id = listaProducao.Last().Id + 1;
                string produto, quantidade;
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
                    quantidade = Console.ReadLine();
                    if (quantidade.Length > 6 || quantidade == "")
                    {
                        Console.WriteLine("Valor ultrapassa o quantidade máxima de caracteres...\nTente novamente.");
                    }
                    else
                        break;
                } while (true);

                quantidade = RemoverCasasDecimaisDesnecessarias(quantidade);

                listaProducao.Add(new(id, dataProducao, produto, double.Parse(quantidade)));
            }

            #endregion


            CadastrarProducao();

            Console.WriteLine();
        }

    }
}
