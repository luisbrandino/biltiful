﻿namespace biltiful.Classes
{
    internal class ItemVenda : IEntidade
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float TotalItem { get; set; }

        public ItemVenda() { }
        public ItemVenda(int id, string produto, int quantidade, float valorUnitario, float totalItem)
        {
            Id = id;
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }
        public string FormatarParaArquivo()
        {
            string id, quantidade, valorUnitario, totalItem;
            id = this.Id.ToString().PadLeft(5, '0');
            quantidade = this.Quantidade.ToString("000");
            valorUnitario = (this.ValorUnitario*100).ToString("00000");
            totalItem = (this.TotalItem * 100).ToString("000000");

            return id + this.Produto + quantidade + valorUnitario + totalItem;

        }

        public void LinhaParaObjeto(string linha)
        {
            this.Id = int.Parse(linha.Substring(0, 5).Trim());
            this.Produto = linha.Substring(5, 13).Trim();
            this.Quantidade = int.Parse(linha.Substring(18, 3).Trim());
            this.ValorUnitario = float.Parse(linha.Substring(21, 5).Trim()) / 100;
            this.TotalItem = float.Parse(linha.Substring(26, 6).Trim()) / 100;

        }

        public override string ToString()
        {
            Arquivo<Produto> arquivoProduto = new Arquivo<Produto>(Constantes.DIRETORIO, Constantes.PRODUTO_ARQUIVO);
            Produto produto = arquivoProduto.Ler().Find(p => p.CodigoBarras == this.Produto);
            return $"ID: {Id.ToString("00000")}\nCódigo de barras: {Produto}\t\tNome do produto: {produto.Nome}\nQuantidade: {Quantidade}\tValor unitário: {ValorUnitario.ToString("0.00")}\tTotal: {TotalItem.ToString("0.00")}";
        }
    }
}
