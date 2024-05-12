using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Produtos
{
    internal class OperacaoCadastroProduto
    {
        Arquivo<Produto> arquivo;

        public OperacaoCadastroProduto()
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
                (string codigoDeBarras) => arquivo.Ler().Find(produto => produto.CodigoBarras == codigoDeBarras) == null,
                "Código de barras já registrado"
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

        public void Executar()
        {
            Console.Clear();
            Console.Write("Informe o código de barras: ");
            string codigoDeBarras = EntradaCodigoDeBarras();

            Console.Write("Informe o nome: ");
            string nome = EntradaNome();

            Console.Write("Informe o valor de venda: ");
            float valorDeVenda = EntradaValorDeVenda();

            arquivo.Inserir(new Produto(codigoDeBarras, nome, valorDeVenda));

            Console.WriteLine("Produto cadastrado!");
            Console.ReadKey();
        }
    }
}
