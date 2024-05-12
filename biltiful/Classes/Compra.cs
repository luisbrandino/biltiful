using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace biltiful.Classes
{
    internal class Compra : IEntidade
    {
        public int Id { get; set; }
        public DateOnly DataCompra { get; set; }
        public string CnpjFornecedor { get; set; }
        public float ValorTotal { get; set; }
        public List<ItemCompra> ListaItens { get; set; }
        public List<int> ListaId { get; set; }

        public Compra()
        {

        }
        public Compra(int id, DateOnly dataCompra, string cnpjFornecedor, float valorTotal, List<ItemCompra> listaItens, List<int> listaId)
        {
            Id = id;
            DataCompra = dataCompra;
            CnpjFornecedor = cnpjFornecedor;
            ValorTotal = valorTotal;
            ListaItens = listaItens;
            ListaId = listaId;
        }

        public void LinhaParaObjeto(string linha)
        {
            int id = int.Parse(linha.Substring(0, 5));
            this.Id = id;

            int dia = int.Parse(linha.Substring(5, 2));
            int mes = int.Parse(linha.Substring(7, 2));
            int ano = int.Parse(linha.Substring(9, 4));
            this.DataCompra = new DateOnly(ano, mes, dia);

            string cnpj = linha.Substring(13, 14);
            this.CnpjFornecedor = cnpj;

            float vTotal = float.Parse(linha.Substring(27, 7));
            this.ValorTotal = vTotal / 100;

        }

        public string FormatarParaArquivo()
        {
            string l = $"{this.Id.ToString("00000")}";
            string lDia = DataCompra.Day.ToString("00");
            string lMes = DataCompra.Month.ToString("00");
            string lAno = DataCompra.Year.ToString("0000");

            return $"{l}{lDia}{lMes}{lAno}{this.CnpjFornecedor}{(this.ValorTotal * 100).ToString("0000000")}";
        }

        public void CadastrarCompra()
        {
            Arquivo<Compra> comp = new("C:\\biltiful\\", "Compra.dat");
            Arquivo<ItemCompra> arqItem = new("C:\\biltiful\\", "ItemCompra.dat");
            ItemCompra item = new();
            int qtdItem;
            int resposta;

            List<Compra> lCompra = comp.Ler();

            if (lCompra.Count > 0)
            {
                Id = lCompra.Last().Id + 1;
            }
            else
            {
                Id = 1;
            }

            Console.Write($"ID: {this.Id}");

            DataCompra = DateOnly.FromDateTime(DateTime.Now);
            Console.Write($"\nData: {DataCompra}");

            Arquivo<Fornecedor> fornecedor = new Arquivo<Fornecedor>("C:\\biltiful\\","Fornecedor.dat");
            List<Fornecedor> listFornecedores = fornecedor.Ler();

            while (true)
            {
                while (true)
                {

                    Console.Write("\nCNPJ: ");
                    CnpjFornecedor = Console.ReadLine();

                    while (true)
                    {

                        if (Fornecedor.VerificarCNPJ(CnpjFornecedor) == true)
                        {
                            break;
                        }
                        else
                            Console.WriteLine("Insira um CNPJ válido!\n");
                    }

                    bool achou = false;

                    foreach (var f in listFornecedores)
                    {
                        if (f.CNPJ == CnpjFornecedor)
                        {
                            achou = true;
                            break;
                        }
                    }

                    if (achou == true)
                    {
                        break;
                    }
                    else
                        Console.WriteLine("\n CNPJ não está cadastrado! \n");
                }

                

            }

            do
            {
                Console.Write("Quantidade de itens: ");
                qtdItem = int.Parse(Console.ReadLine());

                if (qtdItem < 2)
                {
                    Console.WriteLine("\nQuantidade mínima é 2!\n");
                }
                else
                {
                    if (qtdItem > 3)
                        Console.WriteLine("\nA quantidade máxima é 3!\n");
                    else
                        break;
                }

            } while (true);

            for (int i = 0; i < qtdItem; i++)
            {
                Console.WriteLine($"ID: {item.Id = this.Id}");
                Console.WriteLine($"ID: {item.DataCompra = this.DataCompra}")
                    ;
                Console.Write("Insira a matéria prima: ");
                item.MateriaPrima = Console.ReadLine();

                do
                {
                    Console.Write("\nInsira a quantidade: ");
                    item.Quantidade = float.Parse(Console.ReadLine());

                    if (item.Quantidade > 999.99 || item.Quantidade <= 0)
                    {
                        Console.WriteLine("\nQuantidade inválida!");
                        Console.WriteLine("Tem que ser maior que zero e menor que mil\n");
                    }
                    else
                        break;

                } while (true);

                do
                {
                    Console.Write("\nInsira o valor e cada unidade: ");
                    item.ValorUnitario = float.Parse(Console.ReadLine());
                    float totalItem = item.ValorUnitario * item.Quantidade;

                    if (totalItem > 9999.99 || item.ValorUnitario > 999.99 || item.ValorUnitario <= 0)
                    {
                        Console.WriteLine("\nValor unitário inválido!\n");
                    }
                    else
                    {
                        if (totalItem + this.ValorTotal > 99999.99)
                        {
                            Console.WriteLine("\nO valor total da compra foi excedido\n");
                        }
                        else
                            break;
                    }

                } while (true);

                item.TotalItem = item.ValorUnitario * item.Quantidade;
                Console.WriteLine("Total: " + item.TotalItem);

                arqItem.Inserir(item);

                ValorTotal += item.TotalItem;
            }

            comp.Inserir(this);
        }
    }
}
