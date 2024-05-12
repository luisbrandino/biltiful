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

        public float EntradaValorVenda()
        {
            Entrada<float> entrada = new Entrada<float>();

            entrada.AdicionarRegra(
                (float valorVenda) => valorVenda > 0 && valorVenda <= Constantes.VALOR_VENDA_MAXIMO,
                $"Valor de venda não pode ser zerado ou maior que {Constantes.VALOR_VENDA_MAXIMO}"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {

        }
    }
}
