using biltiful.Classes;
using biltiful.Modulos.Operacoes;

namespace biltiful.Modulos
{
    internal class ModuloVenda
    {
        

        void Excluir()
        {
            Arquivo<Venda> arquivoVendas = new Arquivo<Venda>(Constantes.DIRETORIO, Constantes.VENDA_ARQUIVO);
            List<Venda> vendas = arquivoVendas.Ler();

            Arquivo<ItemVenda> arquivoItemVendas = new Arquivo<ItemVenda>(Constantes.DIRETORIO, Constantes.ITEM_VENDA_ARQUIVO);
            List<ItemVenda> itemVendas = arquivoItemVendas.Ler(); ;

            List<ItemVenda> itensParaExcluir = new List<ItemVenda>();
        }

        void ImpressaoPorRegistro()
        {
            Arquivo<Venda> arquivo = new Arquivo<Venda>(Constantes.DIRETORIO, Constantes.VENDA_ARQUIVO);
            List<Venda> vendas = arquivo.Ler();

            if (vendas.Count <= 0)
            {
                Console.WriteLine("Não há vendas cadastradas!");
                Console.ReadKey();
                return;
            }

            new Navegador<Venda>(vendas).Iniciar();
        }

        public void Executar()
        {
            Menu menu = new Menu(" ", " ", " ", "Impressão por registo", "Sair");
            menu.LimparAposImpressao(true);
            menu.DefinirTitulo(">> VENDAS <<");

            while (true)
            {
                switch (menu.Perguntar())
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        ImpressaoPorRegistro();
                        break;
                    default:
                        return;
                }
            }
        }

    }
}
