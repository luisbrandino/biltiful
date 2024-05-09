using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful.Classes
{
    internal class ItemProducao
    {
        public int Id { get; set; }
        public DateOnly DataProducao { get; set; }
        public int MateriaPrima { get; set; }
        public int QuantidadeMateriaPrima { get; set; }

        public ItemProducao()
        {

        }
    }
}
