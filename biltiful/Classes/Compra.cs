using biltiful.Modulos.Operacoes;
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

        int DiferencaEmMeses(DateOnly data1, DateOnly data2)
        {
            return Math.Abs((data1.Month - data2.Month) + 12 * (data1.Year - data2.Year));
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

            Arquivo<Fornecedor> fornecedor = new Arquivo<Fornecedor>("C:\\biltiful\\", "Fornecedor.dat");
            List<Fornecedor> listFornecedores = fornecedor.Ler();

            while (true)
            {
                while (true)
                {
                    while (true)
                    {


                        while (true)
                        {
                            Console.Write("\nCNPJ: ");
                            CnpjFornecedor = Console.ReadLine();
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

                    Arquivo<Bloqueado> cnpjBloqueados = new Arquivo<Bloqueado>("C:\\biltiful\\", "Bloqueado.dat");
                    List<Bloqueado> listBloqueados = cnpjBloqueados.Ler();

                    bool bloqueado = false;
                    foreach (var cnpj in listBloqueados)
                    {
                        if (cnpj.CNPJ == CnpjFornecedor)
                        {
                            bloqueado = true;
                            break;
                        }
                    }

                    if (bloqueado == true)
                        Console.WriteLine("\nCNPJ bloqueado!\n");
                    else
                        break;
                }

                Fornecedor fornecedorF = null;

                foreach (var f in listFornecedores)
                {
                    if (f.CNPJ == CnpjFornecedor)
                    {
                        fornecedorF = f;
                        break;
                    }
                }



                if (DiferencaEmMeses(DateOnly.FromDateTime(DateTime.Now), fornecedorF.DataAbertura) < 6)
                {
                    Console.WriteLine("\nFornecedor possui menos de seis meses!\n");
                }
                else
                    break;

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
                Console.WriteLine($"ID: {item.DataCompra = this.DataCompra}");

                while (true)
                {
                    Arquivo<MPrima> arqMP = new Arquivo<MPrima>("C:\\biltiful\\", "Materia.dat");

                    while (true)
                    {

                        Console.Write("\nInsira a matéria prima: ");
                        item.MateriaPrima = Console.ReadLine().ToUpper();

                        if (MPrima.VerificarId(item.MateriaPrima))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nID inválido!");
                            Console.WriteLine("Uso: MP0000\n");
                        }
                    }

                    List<MPrima> listMP = arqMP.Ler();
                    bool encontrado = false;

                    foreach (var mp in listMP)
                    {
                        if (mp.Id == item.MateriaPrima)
                        {
                            encontrado = true;
                            break;
                        }
                    }

                    if (encontrado == true)
                        break;
                    else
                        Console.WriteLine("\nMateria Prima não encontrada!\n");
                }

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

        public void ExcluirCompra()
        {
            int idExcluir;
            Arquivo<Compra> arqCompra = new Arquivo<Compra>("C:\\biltiful\\", "Compra.dat");
            Arquivo<ItemCompra> arqItemCompra = new Arquivo<ItemCompra>("C:\\biltiful\\", "ItemCompra.dat");
            List<Compra> listCompra = arqCompra.Ler();
            bool achou = false;

            if (listCompra.Count == 0)
            {
                Console.WriteLine("\nLista de Compras Vazia!\n");
                return;
            }
            else
            {

                Console.Write("Informe o ID da compra que deseja excluir: ");
                idExcluir = int.Parse(Console.ReadLine());

                foreach (var c in listCompra)
                {
                    if (c.Id == idExcluir)
                    {
                        listCompra.Remove(c);
                        achou = true;
                        break;
                    }
                }

                if (achou == false)
                {
                    Console.WriteLine("\nID não encontrado!\n");
                    return;
                }

                List<ItemCompra> listItemCompra = arqItemCompra.Ler();
                List<ItemCompra> itensDelete = new List<ItemCompra>();

                foreach (var item in listItemCompra)
                {
                    if (item.Id == idExcluir)
                        itensDelete.Add(item);
                }

                foreach (var item in itensDelete)
                {
                    listItemCompra.Remove(item);
                }

                arqCompra.Sobrescrever(listCompra);
                arqItemCompra.Sobrescrever(listItemCompra);

                Console.WriteLine("\nCompra e itens excluídos!\n");

            }
        }

        public void LocalizarCompra()
        {
            Arquivo<Compra> arqCompra = new Arquivo<Compra>("C:\\biltiful\\", "Compra.dat");
            List<Compra> listCompra = arqCompra.Ler();
            Arquivo<ItemCompra> arqItemCompra = new Arquivo<ItemCompra>("C:\\biltiful\\", "ItemCompra.dat");
            List<ItemCompra> listItemCompra = arqItemCompra.Ler();
            int idLocalizar;

            if (listCompra.Count == 0)
            {
                Console.WriteLine("\nLista de Compras Vazia!\n");
                return;
            }
            else
            {

                Console.Write("\nInsira o ID da compra que você quer localizar: ");
                idLocalizar = int.Parse(Console.ReadLine());

                Compra compLocalizada = null;

                foreach (var compra in listCompra)
                {
                    if (compra.Id == idLocalizar)
                    {
                        compLocalizada = compra;
                        break;
                    }
                }

                if (compLocalizada == null)
                {
                    Console.WriteLine("\nId não encontrado!\n");
                    return;
                }

                List<ItemCompra> itensLocalizados = new List<ItemCompra>();

                foreach (var item in listItemCompra)
                {
                    if (item.Id == compLocalizada.Id)
                    {
                        itensLocalizados.Add(item);
                    }
                }

                Console.WriteLine($"\nID da compra: {compLocalizada.Id} \nData da Compra: {compLocalizada.DataCompra} \nCNPJ Fornecedor: {compLocalizada.CnpjFornecedor} \nValor Total: {compLocalizada.ValorTotal}");

                Console.WriteLine("Itens: ");

                foreach (var item in itensLocalizados)
                {
                    Console.WriteLine($"\nID do Item: {item.Id} \nData da Compra: {item.DataCompra} \nID Materia Prima: {item.MateriaPrima} \nQuantidade Matéria Prima: {item.Quantidade} \nValor Unitario Item: {item.ValorUnitario} \nValor total item: {item.TotalItem}");
                    Console.WriteLine();
                }
            }
        }

        public void ImpressaoPorRegistro()
        {
            Arquivo<Compra> arqCompra = new Arquivo<Compra>("C:\\biltiful\\", "Compra.dat");
            Arquivo<ItemCompra> arqItemCompra = new Arquivo<ItemCompra>("C:\\biltiful\\", "ItemCompra.dat");
            List<Compra> listCompra = arqCompra.Ler();

            if (listCompra.Count == 0)
            {
                Console.WriteLine("\nLista de Compras Vazia!\n");
                return;
            }

            new Navegador<Compra>(listCompra).Iniciar();
        }

        public override string? ToString()
        {
            Arquivo<ItemCompra> arqItemCompra = new Arquivo<ItemCompra>("C:\\biltiful\\", "ItemCompra.dat");
            List<ItemCompra> listItemCompra = arqItemCompra.Ler();

            string infoItemCompra = "";

            foreach (var item in listItemCompra)
            {
                if (item.Id == this.Id)
                {
                    infoItemCompra += $"\nID do Item: {item.Id} \nData da Compra: {item.DataCompra} \nID Materia Prima: {item.MateriaPrima} \nQuantidade Matéria Prima: {item.Quantidade} \nValor Unitario Item: {item.ValorUnitario} \nValor total item: {item.TotalItem}";
                    infoItemCompra += "\n";
                }
            }

            string infoCompra = $"\nID da compra: {this.Id} \nData da Compra: {this.DataCompra} \nCNPJ Fornecedor: {this.CnpjFornecedor} \nValor Total: {this.ValorTotal}";


            return infoCompra + "\nItens:\n" + infoItemCompra;
        }
    }
}