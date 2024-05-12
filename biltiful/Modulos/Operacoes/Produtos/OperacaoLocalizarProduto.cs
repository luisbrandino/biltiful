using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Produtos
{
    internal class OperacaoLocalizarProduto
    {
        Arquivo<Produto> arquivo;

        public OperacaoLocalizarProduto()
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

        public void Executar()
        {
            Console.Clear();

            List<Produto> produtos = arquivo.Ler().Where(produto => produto.Situacao == 'A').ToList();

            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto registrado");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o código de barras: ");
            string codigoDeBarras = EntradaCodigoDeBarras();

            Produto produto = produtos.Find(p => p.CodigoBarras == codigoDeBarras);

            Console.WriteLine(produto);

            Console.ReadKey();
        }

    }
}
