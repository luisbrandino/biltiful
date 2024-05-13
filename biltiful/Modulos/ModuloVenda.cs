using biltiful.Classes;
using biltiful.Modulos.Operacoes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos
{
    internal class ModuloVenda
    {
        Arquivo<ItemVenda> arquivoItemVendas = new Arquivo<ItemVenda>(Constantes.DIRETORIO, Constantes.ITEM_VENDA_ARQUIVO);
        List<ItemVenda> itemVendas;
        Arquivo<Venda> arquivoVendas = new Arquivo<Venda>(Constantes.DIRETORIO, Constantes.VENDA_ARQUIVO);
        List<Venda> vendas;

        public ModuloVenda()
        {
            Resetar();
        }

        void Resetar()
        {
            itemVendas = arquivoItemVendas.Ler();
            vendas = arquivoVendas.Ler();
        }

        int EntradaId()
        {
            Entrada<int> entrada = new();

            entrada.AdicionarRegra(
                (int id) => vendas.Exists(v => v.Id == id),
                "ID não registrado"
            );

            return entrada.Pegar();
        }


        void Excluir()
        {
            if (vendas.Count <= 0)
            {
                Console.WriteLine("Nenhuma venda cadastrada!");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o ID da venda: ");
            int id = EntradaId();

            /**
             *  Remove a venda selecionada pelo ID da lista de vendas (nunca será nulo pois foi checado sua existência no EntradaId)
             */
            vendas.Remove(vendas.Find(v => v.Id == id));

            List<ItemVenda> itensParaExcluir = new List<ItemVenda>();

            /**
             *  Adicione os itens da compra relacionados com o ID para uma lista auxiliar para exclusão
             */
            foreach (ItemVenda item in itemVendas)
                if (item.Id == id)
                    itensParaExcluir.Add(item);

            /**
             *  Remove os itens selecionados pelo ID da lista antes da sobrescrita do arquivo
             */
            foreach(ItemVenda item in itensParaExcluir)
                itemVendas.Remove(item);

            /**
             *  Sobrescreve os arquivos de fato com a nova lista que exclui os itens selecionados
             */
            arquivoItemVendas.Sobrescrever(itemVendas);
            arquivoVendas.Sobrescrever(vendas);
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
                Resetar();
                switch (menu.Perguntar())
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        Excluir();
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
