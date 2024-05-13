using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful.Classes
{
    internal class ItemProducao : IEntidade
    {
        public int Id { get; private set; }
        public DateOnly DataProducao { get; private set; }
        public string MateriaPrima { get; private set; }
        public double QuantidadeMateriaPrima { get; private set; }

        public ItemProducao()
        {

        }

        public ItemProducao(int id, DateOnly dataProducao, string materiaPrima, double quantidadeMateriaPrima)
        {
            Id = id;
            DataProducao = dataProducao;
            MateriaPrima = materiaPrima;
            QuantidadeMateriaPrima = quantidadeMateriaPrima;
        }

        public void LinhaParaObjeto(string linha)
        {
            int id;
            string dia, mes, ano;
            DateOnly dataProducao;

            this.Id = int.Parse(linha.Substring(0, 5));
            dia = linha.Substring(5, 2);
            mes = linha.Substring(7, 2);
            ano = linha.Substring(9, 4);
            this.DataProducao = DateOnly.Parse($"{dia}/{mes}/{ano}");
            this.MateriaPrima = linha.Substring(13, 6);
            this.QuantidadeMateriaPrima = double.Parse(linha.Substring(19, 5).Insert(3, ","));
        }

        public string FormatarParaArquivo()
        {
            string id, data, quantidade;

            id = this.Id.ToString().PadLeft(5, '0');
            data = this.DataProducao.ToString().Replace("/", "");
            quantidade = this.QuantidadeMateriaPrima.ToString("000.00").Replace(",", "").Substring(0, 5);

            return id + data + this.MateriaPrima + quantidade;
        }
        public string Imprimir()
        {
            string s = $"Id Produção: {this.Id} | Data: {this.DataProducao.ToString(CultureInfo.CurrentCulture)}\nMateria Prima: {this.MateriaPrima}\nQuantidade utilizada: {this.QuantidadeMateriaPrima}\n";
            return s;
        }
    }
}
