using biltiful.Modulos.Operacoes;

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
            }
        }

        void Produtos()
        {
            Menu menu = CriarMenuDeOperacao("Produtos");

            menu.Perguntar();
        }

        void MateriasPrima()
        {
            Menu menu = CriarMenuDeOperacao("Matérias-prima");

            menu.Perguntar();
        }

        void Fornecedores()
        {
            Menu menu = CriarMenuDeOperacao("Fornecedores");

            menu.Perguntar();
        }

        void Inadimplentes()
        {
            Menu menu = CriarMenuDeOperacao("Inadimplentes");

            menu.Perguntar();
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
