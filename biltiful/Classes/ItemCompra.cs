using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biltiful.Classes
{
    internal class ItemCompra : IEntidade
    {
        public int Id { get; set; }
        public DateOnly DataCompra { get; set; }
        public string MateriaPrima { get; set; }
        public float Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float TotalItem { get; set; }

        public ItemCompra()
        {
        }

        public ItemCompra(int id, DateOnly dataCompra, string materiaPrima, float quantidade, float valorUnitario, float totalItem)
        {
            Id = id;
            DataCompra = dataCompra;
            MateriaPrima = materiaPrima;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }

        public void LinhaParaObjeto(string linha)
        {
            int id = int.Parse(linha.Substring(0, 5));
            this.Id = id;

            int dia = int.Parse(linha.Substring(5, 2));
            int mes = int.Parse(linha.Substring(7, 2));
            int ano = int.Parse(linha.Substring(9, 4));
            this.DataCompra = new DateOnly(ano, mes, dia);

            string mPrima = linha.Substring(13, 6);
            this.MateriaPrima = mPrima.ToUpper();

            float qtd = float.Parse(linha.Substring(19, 5));
            this.Quantidade = qtd/100;

            float vUnitario = float.Parse(linha.Substring(24, 5));
            this.ValorUnitario = vUnitario / 100;

            float vTotal = float.Parse(linha.Substring(29, 6));
            this.TotalItem = vTotal / 100;
        }

        public string FormatarParaArquivo()
        {
            string l = $"{this.Id.ToString("00000")}";
            string lDia = DataCompra.Day.ToString("00");
            string lMes = DataCompra.Month.ToString("00");
            string lAno = DataCompra.Year.ToString("0000");
            string qtd = (Quantidade*100).ToString("00000");
            string lvUnit = (ValorUnitario * 100).ToString("00000") ;
            string lvTotal = (TotalItem * 100).ToString("000000");

            return $"{l}{lDia}{lMes}{lAno}{this.MateriaPrima.ToUpper()}{qtd}{lvUnit}{lvTotal}";
        }

        //Método Construtor

    }
}
