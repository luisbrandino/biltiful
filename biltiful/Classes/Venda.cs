using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful.Classes
{
    internal class Venda : IEntidade
    {
        public int Id { get; set; }
        public DateOnly DataVenda { get; set; }
        public string Cliente { get; set; }
        public float ValorTotal { get; set; }
        public List<ItemVenda> ItensVenda { get; set; }

        public Venda() { }

        public Venda(int id, DateOnly dataVenda, string cliente, float valorTotal)
        {
            Id = id;
            DataVenda = dataVenda;
            Cliente = cliente;
            ValorTotal = valorTotal;
        }
        public Venda(int id, DateOnly dataVenda, string cliente, float valorTotal, List<ItemVenda> itensVenda)
        {
            Id = id;
            DataVenda = dataVenda;
            Cliente = cliente;
            ValorTotal = valorTotal;
            ItensVenda = itensVenda;
        }


        public string FormatarData(DateOnly data)
        {
            return $"{data.Day.ToString("00")}{data.Month.ToString("00")}{data.Year.ToString("0000")}";
        }

        public string FormatarParaArquivo()
        {
            string id, data, valorTotal;
            id = this.Id.ToString("00000");
            data = FormatarData(DataVenda);
            valorTotal = this.ValorTotal.ToString("N2").Replace(",", "").PadLeft(7, '0');

            return id + data + this.Cliente + valorTotal;
        }

        public void LinhaParaObjeto(string linha)
        {
            this.Id= int.Parse(linha.Substring(0, 5).Trim());
            int dia = int.Parse(linha.Substring(5, 2).Trim());
            int mes = int.Parse(linha.Substring(7, 2).Trim());
            int ano = int.Parse(linha.Substring(9, 4).Trim());
            this.DataVenda = new DateOnly(ano, mes, dia);
            this.Cliente = linha.Substring(13, 11).Trim();
            this.ValorTotal = float.Parse(linha.Substring(24, 7)) / 100;
        }

        public override string ToString()
        {
            /// A fim de economizar tempo
            Arquivo<ItemVenda> arquivoItemCompra = new Arquivo<ItemVenda>(Constantes.DIRETORIO, Constantes.ITEM_VENDA_ARQUIVO);
            List<ItemVenda> itensVenda = arquivoItemCompra.Ler();

            string result = $"ID: {Id.ToString("00000")}\nData: {DataVenda.Day}/{DataVenda.Month}/{DataVenda.Year}\nCPF do cliente: {Cliente}\nValor total: {ValorTotal}\n";

            result += "\nItens:\n\n";

            foreach (ItemVenda item in itensVenda)
                if (item.Id == Id)
                    result += item.ToString() + "\n\n";

            return result;
        }
    }
}
