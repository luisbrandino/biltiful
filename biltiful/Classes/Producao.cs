using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful.Classes
{
    internal class Producao
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
    }
}
