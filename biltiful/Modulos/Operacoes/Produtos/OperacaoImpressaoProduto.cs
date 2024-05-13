using biltiful.Classes;

namespace biltiful.Modulos.Operacoes.Produtos
{
    internal class OperacaoImpressaoProduto
    {
        Arquivo<Produto> arquivo;

        public OperacaoImpressaoProduto()
        {
            arquivo = new Arquivo<Produto>(Constantes.DIRETORIO, Constantes.PRODUTO_ARQUIVO);
        }

        public void Executar()
        {
            Console.Clear();
            List<Produto> produtos = arquivo.Ler().Where(produto => produto.Situacao == 'A').ToList();

            if (produtos.Count == 0)
            {
                Console.WriteLine("Não há produtos cadastrados!");
                Console.ReadKey();

                return;
            }

            new Navegador<Produto>(produtos).Iniciar();
        }
    }
}
