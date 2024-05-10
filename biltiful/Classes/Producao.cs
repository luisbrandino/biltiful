using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful.Classes
{
    internal class Producao: IEntidade
    {
        public int Id { get; set; }
        public DateOnly DataProducao { get; set; }
        public string Produto { get; set; }
        public double Quantidade { get; set; }

        public Producao() { }

        public Producao(int id, DateOnly dataProducao, string produto, double quantidade)
        {
            Id = id;
            DataProducao = dataProducao;
            Produto = produto;
            Quantidade = quantidade;
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
            this.Produto = linha.Substring(13, 13);
            this.Quantidade = double.Parse(linha.Substring(26, 5).Insert(3, ","));
        }

        public string FormatarParaArquivo()
        {
            string id, data, produto, quantidade;

            id = this.Id.ToString().PadLeft(5, '0');
            data = this.DataProducao.ToString().Replace("/", "");
            produto = this.Produto;
            quantidade = this.Quantidade.ToString("000.00").Replace(",", "").Substring(0,5);
            return id + data + produto + quantidade;
        }
    }
}
