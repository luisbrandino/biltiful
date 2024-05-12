using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful
{
    internal class ItemCompra
    {
        public int Id { get; set; }
        public DateOnly DataCompra { get; set; }
        public int MateriaPrima { get; set; }
        public int Quantidade { get; set; }
        public int ValorUnitario { get; set; }
        public int TotalItem { get; set; }

        public ItemCompra(int id, DateOnly dataCompra, int materiaPrima, int quantidade, int valorUnitario, int totalItem)
        {
            Id = id;
            DataCompra = dataCompra;
            MateriaPrima = materiaPrima;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }

        //Método Construtor

    }
}
