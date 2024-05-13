namespace biltiful.Classes
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
            quantidade = this.Quantidade.ToString();
            valorUnitario = this.ValorUnitario.ToString().Replace(",", "").PadLeft(5, '0');
            totalItem = this.TotalItem.ToString().Replace(",", "").PadLeft(7, '0');

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
            return $"ID: {Id.ToString("00000")}\nProduto: {Produto}\nQuantidade: {Quantidade}\nValor unitário: {ValorUnitario}\nTotal: {TotalItem}";
        }
    }
}
