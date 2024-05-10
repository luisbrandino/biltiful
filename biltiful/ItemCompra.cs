using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful
{
    internal class ItemCompra
    {
        int id;
        DateOnly dataCompra;
        int materiaPrima;
        int quantidade;
        int valorUnitario;
        int totalItem;

        //Método Construtor
        public ItemCompra(int id, DateOnly dataCompra)
        {
            this.id = 0;
            this.dataCompra = dataCompra;
            int materiaPrima;
            this.quantidade = 0;
            this.valorUnitario = 0;
            this.totalItem = 0;
        }
    }
}
