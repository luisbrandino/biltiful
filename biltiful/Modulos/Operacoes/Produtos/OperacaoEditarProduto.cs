using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Produtos
{
    internal class OperacaoEditarProduto
    {
        Arquivo<Produto> arquivo;

        public OperacaoEditarProduto()
        {
            arquivo = new Arquivo<Produto>(Constantes.DIRETORIO, Constantes.PRODUTO_ARQUIVO);
        }

        public string EntradaCodigoDeBarras()
        {
            Entrada<string> entrada = new Entrada<string>();

            entrada.AdicionarRegra(
                (string codigoDeBarras) => Produto.VerificarCodigoDeBarras(codigoDeBarras),
                "Código de barras não segue o padrão EAN-13"
            );

            entrada.AdicionarRegra(
                (string codigoDeBarras) => arquivo.Ler().Find(produto => produto.CodigoBarras == codigoDeBarras) != null,
                "Código de barras não registrado"
            );

            return entrada.Pegar();
        }

        public string EntradaNome()
        {
            Entrada<string> entrada = new Entrada<string>();

            entrada.AdicionarRegra(
                (string nome) => nome.Length >= 1 && nome.Length <= Constantes.TAMANHO_NOME_PRODUTO,
                $"Nome precisa ter entre 1 e {Constantes.TAMANHO_NOME_PRODUTO} caracteres"
            );

            return entrada.Pegar();
        }

        public float EntradaValorDeVenda()
        {
            EntradaDecimal<float> entrada = new EntradaDecimal<float>();

            entrada.AdicionarRegra(
                (float valorVenda) => Produto.VerificarValorDeVenda(valorVenda),
                $"Valor de venda não pode ser zerado ou maior que {Constantes.VALOR_VENDA_MAXIMO}"
            );

            return entrada.Pegar();
        }

        char EntradaSituacao()
        {
            EntradaChar entrada = new();

            entrada.AdicionarRegra(
                (char situacao) => char.ToUpper(situacao) == 'A' || char.ToUpper(situacao) == 'I',
                "Situação inválida. Uso: A/I"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();
            Console.Write("Informe o código de barras do produto a ser alterado: ");
            string codigoDeBarras = EntradaCodigoDeBarras();

            List<Produto> produtos = arquivo.Ler();

            Produto? produto = produtos.Find(p => p.CodigoBarras == codigoDeBarras);

            Console.Write("Informe o nome: ");
            produto.Nome = EntradaNome();

            Console.Write("Informe o valor de venda: ");
            produto.ValorVenda = EntradaValorDeVenda();

            Console.Write("Informe a situação (A/I): ");
            produto.Situacao = EntradaSituacao();

            arquivo.Sobrescrever(produtos);

            Console.WriteLine("Produto alterado!");
        }
    }
}
