using biltiful.Classes;
using biltiful.Modulos.Operacoes;
using biltiful.Modulos.Operacoes.Produtos;

namespace biltiful.Modulos
{
    internal class ModuloVenda
    {


        void Excluir()
        {
            Arquivo<Venda> arquivoVendas = new Arquivo<Venda>(Constantes.DIRETORIO, Constantes.VENDA_ARQUIVO);
            List<Venda> vendas = arquivoVendas.Ler();

            Arquivo<ItemVenda> arquivoItemVendas = new Arquivo<ItemVenda>(Constantes.DIRETORIO, Constantes.ITEM_VENDA_ARQUIVO);
            List<ItemVenda> itemVendas = arquivoItemVendas.Ler();

            List<ItemVenda> itensParaExcluir = new List<ItemVenda>();
        }

        void ImpressaoPorRegistro()
        {
            Arquivo<Venda> arquivo = new Arquivo<Venda>(Constantes.DIRETORIO, Constantes.VENDA_ARQUIVO);
            List<Venda> vendas = arquivo.Ler();

            if (vendas.Count <= 0)
            {
                Console.WriteLine("Não há vendas cadastradas!");
                Console.ReadKey();
                return;
            }

            new Navegador<Venda>(vendas).Iniciar();
        }
        bool VerificarInadimplencia(string cpf)
        {
            Arquivo<Inadimplente> arquivoInadimplentes = new(Constantes.DIRETORIO, Constantes.INADIMPLENTE_ARQUIVO);
            List<Inadimplente> inadimplentes = arquivoInadimplentes.Ler();

            foreach (var item in inadimplentes)
            {
                if (cpf == item.CPF)
                    return true;
            }
            return false;
        }
        public void Executar()
        {
            Arquivo<Venda> arquivoVendas = new Arquivo<Venda>(Constantes.DIRETORIO, Constantes.VENDA_ARQUIVO);
            List<Venda> vendas = arquivoVendas.Ler();

            Arquivo<ItemVenda> arquivoItemVendas = new Arquivo<ItemVenda>(Constantes.DIRETORIO, Constantes.ITEM_VENDA_ARQUIVO);
            List<ItemVenda> itemVendas = arquivoItemVendas.Ler();

            Arquivo<Cliente> arquivoClientes = new Arquivo<Cliente>(Constantes.DIRETORIO, Constantes.CLIENTE_ARQUIVO);
            List<Cliente> clientes = arquivoClientes.Ler();

            Arquivo<Produto> arquivoProdutos = new Arquivo<Produto>(Constantes.DIRETORIO, Constantes.PRODUTO_ARQUIVO);
            List<Produto> produtos = arquivoProdutos.Ler();



            Cliente LocalizarCliente(string cpf)
            {
                try
                {
                    foreach (var item in clientes)
                    {
                        if (cpf == item.CPF)
                            return item;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Não foi possível localizar o cliente");
                }
                return null;
            }

            Produto LocalizarProduto(string codigoBarras)
            {
                foreach (var item in produtos)
                {
                    if (codigoBarras == item.CodigoBarras && item.Situacao == 'A')
                    {
                        return item;
                    }
                }
                return null;
            }

            void DadosCliente(Cliente cliente)
            {
                Console.WriteLine("Nome: " + cliente.Nome);
                Console.WriteLine("Data Nascimento: " + cliente.DataNascimento);
            }

            ItemVenda CadastrarItemVenda(int id)
            {
                Console.WriteLine("Insira o código de barras do produto: ");
                string codigoBarras = Console.ReadLine();
                Produto produto = LocalizarProduto(codigoBarras);
                if (produto != null)
                {
                    float valorUnitario = produto.ValorVenda;
                    float valorTotal;
                    int quantidade;
                    Console.WriteLine("Produto Encontrado");
                    Console.WriteLine("Nome: " + produto.Nome);
                    Console.WriteLine("Preço: " + produto.ValorVenda);
                    do
                    {

                        Console.WriteLine("Insira a quantidade que deseja comprar: ");
                        quantidade = int.Parse(Console.ReadLine());
                        valorTotal = (float)quantidade * valorUnitario;

                        if (quantidade > 999 && quantidade < 1)
                        {
                            Console.WriteLine("Quantidade não permitida");
                        }
                        else if (valorTotal > 9999.99)
                        {
                            Console.WriteLine("Valor total ultrapassa o permitido, diminua o número de itens.");
                        }
                        else
                            break;

                    } while (true);

                    return new(id, produto.CodigoBarras, quantidade, valorUnitario, valorTotal);
                }
                return null;
            }

            bool CadastrarVenda()
            {
                try
                {
                    int id = vendas.Last().Id + 1;
                    float valorTotal = 0;
                    DateOnly data = DateOnly.FromDateTime(DateTime.Now);
                    Console.WriteLine("Insira o CPF do cliente: ");
                    string CPF = Console.ReadLine();
                    Cliente cliente = LocalizarCliente(CPF);
                    if (cliente != null && VerificarInadimplencia(CPF))
                    {
                        DadosCliente(cliente);
                        int idade = DateTime.Now.Year - cliente.DataNascimento.Year;
                        if (idade < 18)
                        {
                            Console.WriteLine("Venda não autorizada para menores de 18 anos.");
                            return false;
                        }
                        List<ItemVenda> itens = new List<ItemVenda>();
                        int opc = 0;

                        while (itens.Count < 3 && opc != 1)
                        {
                            itens.Add(CadastrarItemVenda(id));
                            valorTotal += itens[itens.Count - 1].TotalItem;
                            if (valorTotal > 99999.99)
                            {
                                Console.WriteLine("Limite de valor para venda atingido!");
                                break;
                            }
                            Console.WriteLine("Deseja Adicionar mais um item?");
                            Console.WriteLine("1 - Sim | 2 - Não");
                            opc = int.Parse(Console.ReadLine());
                        }

                        Venda novaVenda = new(id, data, cliente.CPF, valorTotal, itens);
                        arquivoVendas.Inserir(novaVenda);
                        foreach (var item in itens)
                        {
                            arquivoItemVendas.Inserir(item);
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Houve um ERRO e não foi possível efetuar a compra");
                }
                return false;
            }

            Menu menu = new Menu(" ", " ", " ", "Impressão por registo", "Sair");
            menu.LimparAposImpressao(true);
            menu.DefinirTitulo(">> VENDAS <<");

            while (true)
            {
                switch (menu.Perguntar())
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        ImpressaoPorRegistro();
                        break;
                    default:
                        return;
                }
            }
        }

    }
}
