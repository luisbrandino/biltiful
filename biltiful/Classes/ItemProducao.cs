using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful.Classes
{
    internal class ItemProducao: IEntidade
    {
        public int Id { get; set; }
        public DateOnly DataProducao { get; set; }
        public string MateriaPrima { get; set; }
        public double QuantidadeMateriaPrima { get; set; }

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
            throw new NotImplementedException();
        }

        public string FormatarParaArquivo()
        {
            throw new NotImplementedException();
        }
    }
}
