using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful
{
    internal class Compra : 
    {
        public int Id { get; set; }
        public DateOnly DataCompra { get; set; }
        public string CnpjFornecedor { get; set; }
        public int ValorTotal { get; set; }
        public List<ItemCompra> ListaItens { get; set; }
        public List<int> ListaId { get; set; }

        public Compra(int id, DateOnly dataCompra, string cnpjFornecedor, int valorTotal, List<ItemCompra> listaItens, List<int> listaId)
        {
            Id = id;
            DataCompra = dataCompra;
            CnpjFornecedor = cnpjFornecedor;
            ValorTotal = valorTotal;
            ListaItens = listaItens;
            ListaId = listaId;
        }


    }
}
