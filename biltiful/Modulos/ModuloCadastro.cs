using biltiful.Modulos.Operacoes;
using biltiful.Modulos.Operacoes.Fornecedores;
using biltiful.Modulos.Operacoes.Produtos;

namespace biltiful.Modulos
{
    internal class ModuloCadastro
    {
        void Clientes()
        {
            Menu menu = CriarMenuDeOperacao("Clientes");

            switch (menu.Perguntar())
            {
                case 1:
                    new OperacaoCadastroCliente().Executar();
                    break;
                case 2:
                    new OperacaoLocalizarCliente().Executar();
                    break;
                case 3:
                    new OperacaoEditarCliente().Executar();
                    break;
                case 4:
                    new OperacaoImpressaoCliente().Executar();
                    break;
                default:
                    return;
            }
        }

        void Produtos()
        {
            Menu menu = CriarMenuDeOperacao("Produtos");

            switch (menu.Perguntar())
            {
                case 1:
                    new OperacaoCadastroProduto().Executar();
                    break;
                case 2:
                    new OperacaoLocalizarProduto().Executar();
                    break;
                case 3:
                    new OperacaoEditarProduto().Executar();
                    break;
                case 4:
                    new OperacaoImpressaoProduto().Executar();
                    break;
                default:
                    return;
            }
        }

        void MateriasPrima()
        {
            Menu menu = CriarMenuDeOperacao("Matérias-prima");

            menu.Perguntar();
        }

        void Fornecedores()
        {
            Menu menu = CriarMenuDeOperacao("Fornecedores");

            switch (menu.Perguntar())
            {
                case 1:
                    new OperacaoCadastrarFornecedor().Executar();
                    break;
                case 3:
                    new OperacaoEditarFornecedor().Executar();
                    break;
            }
        }

        void Inadimplentes()
        {
            Menu menu = CriarMenuDeOperacao("Inadimplentes");

            
        }

        void Bloqueados()
        {
            Menu menu = CriarMenuDeOperacao("Bloqueados");

            menu.Perguntar();
        }

        Menu CriarMenuDeOperacao(string entidade)
        {
            Menu menu = new("Cadastrar", "Localizar", "Editar", "Impressão por registro", "Voltar");
            menu.LimparAposImpressao(true);
            menu.DefinirTitulo($">> {entidade.ToUpper()} <<");

            return menu;
        }

        public void Executar()
        {
            Menu menu = new Menu("Clientes", "Produtos", "Matérias-prima", "Fornecedores", "Inadimplentes", "Bloqueados", "Voltar");

            menu.DefinirTitulo(">> MENU DE CADASTROS <<");

            menu.LimparAposImpressao(true);

            while (true)
            {
                switch (menu.Perguntar())
                {
                    case 1:
                        Clientes();
                        break;
                    case 2:
                        Produtos();
                        break;
                    case 3:
                        MateriasPrima();
                        break;
                    case 4:
                        Fornecedores();
                        break;
                    case 5:
                        Inadimplentes();
                        break;
                    case 6:
                        Bloqueados();
                        break;
                    default:
                        return;

                }
            }
            
        }

    }
}
