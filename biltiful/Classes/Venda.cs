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


        public string FormatarParaArquivo()
        {
            string id, data, valorTotal;
            id = this.Id.ToString().PadLeft(5, '0');
            data = this.DataVenda.ToString().Replace("/", "");
            valorTotal = this.ValorTotal.ToString().Replace(",", "").PadLeft(7, '0');

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
            this.ValorTotal = float.Parse(linha.Substring(24, 7).Insert(5, ","));
        }
    }
}
