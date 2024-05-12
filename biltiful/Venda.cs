using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace biltiful
{
    public class Venda
    {
        public int Id { get; private set; }
        public DateTime DataVenda { get; set; }
        public string Cliente { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemVenda> ItensVenda { get; set; }

        private static List<Venda> vendas = new List<Venda>();
        private static string arquivoVendas = @"C:\Local\Disco\Biltiful\Venda.dat";
        private static int ultimoId = 0;

        public Venda()
        {
            Id = ++ultimoId;
        }

        public static Venda CadastrarVenda()
        {
            Venda novaVenda = new Venda();

            // Definindo a data da venda como a data atual
            novaVenda.DataVenda = DateTime.Now;

            // Solicitando e validando o CPF do cliente
            Console.WriteLine("Digite o CPF do cliente:");
            novaVenda.Cliente = Console.ReadLine();

            // Verificando se o cliente existe e está ativo
            if (!VerificarCliente(novaVenda.Cliente))
            {
                Console.WriteLine("Cliente não encontrado, inativo, ou na lista de alto risco.");
                return null;
            }

            // Solicitando e validando os itens da venda
            novaVenda.ItensVenda = new List<ItemVenda>();
            while (true)
            {
                // Criando novo item de venda
                ItemVenda novoItem = ItemVenda.CriarItemVenda(novaVenda.Id);
                if (novoItem == null)
                {
                    // Se o item não puder ser criado, encerra a venda
                    if (novaVenda.ItensVenda.Count == 0)
                    {
                        Console.WriteLine("Não foi possível adicionar nenhum item à venda. Venda cancelada.");
                        return null;
                    }
                    else
                    {
                        // Se já existirem itens na venda, encerra a adição de itens
                        break;
                    }
                }

                // Adicionando o novo item à lista de itens da venda
                novaVenda.ItensVenda.Add(novoItem);

                // Verifica se atingiu o limite máximo de itens por venda (03)
                if (novaVenda.ItensVenda.Count >= 3)
                {
                    Console.WriteLine("Limite máximo de itens por venda atingido.");
                    break;
                }

                // Pergunta se deseja adicionar mais um item à venda
                Console.WriteLine("Deseja adicionar mais um item à venda? (S/N)");
                string continuar = Console.ReadLine();
                if (continuar.ToUpper() != "S")
                    break;
            }

            // Calcula o valor total da venda
            novaVenda.ValorTotal = novaVenda.ItensVenda.Sum(item => item.TotalItem);

            // Verifica se o valor total excede 9999.99
            if (novaVenda.ValorTotal > 99999.99m)
            {
                Console.WriteLine("O valor total da venda não pode exceder 9999.99.");
                return null;
            }

            // Adiciona a venda à lista de vendas
            vendas.Add(novaVenda);
            SalvarVendas();

            return novaVenda;
        }

        private static bool VerificarCliente(string cpf)
        {
            // Verifica se o arquivo Risco.dat existe
            if (File.Exists("Risco.dat"))
            {
                // Lê todas as linhas do arquivo Risco.dat
                string[] linhasRisco = File.ReadAllLines("Risco.dat");

                // Verifica se alguma linha contém o CPF
                foreach (string linha in linhasRisco)
                {
                    if (linha.Contains(cpf))
                    {
                        Console.WriteLine("Este cliente está na lista de alto risco e não pode realizar uma venda.");
                        return false;
                    }
                }
            }

            // Verifica se o arquivo Clientes.dat existe
            if (!File.Exists(@"C:\Biltiful\Cliente.dat"))
            {
                Console.WriteLine("Arquivo de clientes não encontrado.");
                return false;
            }

            // Lê todas as linhas do arquivo Clientes.dat
            string[] linhasClientes = File.ReadAllLines(@"C:\Biltiful\Cliente.dat");

            // Busca a linha correspondente ao CPF fornecido
            string linhaCliente = linhasClientes.FirstOrDefault(l => l.StartsWith(cpf));

            if (linhaCliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return false;
            }

            // Extrai os dados do cliente da linha encontrada
            string situacao = linhaCliente.Substring(86, 1); // Situação do cliente (Ativo: 'A', Inativo: 'I')
            if (situacao == "I")
            {
                Console.WriteLine("Este cliente está inativo e não pode realizar uma venda.");
                return false;
            }

            // Verifica se o cliente é maior de 18 anos
            DateTime hoje = DateTime.Today;
            int anoNascimento = int.Parse(linhaCliente.Substring(61, 4)); // Ano de nascimento do cliente
            int idade = hoje.Year - anoNascimento;
            if (hoje.Month < int.Parse(linhaCliente.Substring(65, 2)) ||
                (hoje.Month == int.Parse(linhaCliente.Substring(65, 2)) && hoje.Day < int.Parse(linhaCliente.Substring(67, 2))))
            {
                idade--;
            }

            if (idade < 18)
            {
                Console.WriteLine("O cliente é menor de 18 anos e não pode realizar uma venda.");
                return false;
            }

            // Cliente válido
            return true;
        }

        public static Venda LocalizarVenda(int id)
        {
            return vendas.FirstOrDefault(v => v.Id == id);
        }

        public static void ExcluirVenda(int id)
        {
            var venda = LocalizarVenda(id);
            if (venda != null)
            {
                vendas.Remove(venda);
                SalvarVendas();
                Console.WriteLine("Venda excluída com sucesso.");
            }
            else
            {
                Console.WriteLine("Venda não encontrada.");
            }
        }

        public static void ImprimirVendas()
        {
            foreach (var venda in vendas)
            {
                Console.WriteLine($"ID: {venda.Id}, Data da Venda: {venda.DataVenda}, Cliente: {venda.Cliente}, Valor Total: {venda.ValorTotal}");
                foreach (var item in venda.ItensVenda)
                {
                    Console.WriteLine($"   Item ID: {item.IdVenda}, Produto: {item.Produto}, Quantidade: {item.Quantidade}, Preço: {item.ValorUnitario}");
                }
            }
        }

        private static void SalvarVendas()
        {
            using (StreamWriter writer = new StreamWriter(arquivoVendas))
            {
                foreach (var venda in vendas)
                {
                    writer.WriteLine($"{venda.Id:D5}{venda.DataVenda:yyyyMMdd}{venda.Cliente.PadRight(11)}{venda.ValorTotal:F2}".Replace(".", ""));
                }
            }
        }

        public static void CarregarVendas()
        {
            if (File.Exists(arquivoVendas))
            {
                using (StreamReader reader = new StreamReader(arquivoVendas))
                {
                    string linha;
                    while ((linha = reader.ReadLine()) != null)
                    {
                        Venda venda = new Venda
                        {
                            Id = int.Parse(linha.Substring(0, 5)),
                            DataVenda = DateTime.ParseExact(linha.Substring(5, 8), "yyyyMMdd", null),
                            Cliente = linha.Substring(13, 11).Trim(),
                            ValorTotal = decimal.Parse(linha.Substring(24, 7))
                        };
                        vendas.Add(venda);
                        if (venda.Id > ultimoId)
                        {
                            ultimoId = venda.Id;
                        }
                    }
                }
            }
        }
    }
}
