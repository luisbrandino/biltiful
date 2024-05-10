using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful
{
    internal class Compra
    {
        int id;
        DateOnly dataCompra;
        string cnpjFornecedor;
        int valorTotal;
        List<ItemCompra> listaItens;
        List<int> listaID;

        public Compra(DateOnly data)
        {
            this.id = 0;
            this.dataCompra = data;
            this.cnpjFornecedor = "";
            this.valorTotal = 0;
            this.listaItens = new();
            this.listaID = new();
        }
    }
}
