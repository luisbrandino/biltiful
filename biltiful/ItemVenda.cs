using System;
using System.IO;
using System.Collections.Generic;

namespace biltiful
{
    public class ItemVenda
    {
        public int IdVenda { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal TotalItem { get; set; }

        private static string arquivo = @"C:\Biltiful\ItemVenda.dat";
        public ItemVenda()
        {
        }

        public ItemVenda(int idVenda, string produto, int quantidade, decimal valorUnitario, decimal totalItem)
        {
            IdVenda = idVenda;
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }

        // Método para criar um novo item de venda
        public static ItemVenda CriarItemVenda(int idVenda)
        {
            Console.WriteLine("Digite o código de barras do produto:");
            string codigoBarras = Console.ReadLine();

            // Verificar se o produto existe e está ativo no arquivo Cosmetico.dat
            var produtoInfo = LerProdutoDoArquivo(codigoBarras);
            if (produtoInfo == null)
            {
                Console.WriteLine("Produto não encontrado ou inativo.");
                return null;
            }

            string produto = produtoInfo.Nome;
            decimal valorUnitario = produtoInfo.ValorVenda;

            Console.WriteLine($"Produto: {produto}, Valor unitário: {valorUnitario}");

            // Solicita e valida a quantidade do produto
            Console.WriteLine("Digite a quantidade do produto:");
            int quantidade;
            if (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0 || quantidade > 999)
            {
                Console.WriteLine("Quantidade inválida.");
                return null;
            }

            // Calcula o valor total do item
            decimal totalItem = quantidade * valorUnitario;

            // Verifica se o valor total do item excede o limite máximo
            if (totalItem > 9999.99m)
            {
                Console.WriteLine("O valor total do item não pode exceder 9.999,99.");
                return null;
            }

            // O ID da venda será o mesmo que o ID da venda
            return new ItemVenda(idVenda, produto, quantidade, valorUnitario, totalItem);
        }

        // Método para ler as informações do produto do arquivo Cosmetico.dat
        private static ProdutoInfo LerProdutoDoArquivo(string codigoBarras)
        {
            string arquivo = @"C:\Biltiful\Cosmetico.dat";

            if (File.Exists(arquivo))
            {
                using (StreamReader sr = new StreamReader(arquivo))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (linha.StartsWith(codigoBarras))
                        {
                            if (linha.Last() == 'A') // Verifica se a linha tem tamanho suficiente e se o status é 'A' (ativo)
                            {
                                string nome = linha.Substring(13, 20).Trim();
                                decimal valorVenda = (decimal)decimal.Parse(linha.Substring(33, 5)) / 100m; // Dividido por 100 para converter centavos
                                return new ProdutoInfo { Nome = nome, ValorVenda = valorVenda };
                            }
                        }
                    }
                }
            }

            return null;
        }

        // Método para salvar os itens de venda em um arquivo
        public static void SalvarItensVenda(List<ItemVenda> itensVenda)
        {
            try
            {
                // Verifica se o arquivo não existe antes de tentar criar
                if (!File.Exists(arquivo))
                {
                    // Cria o arquivo se não existir
                    using (File.Create(arquivo)) { }
                }

                // Escreve os itens no arquivo
                using (StreamWriter sw = new StreamWriter(arquivo))
                {
                    foreach (var item in itensVenda)
                    {
                        sw.WriteLine($"{item.IdVenda},{item.Produto},{item.Quantidade},{item.ValorUnitario},{item.TotalItem}");
                    }
                }

                Console.WriteLine("Itens de venda salvos com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar itens de venda: {ex.Message}");
            }
        }

        // Método para carregar os itens de venda de um arquivo
        public static List<ItemVenda> CarregarItensVenda()
        {
            List<ItemVenda> itensVenda = new List<ItemVenda>();

            if (File.Exists(arquivo))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivo))
                    {
                        string linha;
                        while ((linha = sr.ReadLine()) != null)
                        {
                            string[] dados = linha.Split(',');

                            int idVenda = int.Parse(dados[0]);
                            string produto = dados[1];
                            int quantidade = int.Parse(dados[2]);
                            decimal valorUnitario = decimal.Parse(dados[3]);
                            decimal totalItem = decimal.Parse(dados[4]);

                            itensVenda.Add(new ItemVenda(idVenda, produto, quantidade, valorUnitario, totalItem));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar itens de venda: {ex.Message}");
                }
            }

            return itensVenda;
        }
    }

    internal class ProdutoInfo
    {
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
    }
}
